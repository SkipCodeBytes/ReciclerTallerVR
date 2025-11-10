using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatyObject : MonoBehaviour
{
    public SineWave3D sineWave3D;
    public float floatOffset = 0f;
    public float floatStrength = 10f;
    public float damping = 0.05f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!sineWave3D) return;

        float waterHeight = sineWave3D.GetHeightByPosition(transform.position) + floatOffset;
        float difference = waterHeight - transform.position.y;

        if (difference > 0f)
        {
            // Empuje hacia arriba proporcional a la inmersión
            Vector3 uplift = Vector3.up * (difference * floatStrength);
            rb.AddForce(uplift, ForceMode.Acceleration);

            // Amortigua solo el movimiento vertical
            Vector3 vel = rb.linearVelocity;
            vel.y *= (1f - damping);
            rb.linearVelocity = vel;
        }
    }
}
