using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightSettingsGUIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private Button _lightToggleButton;
    [SerializeField] private Slider _lightIntensitySlider;

    [Header("Events")]
    [SerializeField] private ScriptableEvent _changeLightIntensityOnLight;

    private LightInteractableController _currentlySelectedInteractableController;

    private bool _isLightOnControllerTurnedOn = false;
    private float _lightIntensityOnController = 0;
    private Color _lightColorOnController = Color.white;

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
            _currentlySelectedInteractableController = (LightInteractableController)p_interactableController;
            GetDataFromController();
            Setup();
        }

        _mainPanel.SetActive(p_shouldDisplay);

    }
    public void OnToggleLightButton()
    {
        _isLightOnControllerTurnedOn = !_isLightOnControllerTurnedOn;

        if (_isLightOnControllerTurnedOn)
            _lightToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off";
        else
            _lightToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Turn On";
    }

    public void OnIntensitySliderChanged(float p_value)
    {
        _changeLightIntensityOnLight.RaiseEvent(new FloatMessage(p_value));
    }

    private void Setup()
    {
        if (_currentlySelectedInteractableController == null)
            return;

        _lightIntensitySlider.value = _currentlySelectedInteractableController.Lights[0].intensity;

        if (_isLightOnControllerTurnedOn)
            _lightToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off";
        else
            _lightToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Turn On";
    }

    private void GetDataFromController()
    {
        _isLightOnControllerTurnedOn = _currentlySelectedInteractableController.Lights[0].enabled;
        _lightIntensityOnController = _currentlySelectedInteractableController.Lights[0].intensity;
        _lightColorOnController = _currentlySelectedInteractableController.Lights[0].color;
    }


}
