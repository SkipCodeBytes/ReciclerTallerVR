using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    [SerializeField] private TrashType trashType;
    [SerializeField] private float maxYPos = -5f;
    public bool IsScored = false;

    private Vector3 _normalScale = Vector3.one;

    public TrashType TrashType { get => trashType; }

    private void Awake()
    {
        _normalScale = transform.localScale;
    }

    private void OnEnable()
    {
        IsScored = false;
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        StartCoroutine(CinematicAnimation.ScaleTo(transform, _normalScale, 0.5f));
    }


    void Update()
    {
        if(maxYPos > transform.position.y) gameObject.SetActive(false);
    }
}
