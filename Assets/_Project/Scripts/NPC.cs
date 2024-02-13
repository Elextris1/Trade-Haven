using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public Transform interactor { get; set; }

    public virtual void StartInteracting()
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
