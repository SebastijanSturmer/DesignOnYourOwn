﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used for raising events.
/// </summary>
[CreateAssetMenu(fileName = "ScriptableEvent", menuName = "My/Event")]
public class ScriptableEvent : ScriptableObject
{
    [SerializeField] private Enums.ScriptableEventTag _eventTag;

    public Enums.ScriptableEventTag EventTag { get => _eventTag; }

    #region Private variables
    [SerializeField] private List<EventListener> _eventListeners;
    #endregion

    #region Unity event functions
    private void OnEnable()
    {
        _eventListeners = new List<EventListener>();
    }

    private void OnDisable()
    {
        _eventListeners = new List<EventListener>();
    }
    #endregion

    #region Public functions
    /// <summary>
    /// Adds an event listener to the list of event listeners for the event.
    /// </summary>
    /// <param name="eventListener">
    /// Event listener that is being added.
    /// </param>
    public void AddListener(EventListener eventListener)
    {
        _eventListeners.Add(eventListener);
    }

    /// <summary>
    /// Removes an event listener from the list of event listeners for the event.
    /// </summary>
    /// <param name="eventListener">
    /// Event listener that is being removed.
    /// </param>
    /// <returns>
    /// True upon successful removal, false otherwise.
    /// </returns>
    public bool RemoveListener(EventListener eventListener)
    {
        return _eventListeners.Remove(eventListener);
    }

    /// <summary>
    /// Raises the event without message.
    /// </summary>
    public void RaiseEvent()
    {
        foreach (var eventListener in _eventListeners)
        {
            foreach (var eventListenerStruct in eventListener.EventListenerStructs)
            {
                if (eventListenerStruct.ScriptableEvent == this)
                {
                    eventListenerStruct.UnityEvent.Invoke();
                }
            }
        }
    }


    /// <summary>
    /// Raises the event with the event message.
    /// </summary>
    /// <param name="eventMessage">
    /// Event message that is being sent through the event.
    /// </param>
    public void RaiseEvent(EventMessage eventMessage)
    {
        foreach(var eventListener in _eventListeners)
        {
            foreach(var eventListenerStruct in eventListener.EventListenerStructs)
            {
                if(eventListenerStruct.ScriptableEvent == this)
                {
                    eventListenerStruct.MessageUnityEvent.Invoke(eventMessage);
                }
            }
        }
    }
    #endregion

}
