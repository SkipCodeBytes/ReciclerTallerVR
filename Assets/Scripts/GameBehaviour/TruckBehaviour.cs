using UnityEngine;
using System.Collections.Generic;

public class TruckBehaviour : MonoBehaviour
{
    [SerializeField] private List<CollectArea> collectAreas = new List<CollectArea>();
    [SerializeField] private Vector2 collectTimeRange = new Vector2(1f, 2f);

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CollectTrash()
    {
        foreach (CollectArea area in collectAreas)
        {
            if (area != null && area.TrashBehaviours != null)
            {
                for (int i = 0; i < area.TrashBehaviours.Count; i++)
                {
                    StartCoroutine(CinematicAnimation.ParabolicMotion(
                        area.TrashBehaviours[i].transform, transform.position, Random.Range(collectTimeRange.x, collectTimeRange.y),
                        () => area.TrashBehaviours[i].gameObject.SetActive(false)));
                }
                
                area.TrashBehaviours.Clear();
            }
        }

        StartCoroutine(CinematicAnimation.WaitTime(collectTimeRange.y + 0.5f, () =>
        {
            animator.Play("EndTruck");
        }));
    }
}
