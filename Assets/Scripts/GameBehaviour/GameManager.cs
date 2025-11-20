using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    [SerializeField] private int PlayerScore = 0;
    [SerializeField] private float gameTimer = 180.0f;
    [SerializeField] private TextMeshPro txtPoints;
    [SerializeField] private TextMeshPro txtTime;
    [SerializeField] private bool initGame = false;

    [SerializeField] private GameObject truck;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        EventManager.StartListening("Score+", () => 
        {
            PlayerScore += 2;
            txtPoints.text = "Puntos: " + (PlayerScore * 10);

        });
        EventManager.StartListening("Score-", () => 
        { 
            PlayerScore--;
            txtPoints.text = "Puntos: " + (PlayerScore * 10);
        });

        EventManager.StartListening("InitGame",() => InitGame());
    }

    private void Update(){
        if(initGame){

	    if(gameTimer > 0){
	    	gameTimer -= Time.deltaTime;
	    	txtTime.text = "Tiempo: " + (int)(gameTimer / 60) + ":" + (int) (gameTimer % 60);
	    } else {
      		initGame = false;
		truck.SetActive(true);
	     }
	}
    }

    private void InitGame(){
    	initGame = true;
	//Iniciamos música
	//Comienza el recorrido
	//Inicia el contador
    }

}
