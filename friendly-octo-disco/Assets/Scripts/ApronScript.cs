using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApronScript : MonoBehaviour {

	public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
