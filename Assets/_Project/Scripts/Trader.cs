using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    public Transform interactor { get; set; }
    [SerializeField] private bool hasDialog;

    [SerializeField]
    public void StartInteracting()
    {
        if (hasDialog)
        {
            if (TryGetComponent<Dialog>(out var dialog))
            {
                dialog.StartDialog();
                DialogManager.instance.OnDialogEnd += DisableDialog;
                return;
            }
        }
    }

    private void DisableDialog()
    {
        hasDialog = false;
        if (TryGetComponent<Shop>(out var shop))
        {
            shop.isOpen = true;
        }
        DialogManager.instance.OnDialogEnd -= DisableDialog;
        Destroy(this);
    }

    public void StopInteracting()
    {
        DialogManager.instance.OnDialogEnd -= DisableDialog;
        DialogManager.instance.EndDialog();
    }
}
