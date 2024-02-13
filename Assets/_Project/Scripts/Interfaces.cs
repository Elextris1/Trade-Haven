//I like to keep all my Interfaces in one script


using UnityEngine.InputSystem;

public interface IInteractable
{
    public bool isInteracting { get; set; }


    public void TryInteracting()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            StartInteracting();
            InputManager.input.UI.Cancel.performed += CancelPressed;
        }
        else
        {
            StopInteracting();
            isInteracting = false;
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
