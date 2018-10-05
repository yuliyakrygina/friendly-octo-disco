using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;


public class DataController : MonoBehaviour
{
    public RoundData[] allRoundData;
	private PlayerProgress playerProgress;

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


