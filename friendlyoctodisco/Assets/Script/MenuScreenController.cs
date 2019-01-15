using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    //Space
    public void StartGame()
    {
        SceneManager.LoadScene("GameS");
    }

    //BOILING MUD
    public void StartGameBM()
    {
        SceneManager.LoadScene("GameBM");
    }

    //ROVER
    public void StartGameR()
    {
        SceneManager.LoadScene("Game");
    }

    //DEEP SEA
    public void StartGameDS()
    {
        SceneManager.LoadScene("GameDS");
    }

    //ACID RIVER
    public void StartGameRTR()
    {
        SceneManager.LoadScene("GameRTR");
    }

    //Desert
    public void StartGameDE()
    {
        SceneManager.LoadScene("GameDE");
    }

	//bonus round
    public void StartGameEnd()
    {
        SceneManager.LoadScene("LastScene");
    }


    //going to apron
    public void EndofGame()
    {
        SceneManager.LoadScene("EndGame");
    }

	//go to map screen
    public void Gotothemap()
    {
        SceneManager.LoadScene("MapScreen");
    }

	//go to main menu
    public void Gotomenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

	//go to credits
    public void Gotocredits()
	{
		SceneManager.LoadScene("Credits");
	}

    //go to tutorial
    public void gototutorial()
    {
        SceneManager.LoadScene("TutorialScreen");
    }

    //go to map

    public void GoToMap()
    {
        SceneManager.LoadScene("MapScreen");
    }



}

