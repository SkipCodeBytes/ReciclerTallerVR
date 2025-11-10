using UnityEngine;
using System.Collections.Generic;

public class CollectArea : MonoBehaviour
{
    [SerializeField] private LayerMask trashLayerMask;
    [SerializeField] private List<TrashType> acceptedTrashTypes = new List<TrashType>();
    [SerializeField] private GameObject scoreParticles;
    
    private GameManager _gm;

    private void Start()
    {
        _gm = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & trashLayerMask) != 0)
        {
            TrashBehaviour trashBehaviour = other.GetComponent<TrashBehaviour>();
            if (trashBehaviour != null)
            {
                gameObject.transform.position = other.transform.position;
                TrashType trashType = trashBehaviour.TrashType;
                trashBehaviour.IsScored = true;
                ProcessTrash(trashType, other.gameObject);
            }
        }
    }
    
    private void ProcessTrash(TrashType trashType, GameObject trashObject)
    {
        if (acceptedTrashTypes.Contains(trashType))
        {
            GameObject gameObject = InstanceManager.Instance.GetObject(scoreParticles);
            EventManager.TriggerEvent("Score+");
        }
        else
        {
            EventManager.TriggerEvent("Score-");
        }
    }
    
}
