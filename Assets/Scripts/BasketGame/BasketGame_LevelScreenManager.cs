﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasketGame_LevelScreenManager : MonoBehaviour {

    public int previousLevel = 1,  currentLevel;
    public GameObject levelIconParent, playerIcon;
    public static BasketGame_LevelScreenManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(ShowTransition());
    }
    IEnumerator ShowTransition()
    {
        Debug.Log("called transition");
        yield return null;
        var master_go = GameObject.Find(BasketGame_SceneVariables.masterGO);
        currentLevel = master_go.GetComponent<Shared_PersistentScript>().GetNewBasketGameLevelDetails().LevelNumber;
        
        //if (previousLevel == -1)
        //{
        //    playerIcon.transform.position = destination;
        //}
        //else
        //{
        playerIcon.GetComponent<BasketGame_PlayerIconManager>().SetTouch(false);
        var val = previousLevel < currentLevel ? 1 : -1;
        Debug.Log("previous level n current level: " + previousLevel + " " + currentLevel);
        int i = previousLevel;
        playerIcon.transform.position = levelIconParent.transform.GetChild(previousLevel - 1).transform.position;
        while (i != (currentLevel+val))
        {
            Debug.Log("called transition to " + levelIconParent.transform.GetChild(i - 1).name);
            var destination = levelIconParent.transform.GetChild(i - 1).transform.position;
            yield return StartCoroutine(playerIcon.GetComponent<BasketGame_PlayerIconManager>().Transition(destination));
            i += val;
            previousLevel = i;
        }
        playerIcon.GetComponent<BasketGame_PlayerIconManager>().SetTouch(true);
        //previousLevel = currentLevel;
        //}
    }
   
}
