using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used for objects (image, prefab, enum name, category etc.)
/// </summary>
[CreateAssetMenu(fileName = "ObjectData", menuName = "My/Data/Object Data")]
public class ObjectData : ScriptableObject
{
    [SerializeField] private Sprite _objectSprite;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private string _objectName;
    [SerializeField] private Enums.ObjectCategory _objectCategory;

    public Sprite ObjectSprite { get => _objectSprite; }
    public GameObject ObjectPrefab { get => _objectPrefab; }
    public string ObjectName { get => _objectName; }
    public Enums.ObjectCategory ObjectCategory { get => _objectCategory; }

}
