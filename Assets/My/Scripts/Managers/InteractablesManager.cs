using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    [SerializeField] private bool _debugLogs;

    [Header("Events")]
    [SerializeField] private ScriptableEvent _onInteractableObjectSelected;
    [SerializeField] private ScriptableEvent _onInteractableObjectUnselected;

    private InteractableController _currentlySelectedInteractableController;
    private List<InteractionIndicatorController> _indicatorControllers;
    private MaterialPropertyBlock _propertyBlock;

    // Start is called before the first frame update
    void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        OnApplicationModeChanged();
    }


    public void OnTouchOnInteractableObject(EventMessage p_message)
    {
        InteractableController l_interactableController = ((InteractableControllerMessage)p_message).InteractableController;

        if (ApplicationManager.Instance.CurrentApplicationMode != l_interactableController.ApplicationModeInteractable)
            return;

        if (_currentlySelectedInteractableController != null)
        {
            if (_currentlySelectedInteractableController == l_interactableController)
            {
                DeselectCurrentInteractableController();
                return;
            }
        }

        if (_currentlySelectedInteractableController != null)
            _currentlySelectedInteractableController.SetIsSelected(false);

        l_interactableController.SetIsSelected(true);

        _currentlySelectedInteractableController = l_interactableController;

        _onInteractableObjectSelected.RaiseEvent(new InteractableControllerMessage(_currentlySelectedInteractableController));

        if (_currentlySelectedInteractableController.InteractionType == Enums.InteractionType.Transform)
            OnTransformInteract();
    }

    public void OnApplicationModeChanged()
    {
        if (ApplicationManager.Instance.CurrentApplicationMode == Enums.ApplicationMode.VirtualWalk)
            DeselectCurrentInteractableController();

        _indicatorControllers = ObjectPoolSystem.Instance.InteractionIndicators;
        ToggleIndicators();
    }

    public void DeselectCurrentInteractableController()
    {
        if (_currentlySelectedInteractableController == null)
            return;

        _currentlySelectedInteractableController.SetIsSelected(false);

        _currentlySelectedInteractableController = null;
        _onInteractableObjectUnselected.RaiseEvent();
    }

    /************************ TRANSFORM INTERACTABLE OBJECT FUNCTIONS *******************************/

    public void OnTransformInteract()
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Transform)
            return;

        var l_transformInteractableController = (TransformInteractionController)_currentlySelectedInteractableController;

        l_transformInteractableController.Interact();

        l_transformInteractableController.SetIsSelected(false);

    }

    /************************ MATERIAL INTERACTABLE OBJECT FUNCTIONS *******************************/
    public void OnMaterialSelected(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Material)
            return;

        var l_materialInteractableController = (MaterialInteractableController)_currentlySelectedInteractableController;

        int l_materialIndex = ((IntMessage)p_message).IntValue;

        l_materialInteractableController.SetTexturePackByIndex(l_materialIndex);

        Utilities.DebugLog(_debugLogs,"Material succesfully changed!");
    }

    public void OnMaterialScaleChanged(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Material)
            return;

        var l_materialInteractableController = (MaterialInteractableController)_currentlySelectedInteractableController;

        l_materialInteractableController.SetMaterialScale(((FloatMessage)p_message).FloatValue);

        Utilities.DebugLog(_debugLogs, "Material succesfully changed!");
    }

    public void OnMaterialRotationChanged(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Material)
            return;

        var l_materialInteractableController = (MaterialInteractableController)_currentlySelectedInteractableController;

        
        l_materialInteractableController.SetMaterialRotation(((FloatMessage)p_message).FloatValue * 90f);

        Utilities.DebugLog(_debugLogs, "Material succesfully changed!");
    }

    public void OnMaterialColorChanged(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Material)
            return;

        var l_materialInteractableController = (MaterialInteractableController)_currentlySelectedInteractableController;

        l_materialInteractableController.SetMaterialColor(((ColorMessage)p_message).ColorValue);

        Utilities.DebugLog(_debugLogs, "Material succesfully changed!");
    }

    /************************ REPLACABLE INTERACTABLE OBJECT FUNCTIONS *******************************/

    public void OnChangeReplacableObject(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Replace)
            return;

        var l_replacableInteractableController = (ReplacableInteractableController)_currentlySelectedInteractableController;

        int l_objectIndex = ((IntMessage)p_message).IntValue;

        l_replacableInteractableController.SetNewObject(l_objectIndex);
    }

    /************************ LIGHT INTERACTABLE OBJECT FUNCTIONS *******************************/

    public void OnToggleLight()
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Light)
            return;

        var l_lightInteractableController = (LightInteractableController)_currentlySelectedInteractableController;


        l_lightInteractableController.ToggleLight();
    }

    public void OnChangeLightIntensity(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Light)
            return;

        var l_lightInteractableController = (LightInteractableController)_currentlySelectedInteractableController;

        float l_lightIntensity = ((FloatMessage)p_message).FloatValue;

        l_lightInteractableController.SetLightIntensity(l_lightIntensity);
    }

    public void OnChangeLightColor(EventMessage p_message)
    {
        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Light)
            return;

        var l_lightInteractableController = (LightInteractableController)_currentlySelectedInteractableController;

        Color l_lightColor = ((ColorMessage)p_message).ColorValue;

        l_lightInteractableController.SetLightColor(l_lightColor);
    }


    /************************ PRIVATE FUNCTIONS *******************************/
    private void ToggleIndicators()
    {
        for (int i = 0; i < _indicatorControllers.Count; i++)
        {
            if (_indicatorControllers[i] != null)
                _indicatorControllers[i].ToggleIndicatorDependingOnCurrentApplicationMode();
        }
    }
}
