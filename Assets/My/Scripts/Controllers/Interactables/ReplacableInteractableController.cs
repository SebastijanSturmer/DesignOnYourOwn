using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacableInteractableController : InteractableController
{
    [SerializeField] private List<GameObject> _availableObjects;

    private GameObject _currentObject;

    public List<GameObject> AvailableObjects { get => _availableObjects; }
    public GameObject CurrentObject { get => _currentObject; }

    public override void Start()
    {

        _interactionType = Enums.InteractionType.Replace;
        SetDefaultObject();

        base.Start();
    }

    public void SetNewObject(int p_objectIndex)
    {
        _currentObject.SetActive(false);
        _availableObjects[p_objectIndex].SetActive(true);
        _currentObject = _availableObjects[p_objectIndex];
    }

    private void SetDefaultObject()
    {
        for (int i = 0; i < _availableObjects.Count; i++) //To setup everything on all available objects
            _availableObjects[i].SetActive(true);

        for (int i = 0; i < _availableObjects.Count; i++)
            _availableObjects[i].SetActive(false);

        _currentObject = _availableObjects[0];
        _currentObject.SetActive(true);
    }
}
