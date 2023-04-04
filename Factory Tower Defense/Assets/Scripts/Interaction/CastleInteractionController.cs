using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleInteractionController : MonoBehaviour, IInteractable
{
    public UnityEvent onInteract;
    public UnityEvent onInteractReset;

    private bool isInteracted;
    public void Interact()
    {
        if (isInteracted) return;

        //Open castle UI
        isInteracted = true;
        onInteract.Invoke();
    }

    public void ResetInteraction()
    {
        isInteracted = false;
        onInteractReset.Invoke();
    }
}
