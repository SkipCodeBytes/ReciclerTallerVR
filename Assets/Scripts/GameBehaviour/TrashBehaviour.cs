using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    public float maxYPos = -5;

    void Update()
    {
        if(maxYPos > transform.position.y) gameObject.SetActive(false);
    }
}
