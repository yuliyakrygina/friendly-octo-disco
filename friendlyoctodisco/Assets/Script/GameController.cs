using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]

public class GameController : MonoBehaviour
{

    // add
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;


    public GameObject smallButton; //addition
    public GameObject mediumButton; //addition
    public GameObject largeButton; //addition
    public GameObject zerocorrect;
    public GameObject onecorrect;
    public GameObject twocorrect;
  

    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
	


    private DataController dataController; //call functions for the data controller
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    //private Button theButton;
    //private ColorBlock theColor;

    // Use this for initialization



  

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
       // UpdateTimeRemainingDisplay();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
        /*
        //theButton = GetComponent<Button>();
        
        */
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {


        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }

        /*
         * Displaying how much questions you answered right during the round. 
        */

        if (playerScore == 0)
        {
            Debug.Log("Zero Answer Correct");
            zerocorrect.SetActive(true);
            onecorrect.SetActive(false);
            twocorrect.SetActive(false);

        }
        else if (playerScore == 300)
        {
            Debug.Log("One Answer Correct");
            zerocorrect.SetActive(false);
            onecorrect.SetActive(true);
            twocorrect.SetActive(false);
        }
        else
        {
            Debug.Log("Both Answers Correct!");
            zerocorrect.SetActive(false);
            onecorrect.SetActive(false);
            twocorrect.SetActive(true);
        }


        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }

    }


    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }



  
    void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();

            if (timeRemaining <= 0f)
            {
                EndRound();
            }

        }
    }

    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);

        // Fiure out button to award
       
        if(playerScore==0) {
            Debug.Log("You get the small button");
            // Hide medium and big button, 
            smallButton.SetActive(true);
            mediumButton.SetActive(false);
            largeButton.SetActive(false);
            
             
        } else if(playerScore==300) {
            Debug.Log("You get the middle button");
            smallButton.SetActive(false);
            mediumButton.SetActive(true);
            largeButton.SetActive(false);
            // Hide small and big button
        } else {
            Debug.Log("You get the biggest button");
            smallButton.SetActive(false);
            mediumButton.SetActive(false);
            largeButton.SetActive(true);
            // Hide small and medium button
        }

        if (playerScore == 0)
        {
            Debug.Log("Try again!");
        }
        else if (playerScore == 300)
        {
            Debug.Log("You have one correct answer");
        }
        else
        {
            Debug.Log("You answered everything correct!");
        }



    }

 



}
