using UnityEngine;

public class Trash_V2 : MonoBehaviour
{
    [SerializeField] private TrashType trashType;
    [SerializeField] private float maxYPos = -5f;

    void Update()
    {
        if(maxYPos > transform.position.y) 
        {
            gameObject.SetActive(false);
        }
    }
}
