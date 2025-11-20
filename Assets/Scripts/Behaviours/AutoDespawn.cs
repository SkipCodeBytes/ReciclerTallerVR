using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    [SerializeField] private float despawnTime = 5f;
    
    private float timer;
    
    void OnEnable()
    {
        timer = despawnTime;
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
