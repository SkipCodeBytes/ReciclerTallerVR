using UnityEngine;
using System.Collections.Generic;

public class CollectArea : MonoBehaviour
{
    [SerializeField] private LayerMask trashLayerMask;
    [SerializeField] private List<TrashType> acceptedTrashTypes = new List<TrashType>();
    [SerializeField] private GameObject scoreParticles;

    [SerializeField] private List<TrashBehaviour> trashBehaviours = new List<TrashBehaviour>();
    
    private GameManager _gm;

    public List<TrashBehaviour> TrashBehaviours { get => trashBehaviours; set => trashBehaviours = value; }

    private void Start()
    {
        _gm = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & trashLayerMask) != 0)
        {
            TrashBehaviour trashBehaviour = other.GetComponent<TrashBehaviour>();

            if (trashBehaviour == null) return;
            if (!trashBehaviour.IsScored) return;

            GameObject particles = InstanceManager.Instance.GetObject(scoreParticles);
            particles.transform.position = other.transform.position;

            TrashType trashType = trashBehaviour.TrashType;
            trashBehaviour.IsScored = true;

            if (acceptedTrashTypes.Contains(trashType)) EventManager.TriggerEvent("Score+");
            else EventManager.TriggerEvent("Score-");

            trashBehaviours.Add(trashBehaviour);
        }
    }
    
}
