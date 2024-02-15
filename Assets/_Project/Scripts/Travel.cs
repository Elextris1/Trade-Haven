using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour, IInteractable
{
    public Transform interactor { get; set; }
    [SerializeField] Transform travelPoint;

    public void StartInteracting()
    {
        StartTravel();
    }

    public void StopInteracting()
    {

    }

    private void StartTravel()
    {
        interactor.position = travelPoint.position;
        interactor = null;

    }
}
