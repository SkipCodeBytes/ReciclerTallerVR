using UnityEngine;

public class TriggerDespawnerArea : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayers) != 0)
        {
            other.gameObject.SetActive(false);
        }
    }
}
