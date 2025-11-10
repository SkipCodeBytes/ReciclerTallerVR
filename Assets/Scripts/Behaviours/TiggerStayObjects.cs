using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class TiggerStayObjects : MonoBehaviour
{
    [SerializeField] private LayerMask affectedLayers = ~0;
    public readonly HashSet<Rigidbody> ObjectsHash = new HashSet<Rigidbody>();

    [Header("Gizmos")]
    [SerializeField] private Color gizmoColor = new Color(0f, 0.5f, 1f, 0.25f);
    [SerializeField] private bool drawGizmos = true;

    private BoxCollider box;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((affectedLayers.value & (1 << other.gameObject.layer)) == 0) return;
        var rb = other.attachedRigidbody;
        if (rb != null) ObjectsHash.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        var rb = other.attachedRigidbody;
        if (rb != null) ObjectsHash.Remove(rb);
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        if (box == null)
            box = GetComponent<BoxCollider>();

        if (box == null) return;

        Gizmos.color = gizmoColor;
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = matrix;
        Gizmos.DrawCube(box.center, box.size);
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
        Gizmos.DrawWireCube(box.center, box.size);
    }
}