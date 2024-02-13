using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI speakerNameTextbox;
    [SerializeField] private TextMeshProUGUI dialogTextbox;
    [SerializeField] private GameObject continueTextbox;

    private int sentenceIndex;
    private bool isSentenceFinished;
    private string currentSentence;
    private List<string> loadedSentences;

    private void Awake()
    {
        instance = this;
    }

    private void MouseClicked(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (isSentenceFinished)
        {
            CheckForNextSentence();
        }
        else
        {
            SkipSentence();
        }
    }

    public void StartDialog(string speakerName, List<string> sentences)
    {
        DisablePlayerInput();
        dialogPanel.SetActive(true);
        speakerNameTextbox.text = speakerName;

        loadedSentences = new List<string>(sentences);
        sentenceIndex = 0;
        currentSentence = loadedSentences[sentenceIndex];

        LoadSentence(currentSentence);
    }

    public void EndDialog()
    {
        EnablePlayerInput();
        dialogPanel.SetActive(false);
        loadedSentences.Clear();
    }


    private void SkipSentence()
    {
        StopAllCoroutines();
        dialogTextbox.text = currentSentence;
        FinishSentence();
    }

    private void FinishSentence()
    {
        isSentenceFinished = true;
        continueTextbox.SetActive(true);

    }

    private void LoadSentence(string sentence)
    {
        isSentenceFinished = false;
        continueTextbox.SetActive(false);
        StartCoroutine(WriteText(sentence));
    }

    private void CheckForNextSentence()
    {
        if (sentenceIndex < loadedSentences.Count - 1)
        {
            sentenceIndex++;
            currentSentence = loadedSentences[sentenceIndex];
            LoadSentence(currentSentence);
        }
        else
        {
            EndDialog();
        }
    }

    private IEnumerator WriteText(string text)
    {
        dialogTextbox.text = string.Empty;
        foreach (char character in text)
        {
            yield return new WaitForSeconds(.05f);
            dialogTextbox.text += character.ToString();
        }
        FinishSentence();
    }

    private void EnablePlayerInput()
    {
        InputManager.input.Player.Move.Enable();
        InputManager.input.Player.Fire.Enable();
        InputManager.input.UI.Click.performed -= MouseClicked;
        InputManager.input.UI.Disable();
    }
    private void DisablePlayerInput()
    {
        InputManager.input.Player.Move.Disable();
        InputManager.input.Player.Fire.Disable();
        InputManager.input.UI.Enable();
        InputManager.input.UI.Click.performed += MouseClicked;
    }
}
