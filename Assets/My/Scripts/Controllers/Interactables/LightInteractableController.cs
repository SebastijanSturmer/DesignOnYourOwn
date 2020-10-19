using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightInteractableController : InteractableController
{
    [SerializeField] private List<Light> _lights = new List<Light>();
    [SerializeField] private bool _isItOnByDefault = false;
    [SerializeField] private float _defaultLightIntensity = 1;
    [SerializeField] private Color _defaultLightColor = Color.white;


    public List<Light> Lights { get => _lights; }

    public override void Start()
    {
        _interactionType = Enums.InteractionType.Light;
        SetDefaultState();

        base.Start();
    }

    public void ToggleLight()
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            _lights[i].enabled = !_lights[i].enabled;
        }
    }

    public void SetLightIntensity(float p_lightIntensity)
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            _lights[i].intensity = p_lightIntensity;
        }
        
    }
    public void SetLightColor(Color p_lightColor)
    {
        for (int i = 0; i < _lights.Count; i++)
        {
            _lights[i].color = p_lightColor;
        }
    }

    private void SetDefaultState()
    {
        if (_lights.Count == 0)
        {
            _lights = GetComponentsInChildren<Light>().ToList();
        }

        for (int i = 0; i < _lights.Count; i++)
        {
            _lights[i].enabled = _isItOnByDefault;
            _lights[i].intensity = _defaultLightIntensity;
            _lights[i].color = _defaultLightColor;
        }
    }
}
