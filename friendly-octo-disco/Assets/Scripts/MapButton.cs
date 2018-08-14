using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour
{

    public void GoToMap()
    {
        SceneManager.LoadScene("MapScreen");
    }

}
