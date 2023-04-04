using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Wood gathered");
    }

    public void ResetInteraction()
    {
        
    }
}
