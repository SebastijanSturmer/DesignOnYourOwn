using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSystem : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Transform _spawnedObjectsHolder;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void OnSpawnObjectFromObjectData(EventMessage p_message)
    {
        SpawnObject(((ObjectDataMessage)p_message).ObjectData.ObjectPrefab);
    }
    public void OnDeleteObject(EventMessage p_message)
    {
        Destroy(((GameObjectMessage)p_message).GameObject);
    }

    private void SpawnObject(GameObject p_object)
    {
        Ray l_ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit l_hit;
        if (Physics.Raycast(l_ray, out l_hit))
        {
            Instantiate(p_object, new Vector3(l_hit.point.x, 0, l_hit.point.z), Quaternion.identity, _spawnedObjectsHolder);
        }
        else
        {
            Instantiate(p_object, new Vector3(_camera.transform.position.x, 0, _camera.transform.position.z), Quaternion.identity, _spawnedObjectsHolder);
        }
    }
}
