using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    //SPACE
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    //BOILING MUD
    public void StartGameBM()
    {
        SceneManager.LoadScene("GameBM");
    }

    //ROVER
    public void StartGameR()
    {
        SceneManager.LoadScene("GameR");
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

    public void StartGameEnd()
    {
        SceneManager.LoadScene("GameEnd");
    }

    public void StartGameApron()
    {
        SceneManager.LoadScene("Apron");
    }

    public void Gotothemap()
    {
        SceneManager.LoadScene("MapScreen");
    }

    public void Gotomenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    
}

