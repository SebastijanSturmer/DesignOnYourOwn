using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickerGUIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _objectSelectionPanel;
    [SerializeField] private Transform _placeHolderForObjectSelectionButtons;
    [SerializeField] private Texture _tempTexture;

    //[Header("Events")]


    private SelectionButtonController[] _objectForSelectionButtonControllers;
    private ReplacableInteractableController _currentlySelectedInteractableController;

    void Start()
    {
        ToggleGUI(false);
        Setup();
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
            _currentlySelectedInteractableController = (ReplacableInteractableController)p_interactableController;
            UpdateAvailableObjects();
        }

        _mainPanel.SetActive(p_shouldDisplay);

    }

    private void Setup()
    {
        _objectForSelectionButtonControllers = _placeHolderForObjectSelectionButtons.GetComponentsInChildren<SelectionButtonController>();

        for (int i = 0; i < _objectForSelectionButtonControllers.Length; i++)
        {
            _objectForSelectionButtonControllers[i].SetIndex(i);
        }

        
    }

    private void UpdateAvailableObjects()
    {
        for (int i = 0; i < _objectForSelectionButtonControllers.Length; i++)
        {
            _objectForSelectionButtonControllers[i].gameObject.SetActive(false);
        }

        if (_currentlySelectedInteractableController == null)
            return;
                

        for (int i = 0; i < _currentlySelectedInteractableController.AvailableObjects.Count; i++)
        {
            _objectForSelectionButtonControllers[i].gameObject.SetActive(true);
            _objectForSelectionButtonControllers[i].DisplayImage.texture = _tempTexture;
            _objectForSelectionButtonControllers[i].Name.text = _currentlySelectedInteractableController.AvailableObjects[i].name;
        }
    }
}
