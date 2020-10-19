using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float _openingPanelSpeed = 0.5f;
    [SerializeField] private Color _selectedModeColor;
    [SerializeField] private Color _unselectedModeColor;
    [Header("References")]
    [SerializeField] private GameObject _toolsPanel;
    [SerializeField] private GameObject _transformTypeChangerPanel;

    [SerializeField] private MaterialPickerGUIController _materialPickerGUIController;
    [SerializeField] private ObjectPickerGUIController _objectPickerGUIController;
    [SerializeField] private ObjectPlacingGUIController _objectPlacingGUIController;
    [SerializeField] private LightSettingsGUIController _lightSettingsGUIController;

    [SerializeField] private Button _virtualWalkModeButton;
    [SerializeField] private Button _materialChangeModeButton;
    [SerializeField] private Button _objectPlacingModeButton;

    [Header("Events")]
    [SerializeField] private ScriptableEvent _switchApplicationMode;
    [SerializeField] private ScriptableEvent _changeTransformTypeOfGizmo;
    [SerializeField] private ScriptableEvent _unselectInteractableController;
    [SerializeField] private ScriptableEvent _quitApplication;

    private InteractableController _currentlySelectedInteractableController;
    private float _toolsPanelClosedPositionY;

    private void Start()
    {
        _toolsPanelClosedPositionY = -_toolsPanel.GetComponent<RectTransform>().rect.height * Screen.height / Screen.width;
        _toolsPanel.transform.position = new Vector3(_toolsPanel.transform.position.x, _toolsPanelClosedPositionY, _toolsPanel.transform.position.z);

        ToggleToolsGUI(false);
        SetActiveModeButtonSize();
        TogglePanelsDependingOnCurrentApplicationMode();
    }

    public void OnInteractableObjectSelected(EventMessage p_message)
    {
        _currentlySelectedInteractableController = ((InteractableControllerMessage)p_message).InteractableController;

        if (_currentlySelectedInteractableController.InteractionType == Enums.InteractionType.None)
            return;


        switch (_currentlySelectedInteractableController.InteractionType)
        {
            case Enums.InteractionType.Material:
                _objectPickerGUIController.ToggleGUI(false);
                _lightSettingsGUIController.ToggleGUI(false);
                _materialPickerGUIController.ToggleGUI(true, _currentlySelectedInteractableController);
                break;

            case Enums.InteractionType.Replace:
                _materialPickerGUIController.ToggleGUI(false);
                _lightSettingsGUIController.ToggleGUI(false);
                _objectPickerGUIController.ToggleGUI(true, _currentlySelectedInteractableController);
                break;

            case Enums.InteractionType.Light:
                _materialPickerGUIController.ToggleGUI(false);
                _objectPickerGUIController.ToggleGUI(false);
                _lightSettingsGUIController.ToggleGUI(true, _currentlySelectedInteractableController);
                break;
            default:
                return;

        }
        ToggleToolsGUI(true);
    }

    public void ToggleToolsGUI(bool p_shouldDisplay)
    {
        if (p_shouldDisplay)
        {
            _toolsPanel.SetActive(true);
            _toolsPanel.transform.DOMoveY(0, _openingPanelSpeed);
        }
        else
        {
            _toolsPanel.transform.DOMoveY(_toolsPanelClosedPositionY, _openingPanelSpeed).OnComplete(() => _toolsPanel.SetActive(false));
        }
    }

    public void SetActiveModeButtonSize()
    {
        switch (ApplicationManager.Instance.CurrentApplicationMode)
        {
            case Enums.ApplicationMode.VirtualWalk:
                _virtualWalkModeButton.image.DOColor(_selectedModeColor,0.3f);
                _objectPlacingModeButton.image.DOColor(_unselectedModeColor, 0.3f);
                _materialChangeModeButton.image.DOColor(_unselectedModeColor, 0.3f);
                break;
            case Enums.ApplicationMode.ObjectPlacing:
                _virtualWalkModeButton.image.DOColor(_unselectedModeColor, 0.3f);
                _objectPlacingModeButton.image.DOColor(_selectedModeColor, 0.3f);
                _materialChangeModeButton.image.DOColor(_unselectedModeColor, 0.3f);
                break;
            case Enums.ApplicationMode.MaterialChanges:
                _virtualWalkModeButton.image.DOColor(_unselectedModeColor, 0.3f);
                _objectPlacingModeButton.image.DOColor(_unselectedModeColor, 0.3f);
                _materialChangeModeButton.image.DOColor(_selectedModeColor, 0.3f);
                break;
        }
    }

    public void TogglePanelsDependingOnCurrentApplicationMode()
    {
        switch (ApplicationManager.Instance.CurrentApplicationMode)
        {
            case Enums.ApplicationMode.VirtualWalk:
                _transformTypeChangerPanel.SetActive(false);
                _objectPlacingGUIController.ToggleGUI(false);
                break;
            case Enums.ApplicationMode.MaterialChanges:
                _transformTypeChangerPanel.SetActive(false);
                _objectPlacingGUIController.ToggleGUI(false);
                break;
            case Enums.ApplicationMode.ObjectPlacing:
                _transformTypeChangerPanel.SetActive(true);
                _objectPlacingGUIController.ToggleGUI(true);
                break;
        }
    }

    //Buttons
    #region Mode Buttons
    public void OnObjectPlacingModePressed()
    {
        _switchApplicationMode.RaiseEvent(new ApplicationModeMessage(Enums.ApplicationMode.ObjectPlacing));
    }

    public void OnMaterialChangesModePressed()
    {
        _switchApplicationMode.RaiseEvent(new ApplicationModeMessage(Enums.ApplicationMode.MaterialChanges));
    }

    public void OnVirtualWalkModePressed()
    {
        _switchApplicationMode.RaiseEvent(new ApplicationModeMessage(Enums.ApplicationMode.VirtualWalk));
    }
    #endregion

    public void ChangeTransformTypeButtonPressed(int p_index)
    {
        switch (p_index)
        {
            case 0:
                _changeTransformTypeOfGizmo.RaiseEvent(new TransformTypeMessage(Enums.TransformType.Move));
                break;
            case 1:
                _changeTransformTypeOfGizmo.RaiseEvent(new TransformTypeMessage(Enums.TransformType.Scale));
                break;
            case 2:
                _changeTransformTypeOfGizmo.RaiseEvent(new TransformTypeMessage(Enums.TransformType.Rotate));
                break;
        }
    }

    public void OnCloseToolsPanel()
    {
        _unselectInteractableController.RaiseEvent();
        ToggleToolsGUI(false);
    }

    public void OnExitButtonPressed()
    {
        _quitApplication.RaiseEvent();
    }

    private Vector3 GetDimensions(GameObject obj)
    {
        Vector3 min = Vector3.one * Mathf.Infinity;
        Vector3 max = Vector3.one * Mathf.NegativeInfinity;

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 vert = mesh.vertices[i];
            min = Vector3.Min(min, vert);
            max = Vector3.Max(max, vert);
        }

        // the size is max-min multiplied by the object scale:
        return Vector3.Scale(max - min, obj.transform.localScale);
    }
}
