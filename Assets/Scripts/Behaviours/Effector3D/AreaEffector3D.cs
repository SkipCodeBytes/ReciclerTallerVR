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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 worldDirection = transform.TransformDirection(forceDirection.normalized);
        float arrowLength = Mathf.Clamp(forceMagnitude * 0.1f, 1f, 5f);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, worldDirection * arrowLength);
        
        // Dibujar punta de flecha
        Vector3 arrowTip = transform.position + worldDirection * arrowLength;
        Vector3 right = Vector3.Cross(worldDirection, Vector3.up).normalized * 0.3f;
        Vector3 up = Vector3.Cross(right, worldDirection).normalized * 0.3f;
        
        Gizmos.DrawLine(arrowTip, arrowTip - worldDirection * 0.5f + right);
        Gizmos.DrawLine(arrowTip, arrowTip - worldDirection * 0.5f - right);
        Gizmos.DrawLine(arrowTip, arrowTip - worldDirection * 0.5f + up);
        Gizmos.DrawLine(arrowTip, arrowTip - worldDirection * 0.5f - up);
    }
#endif
}