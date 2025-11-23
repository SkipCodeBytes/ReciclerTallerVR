
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillboardEffect : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        if (target == null) return;
        
        Vector3 direction = target.position - transform.position;
        
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
