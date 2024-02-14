using UnityEngine;
using UnityEngine.InputSystem;

// I like to keep all my Interfaces in one script
public interface IInteractable
{
    public Transform interactor { get; set; }

    public void TryInteracting(Transform newInteractor)
    {
        if (interactor == null)
        {
            interactor = newInteractor;
            StartInteracting();
            InputManager.input.UI.Cancel.performed += CancelPressed;
        }
        else
        {
            StopInteracting();
            interactor = null;
            InputManager.input.UI.Cancel.performed -= CancelPressed;
        }
    }

    private void CancelPressed(InputAction.CallbackContext context)
    {
        StopInteracting();
    }

    public void StartInteracting();
    public void StopInteracting();
}
