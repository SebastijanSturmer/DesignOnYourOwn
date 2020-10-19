using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetScriptableReference : MonoBehaviour
{
    [SerializeField] bool _invokeOnEnable = true;
    [Space(10)]
    [SerializeField] UnityEvent _event;

    private void OnEnable()
    {
        if (_invokeOnEnable)
            _event.Invoke();
    }

    private void Awake()
    {
        if (_invokeOnEnable)
            _event.Invoke();
    }

    public void InvokeManually()
    {
        _event.Invoke();
    }
}
