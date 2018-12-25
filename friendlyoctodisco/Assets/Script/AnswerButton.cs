using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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

    //incorperated with first and second method to putting in green, lines 117-144 on GameController Script.
    //setting color active 
    /*
 
    public void ColorofButtonActivate()
    {
        gameController.ColorofButton(answerData.isGreen);
      
    }
    */




}

