﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainParticleSystemBehavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
		transform.position = Camera.main.GetComponent<SceneVariables>().GetPointOnScreen(.5f,1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
