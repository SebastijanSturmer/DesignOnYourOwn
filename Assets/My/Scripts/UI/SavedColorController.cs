using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SavedColorController : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private bool _isColorSaved;
    [SerializeField] private Color _savedColor;
    [SerializeField] private Sprite _emptySlotSprite;

    [Header("References")]
    [SerializeField] private ColorPicker _colorPicker;

    private Image _imageComponent;
    private int _index;


    public bool IsColorSaved { get => _isColorSaved; }
    public int Index { get => _index; }

    private void Start()
    {
        _imageComponent = GetComponent<Image>();

        LoadColor();
    }

    public void SetIndex(int p_index)
    {
        _index = p_index;
    }

    public void SaveColor(Color p_color)
    {
        _savedColor = p_color;
        _isColorSaved = true;

        LoadColor();
    }

    public void ResetToEmpty()
    {
        if (_imageComponent == null)
            _imageComponent = GetComponent<Image>();

        _imageComponent.sprite = _emptySlotSprite;
        _imageComponent.color = Color.white;
    }

    private void LoadColor()
    {
        if (!_isColorSaved) 
        {
            ResetToEmpty();
            return;
        }

        if (_imageComponent == null)
            _imageComponent = GetComponent<Image>();

        _imageComponent.sprite = null;
        _imageComponent.color = _savedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isColorSaved)
            _colorPicker.SetToColor(_savedColor);
        else
        {
            _colorPicker.SaveColor(this);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f);
    }
}
