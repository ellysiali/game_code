﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public bool isSelected;
    public bool isActive;
    public ScriptableObject action;
    public DialogueManager dialogueManager;
    public StoreManager storeManager;
    public void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        storeManager = FindObjectOfType<StoreManager>();
        isSelected = isActive = false;
    }
    public void Select()
    {
        isSelected = true;
        GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    }
    public void Deselect()
    {
        isSelected = false;
        GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
    public void Activate()
    {
        try
        {
            switch (action.GetType().Name)
            {
                case nameof(Dialogue):
                    dialogueManager.StartDialogue((Dialogue)action);
                    break;
                case nameof(StoreData):
                    storeManager.ActivateStore((StoreData)action);
                    break;
                case nameof(Save):
                    dialogueManager.ExitDialogue();
                    GameStatus.GetInstance().Save();
                    break;
                default:
                    Debug.LogError("Unknown action type:" + action.GetType().Name);
                    dialogueManager.ExitDialogue();
                    break;
            }
        }
        catch (NullReferenceException)
        {
            dialogueManager.ExitDialogue();
        }
    }
}
