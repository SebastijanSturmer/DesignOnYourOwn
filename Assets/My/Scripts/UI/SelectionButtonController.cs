using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionButtonController : MonoBehaviour, IPointerClickHandler
{
    private int _index = 0;
    [SerializeField] private RawImage _displayImage;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private ScriptableEvent _onButtonSelected;

    public int Index { get => _index; }
    public RawImage DisplayImage { get => _displayImage; set => _displayImage = value; }
    public TextMeshProUGUI Name { get => _name; set => _name = value; }

    public void SetIndex(int p_value)
    {
        _index = p_value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onButtonSelected.RaiseEvent(new IntMessage(_index));
    }
}
