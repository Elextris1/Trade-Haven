using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private List<string> sentences = new List<string>();
    public bool hasDialog
    {
        get
        {
            if (sentences.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void StartDialog()
    {
        if (hasDialog)
        {
            DialogManager.instance.StartDialog(name, sentences);
        }
    }
}
