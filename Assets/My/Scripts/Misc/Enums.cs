using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that acts as a holder for enums.
/// </summary>
/// 
public static class Enums
{

    public enum ScriptableEventTag {None,SubscribeIndicatorControllerToObjectPool,UnsubscribeIndicatorControllerToObjectPool, OnTouchOnInteractableController }

    public enum ApplicationMode {None, VirtualWalk, ObjectPlacing, MaterialChanges }

    public enum InteractionType {None, Material, Replace, Light,Transform }

    public enum TransformField {Position, Rotation, Scale, IsActive }

    [System.Serializable]
    public enum MaterialPickerTabs {Materials, Color, Options }

    public enum TransformType { Move, Rotate, Scale , All }

    public enum ObjectCategory {Everything, Kitchen, DiningRoom, LivingRoom, BathRoom, BedRoom }

}
