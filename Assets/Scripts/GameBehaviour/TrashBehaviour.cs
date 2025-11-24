using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    [SerializeField] private TrashType trashType;
    [SerializeField] private float maxYPos = -5f;
    [SerializeField] private string defaultTag = "Untagged";
    [SerializeField] private string collectedTag = "CollectedTrash";
    
    private bool isScored = false;
    private Vector3 _normalScale = Vector3.one;

    public TrashType TrashType { get => trashType; }
    public bool IsScored { get => isScored; set => isScored = value; }

    private void OnEnable()
    {
        SetDefaultTag();
    }

    void Update()
    {
        if(maxYPos > transform.position.y) gameObject.SetActive(false);
    }

    public void SetDefaultTag()
    {
        gameObject.tag = defaultTag;
        isScored = false;
    }

    public void SetCollectedTag()
    {
        gameObject.tag = collectedTag;
    }
}
