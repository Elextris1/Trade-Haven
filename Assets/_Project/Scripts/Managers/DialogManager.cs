using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartDialog(string speakerName, string sentence)
    {
        Debug.Log (speakerName + " : " + sentence);
    }
}
