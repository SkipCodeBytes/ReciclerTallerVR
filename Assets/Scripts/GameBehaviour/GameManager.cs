using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    public int PlayerScore = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        EventManager.StartListening("Score+", () => PlayerScore++);
        EventManager.StartListening("Score-", () => PlayerScore--);
    }

}
