using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ObjectButtonController : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private ObjectData _currentObjectData;
    [SerializeField] private Image _objectImage;
    [SerializeField] private TextMeshProUGUI _objectNameText;
    [SerializeField] private ScriptableEvent _objectClickedEvent;

    private int _index;

    public int Index { get => _index; }
    public ObjectData CurrentObjectData {get => _currentObjectData; }


    public void SetCurrentObjectData(ObjectData p_objectData)
    {
        _currentObjectData = p_objectData;
        Setup();
    }

    public void SetIndex(int p_index)
    {
        _index = p_index;
    }

    private void Setup()
    {
        if (_currentObjectData == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            _objectImage.sprite = _currentObjectData.ObjectSprite;

            if (_currentObjectData.ObjectSprite == null)
            {
                _objectNameText.gameObject.SetActive(true);
                _objectNameText.text = _currentObjectData.ObjectName;
            }
            else
                _objectNameText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.25f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _objectClickedEvent.RaiseEvent(new IntMessage(_index));
    }
}
