using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransformReference", menuName = "My/Scriptable references/Transform reference")] 
public class TransformReference : ScriptableReferences
{
    [SerializeField] Transform _transform;

    public Transform Transform { get => _transform; set => _transform = value; }

    public override void SetValue<T>(T l_value)
    {
        _transform = l_value as Transform;
    }

    public override T GetValue<T>()
    {
        return _transform as T;
    }

    public override void Clear()
    {
        _transform = null;  
    }
}
