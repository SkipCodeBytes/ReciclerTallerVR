using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshSineWave : MonoBehaviour
{
    public SineWave3D sineWave3D;

    private Mesh _mesh;
    private Vector3[] _baseVertices;
    private Vector3[] _displacedVertices;

    void Start()
    {
        _mesh = Instantiate(GetComponent<MeshFilter>().sharedMesh);
        GetComponent<MeshFilter>().mesh = _mesh;

        _baseVertices = _mesh.vertices;
        _displacedVertices = new Vector3[_baseVertices.Length];
    }

    void Update()
    {
        if (!sineWave3D) return;

        for (int i = 0; i < _baseVertices.Length; i++)
        {
            Vector3 local = _baseVertices[i];
            Vector3 worldPos = transform.TransformPoint(local);
            float newHeight = sineWave3D.GetHeightByPosition(worldPos);

            local.y = newHeight - transform.position.y; // vuelve a espacio local
            _displacedVertices[i] = local;
        }

        _mesh.vertices = _displacedVertices;
        _mesh.RecalculateNormals();
    }
}
