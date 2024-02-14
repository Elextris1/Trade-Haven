using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour, IUsable
{
    public virtual void Use(Transform user)
    {
        Debug.Log("Using " + this);
    }
}
