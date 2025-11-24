using UnityEngine;
using System.Collections.Generic;

public class CollectArea : MonoBehaviour
{
    [SerializeField] private LayerMask trashLayerMask;
    [SerializeField] private TrashType acceptedTrashType;
    [SerializeField] private GameObject scoreParticles;

    [SerializeField] private AudioClip checkSound;
    [SerializeField] private AudioClip failSound;

    [SerializeField] private List<TrashBehaviour> trashBehaviours = new List<TrashBehaviour>();
    
    private GameManager _gm;

    public List<TrashBehaviour> TrashBehaviours { get => trashBehaviours; set => trashBehaviours = value; }

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
            if (trashBehaviour.IsScored) return;


            TrashType trashType = trashBehaviour.TrashType;
            trashBehaviour.IsScored = true;

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

            trashBehaviours.Add(trashBehaviour);
        }
    }
    
}
