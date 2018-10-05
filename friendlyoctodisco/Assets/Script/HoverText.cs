﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        GetComponent<MeshRenderer>().sortingOrder = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (ItemTextControl.textstatus == "off")
        {
            Destroy(gameObject);
        }
	}
}
