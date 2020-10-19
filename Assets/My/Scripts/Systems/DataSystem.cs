using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
    [Tooltip("We are assigning object data from Resources/Objects! Do not assign anything!")]
    [SerializeField] private List<ObjectData> _objectDataList;

    public List<ObjectData> ObjectDataList { get => _objectDataList; }

    public static DataSystem Instance;

    private void Awake()
    {
        //We are loading all object data from Resources/Objects to avoid manually assigning in inspector!
        var _objects = Resources.LoadAll("Objects", typeof(ObjectData));

        for (int i = 0; i < _objects.Length; i++)
        {
            _objectDataList.Add((ObjectData)_objects[i]);
        }

        Instance = this;
    }

    public ObjectData GetObjectDataByName(string p_name)
    {
        for (int i = 0; i < _objectDataList.Count; i++)
        {
            if (_objectDataList[i].ObjectName == p_name)
                return _objectDataList[i];
        }
        return null;
    }
}
