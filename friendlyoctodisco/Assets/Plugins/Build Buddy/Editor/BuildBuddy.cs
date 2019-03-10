#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using UnityEngine;
using System.IO;
using Ionic.Zip;

public class BuildBuddy : MonoBehaviour {
	[MenuItem("Build Buddy/Desktop")]
	static public void BuildGames_Desktop () {
		BuildTarget[] targets = {
			BuildTarget.StandaloneLinuxUniversal,
			BuildTarget.StandaloneOSX,
			BuildTarget.StandaloneWindows,
			BuildTarget.StandaloneWindows64
		};

		string[] targetPrefix = {"-Linux","-Mac","-Win_x86","-Win_x86-64"};
		BuildGame(targets,targetPrefix);
	}

	[MenuItem("Build Buddy/Mobile")]
	static public void BuildGames_Mobile () {
		BuildTarget[] targets = {
			BuildTarget.iOS,
			BuildTarget.Android
		};

		string[] targetPrefix = {"-iOS-Archive","-Android"};
		BuildGame(targets,targetPrefix);
	}

	/// <summary>
	/// Builds the requested items
	/// </summary>
	/// <param name="targets">Build Targets</param>
	/// <param name="targetPrefix">Prefix to name the builds</param>
	static protected void BuildGame (BuildTarget[] targets, string[] targetPrefix) {
		if(CheckForKeystoreIfAndroid(targets)==false) {
			return;
		}

		string baseDir = "Builds" + Path.DirectorySeparatorChar;
		string projectName = PlayerSettings.productName;
		string buildPath;
		string buildName;

		string failures = "";
		bool showIOSNotice = false;

#if UNITY_2018_3_OR_NEWER
		EditorApplication.ExecuteMenuItem("Window/General/Console");
#elif
		EditorApplication.ExecuteMenuItem("Window/Console");
#endif
		ClearConsole();
		UnityEngine.Debug.Log("Starting Builds at " + System.DateTime.Now.ToLocalTime());

		for(var i=0;i<targets.Length;i++) {
			if(IsIOSBuild(targets[i], targetPrefix[i])) {
				showIOSNotice = true;
			}

			// Figure the name of the folder to build to
			buildPath = baseDir + projectName + targetPrefix[i];

			// Figure the extension for the executable
			switch (targets[i]) {
				case BuildTarget.StandaloneOSX: {
					buildName = projectName + ".app";
					break;
				}
				case BuildTarget.Android: {
					buildName = projectName + ".apk";
					break;
				}
				case BuildTarget.iOS: {
					buildName = projectName;
					break;
				}
				default: {
					buildName = projectName + ".exe";
					break;
				}
			}

			UnityEngine.Debug.Log("Building" + targetPrefix[i]);
			BuildReport report;
			try {
				// Build
				report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath + Path.DirectorySeparatorChar + buildName, targets[i], BuildOptions.None);

				// Zip if not building iOS on a Mac
				if (targets[i] != BuildTarget.Android) {
					// You'll have to launch XCode for iOS for we are on a Mac, otherwise zip it so someone with XCode can build it!
					if (targets[i] == BuildTarget.iOS && (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)) {
						// @todo Figure out how to make XCode automatically build this, based on DeviceSDK settings. IE: if (PlayerSettings.iOS.sdkVersion == iOSSdkVersion.DeviceSDK) { ... if (targetPrefix[i].Contains("Archive")) {
					} else {
						UnityEngine.Debug.Log("Zipping" + targetPrefix[i]);
						using (ZipFile zip = new ZipFile()) {
							zip.AddDirectory(buildPath + Path.DirectorySeparatorChar);
							zip.Save(buildPath + ".zip");
						}
					}
				}

				// Done
				if(report.summary.totalErrors>0) {
					CheckBuildStepsForErrors(report);
					UnityEngine.Debug.Log("Building" + targetPrefix[i] + " FAILED! ");
					UnityEngine.Debug.Log(report.summary.ToString());
					failures += "\nFailed Building" + targetPrefix[i];
				} else {
					UnityEngine.Debug.Log("Building" + targetPrefix[i] + " Complete!" + report.summary.totalTime.ToString());
				}
			} catch(Exception e) {
				// Failed...
				UnityEngine.Debug.Log("Building" + targetPrefix[i] + " FAILED WITH EXCEPTION! " + e.Message);
				failures += "\nFailed Building" + targetPrefix[i];
			}
		}

