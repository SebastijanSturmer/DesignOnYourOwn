using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInteractionController : InteractableController
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Enums.TransformField _transformFieldToAffect;
    [SerializeField] private Vector3 _startValue;
    [SerializeField] private Vector3 _endValue;
    [SerializeField] private float _speed;
    private bool _isToggled;

    public override void Start()
    {
        _interactionType = Enums.InteractionType.Transform;

        if (_transform == null)
            _transform = transform;

        base.Start();
    }

    public void Interact()
    {
        _isToggled = !_isToggled;

        Vector3 l_endValue = Vector3.zero;

        if (_isToggled)
        {
            l_endValue = _endValue;
        }
        else
        {
            l_endValue = _startValue;
        }

        switch (_transformFieldToAffect)
        {
            case Enums.TransformField.Position:
                _transform.DOLocalMove(l_endValue, _speed);
                break;
            case Enums.TransformField.Rotation:
                _transform.DOLocalRotate(l_endValue, _speed);
                break;
            case Enums.TransformField.Scale:
                _transform.DOScale(l_endValue, _speed);
                break;
            case Enums.TransformField.IsActive:
                _transform.GetComponent<Renderer>().enabled = _isToggled;
                break;
        }
    }
    
}
