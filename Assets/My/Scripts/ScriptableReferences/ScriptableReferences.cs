using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableReference00", menuName = "My/Scriptable references/Scriptable refence default")]
public class ScriptableReferences : ScriptableObject
{
    
    public virtual void SetValue<T>(T l_value) where T : ScriptableObject
    { 
    }

    public virtual T GetValue<T>() where T : UnityEngine.Object
    {
        return default;
    }

    public virtual void Clear() 
    {
    }
}
