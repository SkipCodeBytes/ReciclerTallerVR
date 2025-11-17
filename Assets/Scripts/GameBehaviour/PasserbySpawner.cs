using System.Collections.Generic;
using UnityEngine;

public class PasserbySpawner : MonoBehaviour
{
    [SerializeField] private GameObject passerbyObject;

    [SerializeField] private List<GameObject> trashObjectList = new List<GameObject>();

    [SerializeField] private Vector2 spawnFrequencyRange = new Vector2(4.0f, 7.0f);
    [SerializeField] private float moventSpeed = 3.5f;

    [SerializeField] private float spawnTrashProbability = 0.75f;

    [SerializeField] private Transform pointSpawnerA;
    [SerializeField] private Transform pointSpawnerB;

    [SerializeField] private Vector3 throwDirection;
    [SerializeField] private Vector2 throwRandomDelay = new Vector2(0.5f, 1.5f);

    private float lastSpawnTime = 0;

    void Start()
    {
        

    }

    void Update()
    {
        if(lastSpawnTime < Time.time)
        {
            lastSpawnTime = Time.time + Random.Range(spawnFrequencyRange.x, spawnFrequencyRange.y);
            Transform passerby = InstanceManager.Instance.GetObject(passerbyObject).transform;
            passerby.position = pointSpawnerA.position;
            passerby.LookAt(pointSpawnerB);
            StartCoroutine(CinematicAnimation.MoveTowardTheTargetAt(passerby, pointSpawnerB.position, moventSpeed, () => { passerby.gameObject.SetActive(false); }));
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(pointSpawnerA != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(pointSpawnerA.transform.position, Vector3.one);
        }

        if (pointSpawnerB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(pointSpawnerB.transform.position, Vector3.one);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + throwDirection);
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Passerby"))
        {
            // Verificar probabilidad de generar basura
            if (Random.value > spawnTrashProbability) return;
            
            if (trashObjectList.Count == 0) return;
            int randomIndex = Random.Range(0, trashObjectList.Count);

            StartCoroutine(CinematicAnimation.WaitTime(Random.Range(throwRandomDelay.x, throwRandomDelay.y), () =>
            {
                Transform trashObject = InstanceManager.Instance.GetObject(trashObjectList[randomIndex]).transform;
                trashObject.transform.position = other.transform.position;
                Rigidbody trashRigisbody = trashObject.GetComponent<Rigidbody>();
                trashRigisbody.linearVelocity = Vector3.zero;
                trashRigisbody.AddForce(throwDirection, ForceMode.Impulse);
            }
            ));
            
        }
    }
}
