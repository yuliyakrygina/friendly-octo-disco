using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;


public enum ButtonSizes
{
    None, Small, Medium, Large
}

public class DataController : MonoBehaviour
{
    public RoundData[] allRoundData;
	private PlayerProgress playerProgress;

    // Varaibles for which buttons
    public ButtonSizes spaceButton = ButtonSizes.None;
    public ButtonSizes boilingMudButton = ButtonSizes.None;
    public ButtonSizes marsButton = ButtonSizes.None;
    public ButtonSizes deepSeaButton = ButtonSizes.None;
    public ButtonSizes acidRiverButton = ButtonSizes.None;
    public ButtonSizes desertButton = ButtonSizes.None;

	void Start()
    {
        DontDestroyOnLoad(gameObject);


		SceneManager.LoadScene("MapScreen");
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }


}


