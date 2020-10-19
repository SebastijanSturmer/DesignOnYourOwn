using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialPickerGUIController : MonoBehaviour
{
    [Header("Internal References")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _materialSelectionPanel;
    [SerializeField] private GameObject _colorChangePanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Transform _placeHolderForMaterialSelectionButtons;
    [SerializeField] private Slider _materialScaleSlider;
    [SerializeField] private Slider _materialRotationSlider;
    [SerializeField] private Button _materialsTabButton;
    [SerializeField] private Button _colorTabButton;
    [SerializeField] private Button _optionsTabButton;


    [Header("Events")]
    [SerializeField] private ScriptableEvent _onMaterialScaleChanged;
    [SerializeField] private ScriptableEvent _onMaterialRotationChanged;
    [SerializeField] private ScriptableEvent _onMaterialColorChanged;
    

    private SelectionButtonController[] _materialForSelectionButtonControllers;
    private MaterialInteractableController _currentlySelectedInteractableController;

    void Start()
    {
        SwitchTab(0); //Set default tab to Materials!
        ToggleGUI(false);

        Setup();
    }


    /******************** PUBLIC FUNCTIONS *********************/
    

    public void OnMaterialButtonSelected(EventMessage p_message)
    {
        
    }

    /// <summary>
    /// Switches tabs on material gui!
    /// </summary>
    /// <param name="p_tab">Int is casted to enum (0 - Materials, 1 - Color, 2 - Options)</param>
    public void SwitchTab(int p_tab) 
    {
        switch ((Enums.MaterialPickerTabs)p_tab)
        {
            case Enums.MaterialPickerTabs.Materials:
                _materialSelectionPanel.SetActive(true);
                _colorChangePanel.SetActive(false);
                _optionsPanel.SetActive(false);
                break;
            case Enums.MaterialPickerTabs.Color:
                _materialSelectionPanel.SetActive(false);
                _colorChangePanel.SetActive(true);
                _optionsPanel.SetActive(false);
                break;
            case Enums.MaterialPickerTabs.Options:
                _materialSelectionPanel.SetActive(false);
                _colorChangePanel.SetActive(false);
                _optionsPanel.SetActive(true);
                break;
        }
    }

    private void UpdateSliders()
    {
        _colorChangePanel.GetComponent<ColorPicker>().SetToColor(_currentlySelectedInteractableController.CurrentlySelectedColor);
        _materialScaleSlider.value = _currentlySelectedInteractableController.CurrentMaterialScale;
        _materialRotationSlider.value = _currentlySelectedInteractableController.CurrentMaterialRotation / 90; // slider is going from 0-1 but material rotation from 0-90
    }


    /// <summary>
    /// Toggles GUI (We should provide interactable controller so we can use it for GUI)
    /// </summary>
    /// <param name="p_shouldDisplay"></param>
    /// <param name="p_interactableController">Interactable Controller to use for GUI</param>
    public void ToggleGUI(bool p_shouldDisplay, InteractableController p_interactableController = null)
    {
        
        if (p_shouldDisplay)
        {
            _currentlySelectedInteractableController = (MaterialInteractableController)p_interactableController;
            DeactivateTabsIfThereAreNoOptions();
            UpdateGUI();
        }

        _mainPanel.SetActive(p_shouldDisplay);
    }

    public void OnMaterialRotationSliderChanged(float p_value)
    {
        _onMaterialRotationChanged.RaiseEvent(new FloatMessage(p_value));
    }

    public void OnMaterialScaleSliderChanged(float p_value)
    {
        _onMaterialScaleChanged.RaiseEvent(new FloatMessage(p_value));
    }

    public void OnMaterialColorChanged(EventMessage p_message)
    {
        _onMaterialColorChanged.RaiseEvent(p_message); //For now we will just raise it again!
    }


    /******************** PRIVATE FUNCTIONS *********************/
    private void Setup()
    {
        _materialForSelectionButtonControllers = _placeHolderForMaterialSelectionButtons.GetComponentsInChildren<SelectionButtonController>();

        for (int i = 0; i < _materialForSelectionButtonControllers.Length; i++)
        {
            _materialForSelectionButtonControllers[i].SetIndex(i);
        }
    }

    private void UpdateGUI()
    {
        if (_currentlySelectedInteractableController == null)
            return;

        UpdateSliders();
        UpdateAvailableMaterials();
    }

    private void DeactivateTabsIfThereAreNoOptions()
    {
        _materialsTabButton.gameObject.SetActive(true);
        _optionsTabButton.gameObject.SetActive(true);

        if (_currentlySelectedInteractableController.TexturePacks.Count <= 1)
        {
            _materialsTabButton.gameObject.SetActive(false);
            SwitchTab(1); //Set tab to color if we cant change materials!
        }

        if (!_currentlySelectedInteractableController.CanChangeRotationAndScale)
            _optionsTabButton.gameObject.SetActive(false);
    }

    private void UpdateAvailableMaterials()
    {
        for (int i = 0; i < _materialForSelectionButtonControllers.Length; i++)
        {
            _materialForSelectionButtonControllers[i].gameObject.SetActive(false);
        }

        if (_currentlySelectedInteractableController == null)
            return;

        if (_currentlySelectedInteractableController.InteractionType != Enums.InteractionType.Material)
            return;

        var l_materialInteractableController = (MaterialInteractableController)_currentlySelectedInteractableController;


        for (int i = 0; i < l_materialInteractableController.TexturePacks.Count; i++)
        {
            _materialForSelectionButtonControllers[i].gameObject.SetActive(true);
            _materialForSelectionButtonControllers[i].DisplayImage.texture = l_materialInteractableController.TexturePacks[i].AlbedoTexture;
            _materialForSelectionButtonControllers[i].Name.text = l_materialInteractableController.TexturePacks[i].name;
        }
    }
}
