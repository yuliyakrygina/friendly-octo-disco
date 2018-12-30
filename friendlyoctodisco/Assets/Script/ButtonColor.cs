using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class ButtonColor : MonoBehaviour
    /*
     * 
     * 
     * This script attempted to change the color of the button depending on score.
     * Each question is 300 points, so if 300 points is rewarded, there will be a green button.
     * Else, there would be a red button. 



*/

{/*
    private AnswerData answerData;
    //private GameController gameController;
    public bool isGreen;
    //public bool isRed;

    private Button theButton;
    private ColorBlock theColor;

    

    void Start()
    {
        //gameController = FindObjectOfType<GameController>();
        theButton = GetComponent<Button>();
        theColor = GetComponent<Button>().colors;
    }

    public void ButtonTransitionColor()
    {
        int playerScore = 0;
        if (playerScore == 300)
        {
           
                //theColor.normalColor = Color.green;
                theColor.highlightedColor = Color.green;
                //theColor.pressedColor = Color.green;
                theButton.colors = theColor;

                print("Clicked");

                Debug.Log("Green - Right");
          
        }
     
    }
    */



    /*
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
 */




    //belonged on the gamecontroller script
    /*
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
     */
}
