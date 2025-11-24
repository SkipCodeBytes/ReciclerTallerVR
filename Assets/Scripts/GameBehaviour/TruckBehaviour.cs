using UnityEngine;
using System.Collections.Generic;

public class TruckBehaviour : MonoBehaviour
{
    //[SerializeField] private List<CollectArea> collectAreas = new List<CollectArea>();

    [SerializeField] private CollectArea collectAreaA;
    [SerializeField] private CollectArea collectAreaB;
    [SerializeField] private float collectTime = 0.5f;
    [SerializeField] private float collectDuration = 1f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CollectTrash()
    {
        float time = 0;

        for (int i = 0; i < collectAreaA.TrashBehaviours.Count; i++)
        {
            StartCoroutine(CinematicAnimation.WaitTime(time, () =>
            {
                StartCoroutine(CinematicAnimation.ParabolicMotion(
                    collectAreaA.TrashBehaviours[i].transform, transform.position, collectDuration,
                    () => collectAreaA.TrashBehaviours[i].gameObject.SetActive(false)));
                time += collectTime;
            }));

        }
        collectAreaA.TrashBehaviours.Clear();

        for (int i = 0; i < collectAreaB.TrashBehaviours.Count; i++)
        {
            StartCoroutine(CinematicAnimation.WaitTime(time, () =>
            {
                StartCoroutine(CinematicAnimation.ParabolicMotion(
                collectAreaB.TrashBehaviours[i].transform, transform.position, collectDuration,
                () => collectAreaB.TrashBehaviours[i].gameObject.SetActive(false)));
                time += collectTime;
            }));
        }
        collectAreaB.TrashBehaviours.Clear();

        StartCoroutine(CinematicAnimation.WaitTime(time, () => { animator.Play("EndTruck"); }));
    }
}
