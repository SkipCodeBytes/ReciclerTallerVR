using System.Collections;
using UnityEngine;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleDespawn : MonoBehaviour
{
    private ParticleSystem ps;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        StartCoroutine(WaitToDeactivate());
    }

    private IEnumerator WaitToDeactivate()
    {
        yield return new WaitWhile(() => ps.IsAlive(true));
        gameObject.SetActive(false);
    }
}
