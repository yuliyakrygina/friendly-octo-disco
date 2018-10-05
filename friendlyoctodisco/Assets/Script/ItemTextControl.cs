using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTextControl : MonoBehaviour {

    public Transform popuptext; //text object
    public static string textstatus="off"; //says whether text is on or off


    void OnMouseEnter()
    {
        if (textstatus == "off")
        {
            if (gameObject.name == "gem")
            {
                popuptext.GetComponent<TextMesh>().text = " Text "; // \n line break
            }
            textstatus = "on";
            Instantiate(popuptext, new Vector3(transform.position.x, transform.position.y+2,0), popuptext.rotation); //position of popup text +2 bumps up

        }
    }

    void OnMouseExit()
    {
        if (textstatus == "on")
        {
            textstatus = "off";

        }
        
    }
}
