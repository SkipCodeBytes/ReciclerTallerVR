using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnAreaBox = Vector3.one;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(2f, 5f);
    [SerializeField] private List<GameObject> trashObjectList = new List<GameObject>();

    private float spawnTime = 0;
    
    void Update()
    {
        if (spawnTime < Time.time)
        {
            spawnTime = Time.time + Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            SpawnRandomTrash();
        }
    }
    
    private void SpawnRandomTrash()
    {
        if (trashObjectList.Count == 0) return;

        int randomIndex = Random.Range(0, trashObjectList.Count);
        GameObject selectedTrashPrefab = trashObjectList[randomIndex];
        
        if (selectedTrashPrefab == null) return;

        GameObject trash = InstanceManager.Instance.GetObject(selectedTrashPrefab);
        trash.transform.position = GetRandomSpawnPosition();
        
        Rigidbody rb = trash.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaBox.x * 0.5f, spawnAreaBox.x * 0.5f),
            Random.Range(-spawnAreaBox.y * 0.5f, spawnAreaBox.y * 0.5f),
            Random.Range(-spawnAreaBox.z * 0.5f, spawnAreaBox.z * 0.5f)
        );
        
        return transform.position + randomOffset;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, spawnAreaBox);
    }
#endif
}
