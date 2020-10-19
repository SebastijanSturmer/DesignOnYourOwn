using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private bool _60FPSLock;
    [SerializeField] private ScriptableEvent _applicationModeChanged;

    private Enums.ApplicationMode _currentApplicationMode = Enums.ApplicationMode.VirtualWalk;

    public Enums.ApplicationMode CurrentApplicationMode { get => _currentApplicationMode; }

    public static ApplicationManager Instance;

    void Awake()
    {
        Instance = this;

        if (_60FPSLock)
            Application.targetFrameRate = 61;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void OnApplicationModeSwitched(EventMessage p_message)
    {
        _currentApplicationMode = ((ApplicationModeMessage)p_message).ApplicationMode;
        _applicationModeChanged.RaiseEvent();
    }
}
