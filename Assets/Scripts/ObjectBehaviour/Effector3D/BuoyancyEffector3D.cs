using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(TiggerStayObjects))]
[DisallowMultipleComponent]
public class BuoyancyEffector3D : MonoBehaviour
{
    [Header("Wave Reference (optional)")]
    [Tooltip("Si se asigna, se usa la altura dinámica de la ola. Si no, usa la altura local.")]
    public SineWave3D sineWave3D;

    [Header("Buoyancy Settings")]
    [Tooltip("Fuerza de flotación base aplicada al objeto (intensidad del empuje).")]
    public float floatStrength = 10f;

    [Tooltip("Amortiguación del movimiento vertical (0 = sin fricción, 1 = totalmente suave).")]
    [Range(0f, 1f)]
    public float damping = 0.1f;

    [Tooltip("Altura relativa donde se considera el nivel del agua dentro del volumen.")]
    public float floatLevel = 0f;

    [Header("Gizmos")]
    [SerializeField] private Color gizmoLevelColor = new Color(0.2f, 0.6f, 1f, 0.6f);
    [SerializeField] private bool drawLevelLine = true;

    private BoxCollider boxCollider;
    private TiggerStayObjects stayObjects;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        stayObjects = GetComponent<TiggerStayObjects>();

        if (boxCollider != null)
            boxCollider.isTrigger = true;
    }

    private void FixedUpdate()
    {
        if (stayObjects == null || stayObjects.ObjectsHash.Count == 0)
            return;

        // Precalcular algunos valores
        Vector3 up = Vector3.up;
        float waterBaseHeight = transform.position.y + floatLevel;

        foreach (Rigidbody rb in stayObjects.ObjectsHash)
        {
            if (rb == null) continue;
            if (rb.isKinematic) continue;

            Vector3 pos = rb.worldCenterOfMass;

            // Altura actual del agua (puede venir de SineWave3D o ser plana)
            float waterHeight = sineWave3D ? sineWave3D.GetHeightByPosition(pos) + floatLevel : 
                waterBaseHeight;

            float difference = waterHeight - pos.y;
            if (difference > 0f)
            {
                // Empuje proporcional a la inmersión
                Vector3 uplift = up * (difference * floatStrength);
                rb.AddForce(uplift, ForceMode.Acceleration);

                // Amortiguación solo en el eje vertical
                Vector3 vel = rb.linearVelocity;
                vel.y *= (1f - damping);
                rb.linearVelocity = vel;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawLevelLine || boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null) return;

        // Dibujar línea del nivel de flotabilidad
        Gizmos.color = gizmoLevelColor;

        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = matrix;

        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;

        float levelY = center.y + floatLevel;

        Vector3 p1 = new Vector3(center.x - size.x / 2f, levelY, center.z - size.z / 2f);
        Vector3 p2 = new Vector3(center.x + size.x / 2f, levelY, center.z - size.z / 2f);
        Vector3 p3 = new Vector3(center.x + size.x / 2f, levelY, center.z + size.z / 2f);
        Vector3 p4 = new Vector3(center.x - size.x / 2f, levelY, center.z + size.z / 2f);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
