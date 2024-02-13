using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Iteracing with " + this);
        DialogManager.instance.StartDialog("NPC", "This is a Test sentence");
    }
}
