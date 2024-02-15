using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private IInteractable interactableObject;
    [SerializeField] private GameObject interactHint;

    private void OnEnable()
    {
        InputManager.input.Player.Enable();
        InputManager.input.Player.Interact.performed += _ => Interact();
        InputManager.input.Player.Cancel.performed += _ => GameManager.instance.PauseGame();
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
        interactableObject.TryInteracting(transform);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out interactableObject))
        {
            interactHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out interactableObject))
        {
            interactableObject = null;
            interactHint.SetActive(false);
        }
    }
}
