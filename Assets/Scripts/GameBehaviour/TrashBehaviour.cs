using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    private float maxYPos = -5;

    void Update()
    {
        if(maxYPos < -5) gameObject.SetActive(false);
    }
}
