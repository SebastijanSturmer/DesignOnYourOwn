using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Base class for event messages.
/// </summary>
public class EventMessage
{    
}

/// <summary>
/// Event message that holds a bool value;
/// </summary>
public class BoolMessage : EventMessage
{
    public bool BoolValue;

    public BoolMessage(bool value)
    {
        BoolValue = value;
    }
}

/// <summary>
/// Event message that holds an integer value.
/// </summary>
public class IntMessage : EventMessage
{
    public int IntValue;

    public IntMessage(int value)
    {
        IntValue = value;
    }
}

/// <summary>
/// Event message that holds a float value.
/// </summary>
public class FloatMessage : EventMessage
{
    public float FloatValue;

    public FloatMessage(float value)
    {
        FloatValue = value;
    }
}

/// <summary>
/// Event message that holds a string value;
/// </summary>
public class StringMessage : EventMessage
{
    public string StringValue;

    public StringMessage(string value)
    {
        StringValue = value;
    }
}

/// <summary>
/// Event message that holds a color value;
/// </summary>
public class ColorMessage : EventMessage
{
    public Color ColorValue;

    public ColorMessage(Color value)
    {
        ColorValue = value;
    }
}

/// <summary>
/// Event message that holds a Vector3 value.
/// </summary>
public class Vector2Message : EventMessage
{
    public Vector2 Vector2Value;

    public Vector2Message(Vector2 vector2)
    {
        Vector2Value = vector2;
    }
}

/// <summary>
/// Event message that holds a Vector3 value.
/// </summary>
public class Vector3Message : EventMessage
{
    public Vector3 Vector3Value;

    public Vector3Message(Vector3 vector3)
    {
        Vector3Value = vector3;
    }
}

/// <summary>
/// Event message that holds Vector3 values needed for BezierCurve creation.
/// </summary>
public class BezierCurveMessage : EventMessage
{
    public Vector3 Point1;
    public Vector3 Point2;
    public Vector3 Point3;
    public Vector3 Point4;

    public BezierCurveMessage(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
        Point4 = point4;
    }
}

/// <summary>
/// Event message that holds a GameObject value.
/// </summary>
public class GameObjectMessage : EventMessage
{
    public GameObject GameObject;

    public GameObjectMessage(GameObject gameObject)
    {
        GameObject = gameObject;
    }
}

/// <summary>
/// Event message that holds a ObjectData value.
/// </summary>
public class ObjectDataMessage : EventMessage
{
    public ObjectData ObjectData;

    public ObjectDataMessage(ObjectData p_objectData)
    {
        ObjectData = p_objectData;
    }
}

/// <summary>
/// Event message that holds a TransformType value;
/// </summary>
public class TransformTypeMessage : EventMessage
{
    public Enums.TransformType TransformType;

    public TransformTypeMessage(Enums.TransformType p_type)
    {
        TransformType = p_type;
    }
}

/// <summary>
/// Event message that holds a InteractableController value.
/// </summary>
public class InteractableControllerMessage : EventMessage
{
    public InteractableController InteractableController;

    public InteractableControllerMessage(InteractableController p_interactableController)
    {
        InteractableController = p_interactableController;
    }
}

/// <summary>
/// Event message that holds a InteractionIndicatorController value.
/// </summary>
public class InteractionIndicatorControllerMessage : EventMessage
{
    public InteractionIndicatorController InteractionIndicatorController;

    public InteractionIndicatorControllerMessage(InteractionIndicatorController p_interactionIndicatorController)
    {
        InteractionIndicatorController = p_interactionIndicatorController;
    }
}

/// <summary>
/// Event message that holds a ApplicationMode value.
/// </summary>
public class ApplicationModeMessage : EventMessage
{
    public Enums.ApplicationMode ApplicationMode;

    public ApplicationModeMessage(Enums.ApplicationMode p_applicationMode)
    {
        ApplicationMode = p_applicationMode;
    }
}

/// <summary>
/// Event message that holds info about pointer that initialized that event.
/// </summary>
public class PointerEventDataMessage : EventMessage
{
    public PointerEventData PointerEventData;

    public PointerEventDataMessage(PointerEventData p_pointerEventData)
    {
        PointerEventData = p_pointerEventData;
    }
}




/// <summary>
/// Event message that holds particle effect.
/// </summary>
public class ParticleSystemMessage : EventMessage
{
    public ParticleSystem ParticleSystem;

    public ParticleSystemMessage(ParticleSystem p_particleSystem)
    {
        ParticleSystem = p_particleSystem;
    }
}


