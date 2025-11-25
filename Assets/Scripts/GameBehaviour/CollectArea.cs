using UnityEngine;
using System.Collections.Generic;

public class CollectArea : MonoBehaviour
{
    [SerializeField] private LayerMask trashLayerMask;
    [SerializeField] private TrashType acceptedTrashType;
    [SerializeField] private GameObject scoreParticles;

    [SerializeField] private AudioClip checkSound;
    [SerializeField] private AudioClip failSound;

    [SerializeField] private List<GameObject> trashObjects = new List<GameObject>();
    
    private GameManager _gm;

    public List<GameObject> TrashObjects { get => trashObjects; set => trashObjects = value; }

    private void Start()
    {
        _gm = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & trashLayerMask) != 0)
        {
            TrashBehaviour trashBehaviour = other.GetComponent<TrashBehaviour>();

            if (trashBehaviour == null) return;
            if (trashBehaviour.CompareTag("CollectedTrash")) return;


            TrashType trashType = trashBehaviour.TrashType;
            //trashBehaviour.IsScored = true;
            trashBehaviour.SetCollectedTag();

            if (acceptedTrashType == trashType) { 
                EventManager.TriggerEvent("Score+");
                GameObject particles = InstanceManager.Instance.GetObject(scoreParticles);
                SoundController.Instance.PlaySound(checkSound);
                particles.transform.position = other.transform.position;
            }
            else 
            {
                EventManager.TriggerEvent("Score-");
                SoundController.Instance.PlaySound(failSound);
            }

            trashObjects.Add(trashBehaviour.gameObject);
        }
    }
    
}
