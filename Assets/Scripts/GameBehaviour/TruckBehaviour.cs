using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using TMPro;

public class TruckBehaviour : MonoBehaviour
{
    [SerializeField] private CollectArea collectAreaA;
    [SerializeField] private CollectArea collectAreaB;
    [SerializeField] private float collectTime = 0.5f;
    [SerializeField] private float collectDuration = 1f;

    [SerializeField] private AudioClip truckSiren;
    [SerializeField] private AudioClip truckMusic;
    [SerializeField] private AudioClip truckCollect;

    [SerializeField] private TextMeshPro txtBonus;

    [SerializeField] private Vector3 CollectPositionWorldSpace;

    [SerializeField] private List<GameObject> dancingWorkers;
    [SerializeField] private GameObject particles_1;
    [SerializeField] private GameObject particles_2;

    private Animator animator;
    private AudioSource audioSource;
    private Animator txtBonusAnimator;
    private bool _isCollecting = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        txtBonusAnimator = txtBonus.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(truckSiren);
        animator.Play("BeginTruck");
        StartCoroutine(CinematicAnimation.WaitTime(1.5f, () => audioSource.PlayOneShot(truckMusic)));
    }

    public void CollectTrash()
    {
        if (_isCollecting) return;
        _isCollecting = true;

        foreach(GameObject dancingWorker in dancingWorkers)
        {
            dancingWorker.SetActive(true);
            GameObject particle = InstanceManager.Instance.GetObject(particles_1);
            particle.transform.position = dancingWorker.transform.position;
            particle = InstanceManager.Instance.GetObject(particles_2);
            particle.transform.position = dancingWorker.transform.position;
        }

        float time = 0;
        int bonus = 0;

        for (int i = 0; i < collectAreaA.TrashObjects.Count; i++)
        {
            GameObject trashObj = collectAreaA.TrashObjects[i];
            if (!trashObj.activeInHierarchy) continue;
            time += collectTime;

            StartCoroutine(CinematicAnimation.WaitTime(time, () =>
            {
                StartCoroutine(CinematicAnimation.ParabolicMotion(
                    trashObj.transform, CollectPositionWorldSpace, collectDuration,
                    () => { 
                        trashObj.SetActive(false);
                        //Aqu� reproducir sonido de recolectar
                        audioSource.PlayOneShot(truckCollect);
                        animator.Play("Collect", 0, 0f);

                        GameManager.Instance.PlayerScore += 10;
                        bonus++;
                        txtBonus.text = $"Bonus x{bonus}\r\nPuntos Totales: {GameManager.Instance.PlayerScore}";
                        txtBonusAnimator.Play("Pin_", 0, 0f);
                    }));
            }));

        }
        collectAreaA.TrashObjects.Clear();

        for (int i = 0; i < collectAreaB.TrashObjects.Count; i++)
        {
            GameObject trashObj = collectAreaB.TrashObjects[i];
            if (!trashObj.activeInHierarchy) continue;
            time += collectTime;

            StartCoroutine(CinematicAnimation.WaitTime(time, () =>
            {
                StartCoroutine(CinematicAnimation.ParabolicMotion(
                trashObj.transform, CollectPositionWorldSpace, collectDuration,
                () => {
                    trashObj.SetActive(false);
                    //Aqu� reproducir sonido de recolectar
                    audioSource.PlayOneShot(truckCollect);
                    animator.Play("Collect", 0, 0f);

                    GameManager.Instance.PlayerScore += 10;
                    bonus++;
                    txtBonus.text = $"Bonus x{bonus}\r\nPuntos Totales: {GameManager.Instance.PlayerScore}";
                    txtBonusAnimator.Play("Pin_", 0, 0f);
                }));
            }));
        }
        collectAreaB.TrashObjects.Clear();

        StartCoroutine(CinematicAnimation.WaitTime(time + 3f, () => {
            _isCollecting = false;
            animator.Play("EndTruck");
            txtBonusAnimator.Play("End_", 0, 0f);
            audioSource.PlayOneShot(truckSiren);

            foreach (GameObject dancingWorker in dancingWorkers)
            {
                dancingWorker.SetActive(false);
                GameObject particle = InstanceManager.Instance.GetObject(particles_1);
                particle.transform.position = dancingWorker.transform.position;
                particle = InstanceManager.Instance.GetObject(particles_2);
                particle.transform.position = dancingWorker.transform.position;
            }
        }));
    }

    public void EndAnimation()
    {
        StartCoroutine(LerpUtils.LerpFloat(value => audioSource.volume = value, 1f, 0f, 5f,
            () => {
                audioSource.Stop();
                audioSource.volume = 1f;
                gameObject.SetActive(false);
                txtBonusAnimator.gameObject.SetActive(false);

                GameManager.Instance.RestartGame();
            }));
    }
}