		// Show a dialog so that the user can also open the folder with the builds in it if they want
		string buildCompleteMessage = "Your builds were created!";

		if (showIOSNotice) {
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer) {
				buildCompleteMessage += "\n\nZip archive are available for the iOS Build. You will need to pass them to a MacOS computer to build them.\n";
			}
		}

		if (failures.Length > 0) {
			buildCompleteMessage += "\n\nHowever, the following targets may not have been built:\n" + failures;
		}

		if (EditorUtility.DisplayDialog("Builds complete!", buildCompleteMessage, "Show Build Folder", "Okay")) {
			RevealBuildFolder();
		}
	}

    /// <summary>
    /// Clears the Console window.
    /// </summary>
	static public void ClearConsole() {
#if UNITY_2017_3_OR_NEWER
		// https://forum.unity.com/threads/solved-unity-2017-1-0f3-trouble-cleaning-console-via-code.484079/
		var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null, null);
#elif
		Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
		Type type = assembly.GetType("UnityEditor.LogEntries");
		MethodInfo method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
#endif
	}

	/// <summary>
	/// Checks if this is building for iOS
	/// </summary>
	/// <returns><c>true</c>, if IOSB uild was ised, <c>false</c> otherwise.</returns>
	/// <param name="target">Build Target</param>
	/// <param name="prefix">Prefix to name build</param>
	static protected bool IsIOSBuild(BuildTarget target, string prefix) {
		if(target==BuildTarget.iOS) {
//			UnityEngine.Debug.Log("IOS SDK Setting wanted: " + targetPrefix);
			if (prefix.Contains("Simulator")) {
//				UnityEngine.Debug.Log("iOS Simulator SDK");
				PlayerSettings.iOS.sdkVersion = iOSSdkVersion.SimulatorSDK;
			} else {
//				UnityEngine.Debug.Log("iOS Device SDK");
				PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
			}
			return true;
		}
		return false;
	}

	static private bool CheckForKeystoreIfAndroid(BuildTarget[] targets) {
		foreach(BuildTarget target in targets) {
			if(target==BuildTarget.Android) {
				if(PlayerSettings.Android.keystoreName.Length>0 && (PlayerSettings.Android.keystorePass.Length==0 || PlayerSettings.Android.keyaliasPass.Length==0)) {
					EditorUtility.DisplayDialog("Android Keystore Passwords Empty!", "Since the project is using an Android keystore, you need to use the passwords to build Android!","I will enter it!");
					OpenPlayerSetting();
					return false;
				}
			}
		}
		return true;
	}

	/// <summary>
	/// Checks the Build Report for errors if there are certain errors that we can try to warn about
	/// </summary>
	/// <param name="report">Report from BuildPipeline</param>
	static protected void CheckBuildStepsForErrors(BuildReport report) {
		foreach (BuildStep step in report.steps) {
			foreach (BuildStepMessage message in step.messages) {
				// If we need passwords, then it's probably a Player's setting error for Android
				if(message.content.Contains("Unable to sign the application; please provide passwords")) {
					OpenPlayerSetting();
				} else {
					UnityEngine.Debug.Log("*** BUILD ERROR: " + message.content + " ***" );
				}
			}
		}
	}

	/// <summary>
	/// Shows in Windows Explorer/Finder whene the build folder is.
	/// </summary>
	static public void RevealBuildFolder() {
		EditorUtility.RevealInFinder("Builds" + Path.DirectorySeparatorChar);
	}

	/// <summary>
	/// Opens the player setting window.
	/// </summary>
	static public void OpenPlayerSetting() {
#if UNITY_2018_3_OR_NEWER
		Selection.activeObject = Unsupported.GetSerializedAssetInterfaceSingleton("PlayerSettings");
#elif
		EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
#endif
	}
}
#endif