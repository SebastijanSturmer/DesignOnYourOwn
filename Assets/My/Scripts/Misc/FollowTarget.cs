using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _targetOffset;

    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    private float _multiplier;

    // Update is called once per frame

    private void Start()
    {
        _multiplier = 1;
    }
    void Update()
    {
        transform.position = _targetToFollow.position + _targetOffset*_multiplier;
    }

    public void DivideMultierWithPinchScale(EventMessage p_message)
    {
        float l_value = ((FloatMessage)p_message).FloatValue;

        if ((_multiplier / l_value) >= _minZoom && (_multiplier / l_value) <= _maxZoom)
            _multiplier /= l_value;
    }
}
