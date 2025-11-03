using UnityEngine;
using System.Collections;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnAreaBox = Vector3.one;
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
            trash.transform.position = GetRandomSpawnPosition();
            trash.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
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
