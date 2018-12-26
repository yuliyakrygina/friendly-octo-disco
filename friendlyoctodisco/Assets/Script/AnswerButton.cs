using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
[System.Serializable]


public class AnswerButton : MonoBehaviour
{

    public Text answerText;

    private AnswerData answerData;
    private GameController gameController;


    void Start()
    {

        gameController = FindObjectOfType<GameController>();
    }

    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }


    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
        
    }

    /*
    public void ButtonTransitionColor()
    {
        gameController.ColorOfButton(answerData.isGreen, answerData.isRed);
    }
    */




}

