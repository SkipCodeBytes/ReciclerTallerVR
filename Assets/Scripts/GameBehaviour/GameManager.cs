using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    

    [Header("Init Panel")]
    [SerializeField] private GameObject initPanel;
    [SerializeField] private GameObject pokeObject;
    [SerializeField] private TextMeshProUGUI txtInfo;
    [SerializeField] private TextMeshProUGUI txtInitCount;
    [SerializeField] private float timeToInit = 5f;
    [SerializeField] private bool playingGame = false;
    [SerializeField] private AudioClip countDownSound;
    [SerializeField] private AudioClip startSound;

    [Header("Game Stats")]
    [SerializeField] private TextMeshPro txtPoints;
    [SerializeField] private TextMeshPro txtTime;
    [SerializeField] private float roundDuration = 180.0f;

    [Header("References")]
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private TrashSpawner trashSpawner;
    [SerializeField] private PasserbySpawner passerbySpawnerA;
    [SerializeField] private PasserbySpawner passerbySpawnerB;
    [SerializeField] private ComentarySystem comentarySystem;

    private float gameTimer;
    private int playerScore = 0;

    [SerializeField] private GameObject truck;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        EventManager.StartListening("Score+", () => 
        {
            playerScore += 2;
            txtPoints.text = "Puntos: " + (playerScore * 10);

        });
        EventManager.StartListening("Score-", () => 
        { 
            playerScore--;
            txtPoints.text = "Puntos: " + (playerScore * 10);
        });

        //EventManager.StartListening("InitGame",() => InitGame());
        initPanel.SetActive(true);
        pokeObject.SetActive(true);
        txtInfo.gameObject.SetActive(true);
        txtInitCount.gameObject.SetActive(false);

        txtPoints.gameObject.SetActive(false);
        txtTime.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playingGame)
        {
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
                int minutes = (int)(gameTimer / 60);
                int seconds = (int)(gameTimer % 60);
                txtTime.text = "Tiempo: " + minutes + ":" + seconds.ToString("00");
            }
            else
            {
                playingGame = false;
                EndGame();
            }
        }
        
    }

    public void StartGame()
    {
        txtInfo.gameObject.SetActive(false);
        pokeObject.SetActive(false);
        txtInitCount.gameObject.SetActive(true);

        trashSpawner.IsActive = true;
        passerbySpawnerA.IsActive = true;
        passerbySpawnerB.IsActive = true;
        StartCoroutine(CountdownRoutine());
    }
    
    private IEnumerator CountdownRoutine()
    {
        float countdown = timeToInit;
        
        while (countdown > 0)
        {
            txtInitCount.text = "Empezando en ... " + Mathf.CeilToInt(countdown);
            SoundController.Instance.PlaySound(countDownSound);
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }
        
        txtInitCount.text = "¡Vamos!";
        SoundController.Instance.PlaySound(startSound);
        yield return new WaitForSeconds(0.5f);
        
        InitGame();
    }

    private void InitGame()
    {
        initPanel.SetActive(false);
        txtPoints.gameObject.SetActive(true);
        txtTime.gameObject.SetActive(true);

        comentarySystem.IsActive = true;

        gameMusic.Play();
        gameTimer = roundDuration;
        playerScore = 0;
        playingGame = true;
    }

    public void EndGame()
    {
        trashSpawner.IsActive = false;
        passerbySpawnerA.IsActive = false;
        passerbySpawnerB.IsActive = false;
        comentarySystem.IsActive = false;
        
        truck.SetActive(true);
    }

    public void RestartGame()
    {
        
        initPanel.SetActive(true);
        pokeObject.SetActive(true);
        txtInfo.gameObject.SetActive(true);
        txtInitCount.gameObject.SetActive(false);
        txtPoints.gameObject.SetActive(false);
        txtTime.gameObject.SetActive(false);

        txtInfo.text = "Tu puntuación es: " + (playerScore * 10) + "\n¿Volver a jugar?";
        playingGame = false;

    }
}
