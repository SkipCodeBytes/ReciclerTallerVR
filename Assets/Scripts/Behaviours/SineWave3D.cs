using UnityEngine;
[ExecuteAlways]
public class SineWave3D : MonoBehaviour
{
    [Header("Wave Settings")]
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float speed = 1f;
    public float baseHeight = 0f;

    [Header("Gizmos")]
    [SerializeField] private bool showGrid = false;
    [SerializeField] private Color gizmoColor = Color.cyan;
    [SerializeField] private float gizmoSize = 10f;
    [SerializeField] private int gizmoResolution = 50;

    public static SineWave3D Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>Devuelve la altura del agua en una posición global.</summary>
    public float GetHeightByPosition(Vector3 position)
    {
        float x = position.x + transform.position.x;
        float z = position.z + transform.position.z;
        float time = Application.isPlaying ? Time.time : 0f;

        float wave =
            Mathf.Sin(x * frequency + time * speed) +
            Mathf.Cos(z * frequency + time * speed);

        return baseHeight + amplitude * wave * 0.5f + transform.position.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        if (!showGrid)
        {
            // Línea de perfil 2D
            Vector3 prev = transform.position;
            for (int i = 0; i <= gizmoResolution; i++)
            {
                float t = i / (float)gizmoResolution;
                Vector3 pos = transform.position + Vector3.right * (t * gizmoSize - gizmoSize / 2f);
                pos.y = GetHeightByPosition(pos);
                if (i > 0) Gizmos.DrawLine(prev, pos);
                prev = pos;
            }
        }
        else
        {
            // Cuadrícula 3D
            int grid = 10;
            float step = gizmoSize / grid;
            Vector3 origin = transform.position - new Vector3(gizmoSize / 2f, 0, gizmoSize / 2f);

            for (int x = 0; x <= grid; x++)
            {
                for (int z = 0; z <= grid; z++)
                {
                    Vector3 pos = origin + new Vector3(x * step, 0, z * step);
                    pos.y = GetHeightByPosition(pos);
                    Gizmos.DrawSphere(pos, 0.05f);
                }
            }
        }
    }
}