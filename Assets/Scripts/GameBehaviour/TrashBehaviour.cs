using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    [SerializeField] private TrashType trashType;
    [SerializeField] private float maxYPos = -5f;
    public bool IsScored = false;

    public TrashType TrashType { get => trashType; }

    private void OnEnable()
    {
        IsScored = false;
    }

    void Update()
    {
        if(maxYPos > transform.position.y) 
        {
            gameObject.SetActive(false);
        }
    }
}
