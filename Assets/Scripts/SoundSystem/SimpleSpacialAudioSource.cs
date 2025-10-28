using System.Collections;
using UnityEngine;

public class SimpleSpacialAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayAudio();
    }

    public void PlayAudio()
    {
        audioSource.Play();
        StartCoroutine(DeactivateAfter(audioSource.clip.length));
    }

    private IEnumerator DeactivateAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
