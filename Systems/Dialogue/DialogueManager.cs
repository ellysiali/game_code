using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    #region Other Variables
    const float MinInputValue = 0.7f;
    private int activeDialogueLine, selectedOption;
    private bool dialogueIsActive, questionIsActive;
    private string activeLine;
    private Dialogue dialogue;
    private float lastInputTime = -100f;
    public float waitTime = 0.1f;
    #endregion

    #region Components
    [SerializeField] private Transform back;
    [SerializeField] private Transform portrait;
    [SerializeField] private Transform text;
    [SerializeField] private Transform speaker;
    [SerializeField] private GameObject option;
    [SerializeField] private Transform optionHolder;
    [SerializeField] public PlayerInputHandler InputHandler;
    public StoreManager storeManager;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        back.gameObject.SetActive(false);
        dialogueIsActive = false;
        questionIsActive = false;
        storeManager = GameObject.Find("StoreManager").GetComponent<StoreManager>();
    }
    private void Update()
    {
        if (dialogueIsActive && !CheckTypingDone())
        {
            if (InputHandler.ContinueInput)
            {
                StopTyping();
                InputHandler.UseContinueInput();
            }
            if (InputHandler.ExitInput)
            {
                InputHandler.UseExitInput();
            }
        }

        if (questionIsActive)
        {
            if (questionIsActive && CheckTypingDone())
            {
                foreach (Transform child in optionHolder)
                {
                    child.gameObject.SetActive(true);
                }

                if(InputHandler.MovementInput.x >= MinInputValue && selectedOption < optionHolder.childCount - 1 && Time.time >= lastInputTime + waitTime)
                {
                    MoveOptionSelectRight();
                    lastInputTime = Time.time;
                }
                else if (InputHandler.MovementInput.x <= -MinInputValue && selectedOption > 0 && Time.time >= lastInputTime + waitTime)
                {
                    MoveOptionSelectLeft();
                    lastInputTime = Time.time;
                }
                else if (InputHandler.ContinueInput)
                {
                    InputHandler.UseContinueInput();
                    questionIsActive = false;
                    RemoveOptions();
                    optionHolder.GetChild(selectedOption).GetComponent<Option>().Activate();
                }
                else if (InputHandler.ExitInput)
                {
                    InputHandler.UseExitInput();
                    ExitDialogue();
                }
            }
        }

        else if (InputHandler.ContinueInput && activeDialogueLine != 0 && dialogueIsActive && !storeManager.CheckIfStoreActive())
        {
            InputHandler.UseContinueInput();
            StopAllCoroutines();
            ContinueDialogue();
        }
        else if (InputHandler.ExitInput && dialogueIsActive && !storeManager.CheckIfStoreActive())
        {
            InputHandler.UseExitInput();
        }
    }
    #endregion

    #region Main Dialogue Functions
    public void StartDialogue(Dialogue dialogue)
    {
        InputHandler.SetActionMapToInteractions();
        activeDialogueLine = selectedOption = 0;
        this.dialogue = dialogue;
        back.gameObject.SetActive(true);
        dialogueIsActive = true;
        ContinueDialogue();
    }
    public void ContinueDialogue()
    {
        if (dialogue.lines.Length <= activeDialogueLine)
        {
            if (dialogue.question != null)
            {
                questionIsActive = true;
                InitializeOptions(dialogue.question);
                UpdateDialogueLine(dialogue.question.dialogue);
            }
            else
            {
                ExitDialogue();
            }
        }
        else
        {
            UpdateDialogueLine(dialogue.lines[activeDialogueLine]);
            activeDialogueLine++;
        }
    }
    public void ExitDialogue()
    {
        back.gameObject.SetActive(false);
        dialogueIsActive = false;
        questionIsActive = false;
        activeLine = "";
        RemoveOptions();
        InputHandler.SetActionMapToGameplay();
    }
    public void UpdateDialogueLine(DialogueLine line)
    {
        activeLine = line.dialogue;

        StartCoroutine(TypeText(line.dialogue));
        portrait.GetComponent<UnityEngine.UI.Image>().sprite = line.portrait;
        speaker.GetComponent<Text>().text = line.speaker;
    }
    #endregion

    #region Question/Option Functions
    public void MoveOptionSelectRight()
    {
        optionHolder.GetChild(selectedOption).GetComponent<Option>().Deselect();
        selectedOption++;
        optionHolder.GetChild(selectedOption).GetComponent<Option>().Select();
    }
    public void MoveOptionSelectLeft()
    {
        optionHolder.GetChild(selectedOption).GetComponent<Option>().Deselect();
        selectedOption--;
        optionHolder.GetChild(selectedOption).GetComponent<Option>().Select();
    }
    public void InitializeOptions(Question question)
    {
        for (int i = 0; i < question.options.Length; i++)
        {
            OptionObject workingOption = question.options[i];
            GameObject workspace = Instantiate(option, optionHolder);
            workspace.GetComponentInChildren<TextMeshProUGUI>().text = workingOption.option;
            workspace.GetComponent<Option>().action = workingOption.action;
            workspace.SetActive(false);
        }
        optionHolder.GetChild(0).GetComponent<Option>().Select();
    }
    public void RemoveOptions()
    {
        foreach (Transform child in optionHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    #endregion

    #region Other Functions
    public void StopTyping()
    {
        StopAllCoroutines();
        text.GetComponent<TextMeshProUGUI>().text = activeLine;
    }
    public bool CheckTypingDone() => text.GetComponent<TextMeshProUGUI>().text == activeLine;
    public bool CheckIfDialogueActive() => dialogueIsActive;
    public IEnumerator TypeText(string input)
    {
        text.GetComponent<TextMeshProUGUI>().text = "";
        for (int i = 0; i < input.Length; i++)
        {
            text.GetComponent<TextMeshProUGUI>().text += input[i];
            yield return new WaitForSeconds(0.02f);
        }
    }
    #endregion
}
