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

    private Button theButton;
    private ColorBlock theColor;

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

        theButton = GetComponent<Button>();
        theColor = GetComponent<Button>().colors; //error: NullReferenceException: Object reference not set to an instance of an object
                                                  //GameController.Start()(at Assets / Script / GameController.cs:64)

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

    public void AnswerButtonClicked(bool isCorrect, bool isGreen)
    {
        if (isCorrect)
        {

            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }

        if (isGreen)
        {
            if (playerScore == 300) {
                theColor.normalColor = Color.green;
                theColor.highlightedColor = Color.green;
                theColor.pressedColor = Color.green;
                theButton.colors = theColor; //error : NullReferenceException: Object reference not set to an instance of an object
                                             //GameController.Start()(at Assets / Script / GameController.cs:64)

                print("Clicked");
                Debug.Log("Green - Right");
            }
            else
            {
                Debug.Log("Red - Wrong");
            }

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


   //TRYING TO CHANGE COLOR, SIMILAR METHOD TO "ISCORRECT" in GameController in inspector. Choosing which will show up as 
   //green or red when clicked. 
    public void ColorOfButton(bool isGreen, bool isRed)
    {
        if (isGreen)
        {
            //theColor.normalColor = Color.green;
            theColor.highlightedColor = Color.green;
            //theColor.pressedColor = Color.green;
            theButton.colors = theColor;

            print("Clicked");
        }

        if (isRed)
        {
            theColor.highlightedColor = Color.red;
            theButton.colors = theColor;

            print("Clicked");
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
    }

 



}
