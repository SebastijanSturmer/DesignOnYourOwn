using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ColorPicker : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private ScriptableEvent _colorChangedOnColorPicker;
    [Header("Internal references")]
    [SerializeField] private Slider _hSlider;
    [SerializeField] private Slider _sSlider;
    [SerializeField] private Slider _vSlider;
    [SerializeField] private Image _hBackgroundImage;
    [SerializeField] private Image _sBackgroundImage;
    [SerializeField] private Image _vBackgroundImage;


    private Color32 _originalColor = Color.white;
    private Color32 _modifiedColor;
    private List<SavedColorController> _savedColors; 

    private HSV _hsv = new HSV();
    private HSV _tempHSV = new HSV();


    private void Start()
    {
        _savedColors = GetComponentsInChildren<SavedColorController>().ToList();

        bool _firstEmptyFound = false;
        for (int i = 0; i < _savedColors.Count; i++)
        {
            _savedColors[i].SetIndex(i);

            if (!_savedColors[i].IsColorSaved && !_firstEmptyFound)
            {
                _savedColors[i].ResetToEmpty();
                _firstEmptyFound = true;
            }
            else if (!_savedColors[i].IsColorSaved)
            {
                _savedColors[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetH(float p_value)
    {  
        _hsv.H = p_value;
        RecalculateMenu();
    }
    public void SetS(float p_value)
    {
        _hsv.S = p_value;
        RecalculateMenu();
    }
    public void SetV(float p_value)
    {
        _hsv.V = p_value;
        RecalculateMenu();
    }

    public void SaveColor(SavedColorController p_controller)
    {
        p_controller.SaveColor(_hsv.ToColor());

        if (_savedColors.Count > p_controller.Index + 1)
            _savedColors[p_controller.Index + 1].gameObject.SetActive(true);
    }


    public void SetToColor(Color p_color)
    {
        _hsv = new HSV(p_color);

        Color.RGBToHSV(p_color, out _hsv.H, out _hsv.S, out _hsv.V);

        _hSlider.value = _hsv.H;
        _sSlider.value = _hsv.S;
        _vSlider.value = _hsv.V;

        RecalculateMenu();
    }

    private void SetSliderValuesToMatchColor()
    {
        _hSlider.value = _hsv.H;
        _sSlider.value = _hsv.S;
        _vSlider.value = _hsv.V;

        RecalculateMenu();
    }


    private void RecalculateMenu()
    {
        //Setting background colors of sliders to match!
        _tempHSV.H = _hsv.H;
        _tempHSV.S = _hsv.S;
        _tempHSV.V = 1;
        _vBackgroundImage.color = _tempHSV.ToColor();
        _tempHSV.S = 1;
        _tempHSV.V = _hsv.V;
        _sBackgroundImage.color = _tempHSV.ToColor();


        _modifiedColor = _hsv.ToColor();

        _colorChangedOnColorPicker.RaiseEvent(new ColorMessage(_modifiedColor));
    }



    
}

public class HSV
{
    public float H = 0, S = 1, V = 1;
    public HSV() { }
    public HSV(float h, float s, float v)
    {
        H = h;
        S = s;
        V = v;
    }
    public HSV(Color color)
    {
        Color.RGBToHSV(color, out H, out S, out V);
    }
    public Color32 ToColor()
    {
        return Color.HSVToRGB(H, S, V);
    }
}