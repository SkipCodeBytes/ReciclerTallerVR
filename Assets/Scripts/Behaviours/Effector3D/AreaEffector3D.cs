using UnityEngine;

[RequireComponent(typeof(TiggerStayObjects))]
public class AreaEffector3D : MonoBehaviour
{
    [Header("Effector Settings")]
    [SerializeField] private float forceMagnitude = 10f;
    [SerializeField] private Vector3 forceDirection = Vector3.forward;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;

    private TiggerStayObjects stayObjects;
    private Vector3 cachedWorldDir;

    private void Awake()
    {
        stayObjects = GetComponent<TiggerStayObjects>();
    }

    private void FixedUpdate()
    {
        if (stayObjects == null || stayObjects.ObjectsHash.Count == 0)
            return;

        cachedWorldDir = transform.TransformDirection(forceDirection.normalized);

        foreach (Rigidbody rb in stayObjects.ObjectsHash)
        {
            if (rb == null) continue;
            rb.AddForce(cachedWorldDir * forceMagnitude, forceMode);
        }
    }
}