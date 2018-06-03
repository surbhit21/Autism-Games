﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketGame_PreBaloonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(int path_speed, string fruitPath)
    {
       iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(fruitPath), "speed", path_speed, "easetype", "linear", "oncomplete", "OnCompletingMotion"));
    }

    void OnCompletingMotion()
    {
        Camera.main.GetComponent<BasketGame_PreGameManager>().ReduceLevel();
    }
}