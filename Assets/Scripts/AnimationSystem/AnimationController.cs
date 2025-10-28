using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private List<Coroutine> activeCoroutines = new List<Coroutine>();
    
    public Coroutine StartManagedCoroutine(IEnumerator routine)
    {
        var coroutine = StartCoroutine(routine);
        activeCoroutines.Add(coroutine);
        return coroutine;
    }
    
    public void StopManagedCoroutine(Coroutine coroutine)
    {
        if (coroutine != null && activeCoroutines.Contains(coroutine))
        {
            StopCoroutine(coroutine);
            activeCoroutines.Remove(coroutine);
        }
    }
    
    public void StopAllManagedCoroutines()
    {
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        activeCoroutines.Clear();
    }
    
    private void OnDestroy()
    {
        StopAllManagedCoroutines();
    }
    
    private void OnDisable()
    {
        StopAllManagedCoroutines();
    }
}