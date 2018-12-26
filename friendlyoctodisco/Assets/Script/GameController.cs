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
	public Text highScoreDisplay; //addition


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
        theColor = GetComponent<Button>().colors;

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

            //theColor.normalColor = Color.green;
            theColor.highlightedColor = Color.green;
            //theColor.pressedColor = Color.green;
            theButton.colors = theColor;

            print("Clicked");


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


    /*
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
    */

    /*

    //setting the function for green buttons.
    //There would be an option on the game controller in the inspector to choose which answer will have a green color when pressed.
    // similar mechanic to "isCorrect" option on the gamecontroller inspector in the scene. 

    public void ColorofButton(bool isGreen)
    {
        /// first method
        if (isGreen)
        {
            GetComponent<Image>().color = Color.green;

        }
        //
        

        //Second Method
        if (isGreen)
        {

            var colors = GetComponent<Button>().colors;
            colors.normalColor = Color.green;
            GetComponent<Button>().colors = colors;
        }

    }
    ////////////

    */

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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MapScreen");
    }



}
