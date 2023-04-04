using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float movementSpeed;

    private Vector3 startPos;

    private List<IInteractable> interactedGO = new List<IInteractable>();
    void Update()
    {
        AndroidCameraMovement();
    }

    void AndroidCameraMovement()
    {
        Touch touch;

        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (IsTargettingUI(touch)) return;

            if (touch.phase == TouchPhase.Began)
            {           
                startPos = cameraTransform.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (interactedGO.Count > 0)
                    ClearAllInteractions();

                Vector2 movePos = touch.deltaPosition;

                cameraTransform.position = cameraTransform.position + new Vector3(
                    -movePos.x * movementSpeed,
                    0,
                    -movePos.y * movementSpeed * 1.2f
                );
            }

            if(touch.phase == TouchPhase.Ended)
            {
                if (Vector3.Distance(startPos, cameraTransform.position) < 1f)
                {
                    Interact(touch);
                }
            }
        }
    }

    private void Interact(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.GetComponent<IInteractable>() != null)
            {
                hit.collider.GetComponent<IInteractable>()?.Interact();
                interactedGO.Add(hit.collider.GetComponent<IInteractable>());
            }             
        }
    }

    private void ClearAllInteractions()
    {
        foreach(IInteractable interaction in interactedGO)
        {
            interaction.ResetInteraction();
        }

        interactedGO.Clear();
    }

    private bool IsTargettingUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }
}
