using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSystem : MonoBehaviour
{
    [SerializeField] private List<ScriptableEvent> _scriptableEventsPool;
    [SerializeField] private List<InteractionIndicatorController> _interactionIndicators;
    [SerializeField] private Material _mainMaterial;
        
    public List<InteractionIndicatorController> InteractionIndicators { get => _interactionIndicators; }

    public static ObjectPoolSystem Instance;


    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Instance = this;
    }

    public ScriptableEvent GetScriptableEventByTag(Enums.ScriptableEventTag p_tag)
    {
        for (int i = 0; i < _scriptableEventsPool.Count; i++)
        {
            if (_scriptableEventsPool[i].EventTag == p_tag)
                return _scriptableEventsPool[i];
        }

        return null;
    }

    public void OnInteractableIndicatorSubscribed(EventMessage p_message)
    {
        if (((InteractionIndicatorControllerMessage)p_message).InteractionIndicatorController == null)
            return;

        _interactionIndicators.Add(((InteractionIndicatorControllerMessage)p_message).InteractionIndicatorController);
    }

    public Material GetMainMaterial()
    {
        return _mainMaterial;
    }
}
