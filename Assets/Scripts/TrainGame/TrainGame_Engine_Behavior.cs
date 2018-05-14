﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrainGame_Engine_Behavior : MonoBehaviour {

	public string TRAIN_TYPE;
	Vector3 velocity = Vector3.zero; 
	float smoothTime = .5f, minDistance = 0.01f;
	// Use this for initialization
	Stack<Vector3> bogiePositions;

	int numofnonBogies = 2;
	void Start () {
		bogiePositions = new Stack<Vector3> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetTopPosition(){
		return bogiePositions.Peek ();
	}

	public void RemoveFromTop(){
		bogiePositions.Pop ();
	}

	void AddAtTop(Vector3 top){
		bogiePositions.Push (top);
	}
	// only to be used if the train is of type Key and Lock
	IEnumerator DetachBogie(){
//		var detacher = new GameObject();
		var num_of_bogies = transform.childCount ;
		int x = num_of_bogies;
		for (int i = num_of_bogies-1; i >= numofnonBogies; i--) {
			Debug.Log ("child number "+ i);
			var bogie_object = transform.GetChild (i).gameObject;
			var original_position = bogie_object.transform.position;
			AddAtTop (original_position);
			bogie_object.GetComponent<TrainGame_BogieBehavior> ().enabled = true;
			bogie_object.GetComponent<TrainGame_BogieBehavior> ().original_position = original_position;
			var move_back_to = original_position;
			move_back_to.x += x;
			x-=1;
			Debug.Log ("move_back_to " + move_back_to);
			Debug.Log("to this point1");
			yield return StartCoroutine(bogie_object.GetComponent<TrainGame_BogieBehavior> ().MoveToTargetAndSet (move_back_to, false, TrainGame_SceneVariables.BOGIE_TAG));
//			yield return new WaitForSeconds (1f);
		}

		yield return new WaitForSeconds (1f);
		for (int i = numofnonBogies; i < num_of_bogies; i++) {
			var bogie_object = transform.GetChild (i).gameObject;
			bogie_object.GetComponent<TrainGame_BogieBehavior> ().AttachBack();
		}
	}


	public void RandomizeBogies(bool touchvalue){
		var num_of_bogies = transform.childCount ;
		var N_points = Shared_ScriptForGeneralFunctions.GetNPointsAtHeight (.4f, num_of_bogies-numofnonBogies, true, 0.05f,0.05f);
		for (int i = 0; i < num_of_bogies - numofnonBogies; i++) {
			var bogie_object = transform.GetChild (i + numofnonBogies).gameObject;
			Debug.Log ("position for " + i + N_points[i] );
			StartCoroutine(bogie_object.GetComponent<TrainGame_BogieBehavior> ().MoveToTargetAndSet (N_points[i], touchvalue, TrainGame_SceneVariables.BOGIE_TAG));
		}

	}
	public IEnumerator InitialAnimation(){
//		transform.position = Shared_ScriptForGeneralFunctions.GetPointOnScreen (1.1f, .85f);
		var target = Shared_ScriptForGeneralFunctions.GetPointOnScreen (.1f, .71f);
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length * .5f);
		yield return StartCoroutine(MoveToTarget( target));
		GetComponentInChildren<ParticleSystem> ().Stop ();
		yield return new WaitForSeconds (2f);
		StartCoroutine (DetachBogie ());
	}

	public IEnumerator MoveToTarget ( Vector3 target)
	{

		while (Vector3.Distance (transform.position, target) > Mathf.Min(minDistance,0.0001f)) {
			transform.position = Vector3.SmoothDamp (transform.position, target, ref velocity, smoothTime);
			yield return null;
		} 
		transform.position = target;
	}

	public IEnumerator FinalAnimation(){
		var target = Shared_ScriptForGeneralFunctions.GetPointOnScreen (-1.1f, .71f);
		yield return StartCoroutine(MoveToTarget( target));
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
