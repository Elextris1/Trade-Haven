using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private IInteractable interactableObject;

    private void OnEnable()
    {
        InputManager.input.Player.Enable();
        InputManager.input.Player.Interact.performed += _ => Interact();
    }
    private void OnDisable()
    {
        InputManager.input.Player.Disable();
    }

    void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector3 moveDirection = InputManager.GetAxis2D();
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void Interact()
    {
        if (interactableObject == null)
        {
            Debug.Log("Nothing to interact with");
            return;
        }
        interactableObject.Interact();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out interactableObject))
        {
            Debug.Log("Press E to interact");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /* This might seem unneceseary since I'm setting it to null, but in case 
         * the player leaves another trigger BUT stays within interactable object's
         * trigger, the interactableObject stays intact */
        if (other.TryGetComponent(out interactableObject))
        {
            interactableObject = null;
            Debug.Log("Left interactable area");
        }
    }
}
