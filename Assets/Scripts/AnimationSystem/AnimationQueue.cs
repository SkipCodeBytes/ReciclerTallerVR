using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnimationQueue : MonoBehaviour
{
    [SerializeField] private bool autoStart = true;
    [SerializeField] private bool isPlaying = false;
    [SerializeField] private int currentAnimationIndex = 0;
    
    private Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();
    private Coroutine currentStackCoroutine;
    
    void Start()
    {
        if (autoStart && animationQueue.Count > 0)
        {
            PlayAnimationStack();
        }
    }
    
    public void AddAnimation(IEnumerator animation)
    {
        animationQueue.Enqueue(animation);
    }
    
    public void PlayAnimationStack()
    {
        if (isPlaying) return;
        
        if (animationQueue.Count > 0)
        {
            currentStackCoroutine = StartCoroutine(PlayAnimationsSequentially());
        }
    }
    
    public void StopAnimationStack()
    {
        if (currentStackCoroutine != null)
        {
            StopCoroutine(currentStackCoroutine);
            currentStackCoroutine = null;
        }
        isPlaying = false;
        currentAnimationIndex = 0;
    }
    
    public void ClearAnimationStack()
    {
        StopAnimationStack();
        animationQueue.Clear();
    }
    
    private IEnumerator PlayAnimationsSequentially()
    {
        isPlaying = true;
        currentAnimationIndex = 0;
        
        while (animationQueue.Count > 0)
        {
            IEnumerator currentAnimation = animationQueue.Dequeue();
            yield return StartCoroutine(currentAnimation);
            currentAnimationIndex++;
        }
        
        isPlaying = false;
        currentAnimationIndex = 0;
    }
    
    public bool IsPlaying() => isPlaying;
    public int GetCurrentAnimationIndex() => currentAnimationIndex;
    public int GetRemainingAnimations() => animationQueue.Count;
}
