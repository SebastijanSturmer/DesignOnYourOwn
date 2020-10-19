using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableController : MonoBehaviour
{
    [SerializeField] protected bool _isSelected = false;
    
    protected Enums.InteractionType _interactionType;
    protected Enums.ApplicationMode _applicationModeInteractable;

    protected ObjectOutline _outline;

    public bool IsSelected { get => _isSelected; }
    public Enums.InteractionType InteractionType { get => _interactionType; }
    public Enums.ApplicationMode ApplicationModeInteractable { get => _applicationModeInteractable; }

    public virtual void Start()
    {
        _outline = GetComponent<ObjectOutline>();

        if (_outline == null)
            AddOutline();
        else
            ConfigureOutline();

        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<MeshCollider>();

        if (_applicationModeInteractable == Enums.ApplicationMode.None)
            AssignWhenInteractable();

    }


    public virtual void SetIsSelected(bool p_value)
    {
        _isSelected = p_value;
        if (_isSelected)
            _outline.AddOutline();
        else
            _outline.RemoveOutline();
    }

    protected void AddOutline()
    {
        gameObject.AddComponent<ObjectOutline>();
        _outline = GetComponent<ObjectOutline>();
        ConfigureOutline();
    }

    protected void ConfigureOutline()
    {
        _outline.OutlineWidth = 7;
        _outline.precomputeOutline = true;
        _outline.OutlineMode = ObjectOutline.Mode.OutlineAll;
        _outline.OutlineColor = Color.yellow;
    }

    protected void AssignWhenInteractable()
    {
        if (_interactionType == Enums.InteractionType.Light || _interactionType == Enums.InteractionType.Transform)
            _applicationModeInteractable = Enums.ApplicationMode.VirtualWalk;
        if (_interactionType == Enums.InteractionType.Material)
            _applicationModeInteractable = Enums.ApplicationMode.MaterialChanges;
        if (_interactionType == Enums.InteractionType.Replace)
            _applicationModeInteractable = Enums.ApplicationMode.ObjectPlacing;
    }
}
