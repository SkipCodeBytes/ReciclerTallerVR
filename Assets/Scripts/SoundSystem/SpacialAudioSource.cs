using UnityEngine;

public class SpacialAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    private Transform followedTarget;
    private bool isFollowingTarget = false;
    private float timeRemaining;
    private bool isPlaying = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        isPlaying = false;
        isFollowingTarget = false;
        followedTarget = null;
        timeRemaining = 0f;
    }

    private void Update()
    {
        if (isPlaying)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                gameObject.SetActive(false); // Devolver al pool
            }
        }

        if (isFollowingTarget && followedTarget != null)
        {
            transform.position = followedTarget.position;
        }
    }

    public void PlayAudio(AudioClip clip, Vector3 position, float volume = 1f)
    {
        isFollowingTarget = false;
        followedTarget = null;

        transform.position = position;
        audioSource.PlayOneShot(clip, volume);
        StartTimer(clip.length);
    }

    public void PlayAudio(AudioClip clip, Transform target, bool followTarget = false, float volume = 1f)
    {
        isFollowingTarget = followTarget;
        followedTarget = target;

        transform.position = target.position;
        audioSource.PlayOneShot(clip, volume);
        StartTimer(clip.length);
    }

    private void StartTimer(float duration)
    {
        timeRemaining = duration;
        isPlaying = true;
    }
}
