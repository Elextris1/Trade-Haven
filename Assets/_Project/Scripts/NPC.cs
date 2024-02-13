using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public bool isInteracting { get; set; }

    public void StartInteracting()
    {
        if (TryGetComponent<Dialog>(out var dialog))
        {
            if (dialog.hasDialog)
            {
                dialog.StartDialog();
            }
        }
    }

    public void StopInteracting()
    {
        DialogManager.instance.EndDialog();
    }
}
