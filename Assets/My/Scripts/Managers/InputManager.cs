using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Touch Events")]
    [SerializeField] private ScriptableEvent _onTouchOnInteractableObject;
    [SerializeField] private ScriptableEvent _onTouchOld;
    [SerializeField] private ScriptableEvent _onMouseScroll;


    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _onMouseScroll.RaiseEvent(new FloatMessage(Input.mouseScrollDelta.y));
        }
    }

    public void OnLeanTap(LeanFinger p_finger)
    {

        if (LeanTouch.Fingers.Count > 1) //disable input if there are multiple fingers on screen!
            return;



        Ray l_ray = p_finger.GetRay();

        RaycastHit l_hit;

        if (Physics.Raycast(l_ray, out l_hit))
        {
            switch (l_hit.collider.tag)
            {
                case "Interactable":
                    if (l_hit.collider.gameObject.GetComponent<InteractableController>() != null) //check if collider has controller
                        _onTouchOnInteractableObject.RaiseEvent(new InteractableControllerMessage(l_hit.collider.gameObject.GetComponent<InteractableController>()));
                    else if (l_hit.collider.gameObject.GetComponentInParent<InteractableController>() != null) //if collider doesnt have controller maybe its in parent
                        _onTouchOnInteractableObject.RaiseEvent(new InteractableControllerMessage(l_hit.collider.gameObject.GetComponentInParent<InteractableController>()));
                    else //if not fuck.
                        Debug.LogError(string.Format("Interactable object {0} doesn't have InteractableController!!!", l_hit.collider.gameObject.name));
                    break;
            }
        }

    }

    public void OnLeanOld(LeanFinger p_finger)
    {
        _onTouchOld.RaiseEvent();
    }
}
