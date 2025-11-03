using UnityEngine;
using System.Collections;

public class TrashSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(2f, 5f);
    [SerializeField] private GameObject trashObject;

    private float spawnTime = 0;
    
    void Update()
    {
        // Actualizar información de debug en el inspector
        if (spawnTime < Time.time)
        {
            spawnTime = Time.time + Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            GameObject trash = InstanceManager.Instance.GetObject(trashObject);
            trash.transform.position = transform.position;
            trash.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
    
}
