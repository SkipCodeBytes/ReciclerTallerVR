using UnityEngine;

public class TrashDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
