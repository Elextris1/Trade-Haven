using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour, IInteractable
{
    public Transform interactor { get; set; }

    public virtual void StartInteracting()
    {
        if (TryGetComponent<Dialog>(out var dialog))
        {
            dialog.StartDialog();
        }
    }

    public virtual void StopInteracting()
    {
        DialogManager.instance.EndDialog();
    }
}
