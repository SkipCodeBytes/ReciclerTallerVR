using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public enum CommentaryType
{
    Good,
    Bad,
    Reaction
}

public class ComentarySystem : MonoBehaviour
{
    [Header("Rotation Speed")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 50, 0);

    [Header("World comentary")]
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private List<GameObject> textPool = new List<GameObject>();
    [SerializeField] private Vector3 initPosition = new Vector3(0, 0, 3f);

    [SerializeField] private Vector2 YPositionRange = new Vector2(-1f,1f);
    [SerializeField] private Vector2 textSizeRange = new Vector2(2f, 3f);
    [SerializeField] private Vector2 reactionSpawnRange = new Vector2(1f, 6f);
    [SerializeField] private float duration = 6f;

    [SerializeField] private Color goodColor = Color.green;
    [SerializeField] private Color badColor = Color.red;
    [SerializeField] private Color reactionColor = Color.yellow;


    [Header("Comentary Bank")]
    [SerializeField] private List<string> goodComentaries = new List<string> 
    { "¡Genial!", "¡Sigue así!", "¡Excelente!", "¡Muy bien!", "¡Perfecto!", "¡Increíble!", "¡Fantástico!", "¡Bien hecho!", "¡Así se hace!", "¡Qué pro!" };

    [SerializeField]
    private List<string> badComentaries = new List<string>
    { "Creo que eso no va ahí", "¡Cuidado!", "Mmm, no creo", "¡Ups!", "Inténtalo de nuevo", "No es el lugar correcto", "¡Oye, espera!", "Mejor revisa eso", "¡No tan rápido!", "Piénsalo mejor" };

    [SerializeField]
    private List<string> reactionComentaries = new List<string>
    { "¡No te rindas!", "¡Qué palta!", "¡Vamos!", "¡Tú puedes!", "¡Dale que va!", "¡Interesante!", "¡Wow!", "¡Guau!", "¡Impresionante!", "¡Qué locura!" };

    private float reactionTimer = 0f;
    
    void Start()
    {
        InitializePool();
        reactionTimer = Random.Range(reactionSpawnRange.x, reactionSpawnRange.y);
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
        
        // Timer para comentarios de reacción automáticos
        reactionTimer -= Time.deltaTime;
        if (reactionTimer <= 0f)
        {
            ShowReactionCommentary();
            reactionTimer = Random.Range(reactionSpawnRange.x, reactionSpawnRange.y);
        }
    }
    

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject textObj = Instantiate(textPrefab, transform);
            textObj.SetActive(false);
            textPool.Add(textObj);
        }
    }

    public void ShowCommentary(string text, CommentaryType type)
    {
        GameObject textObj = GetPooledObject();
        if (textObj == null) return;

        // Posición mundial (no afectada por la rotación del padre) con variación en Y
        Vector3 worldPosition = transform.position + initPosition;
        worldPosition.y += Random.Range(YPositionRange.x, YPositionRange.y);
        textObj.transform.position = worldPosition;
        
        // Hacer que el texto mire hacia el centro (posición de este objeto) con 180° adicionales
        Vector3 direction = transform.position - textObj.transform.position;
        if (direction != Vector3.zero)
        {
            textObj.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
        }

        Color txtColor = Color.white;
        textObj.SetActive(true);

        // Configurar texto
        TextMeshPro textMesh = textObj.GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.fontSize = Random.Range(textSizeRange.x, textSizeRange.y);


            // Asignar color según tipo
            switch (type)
            {
                case CommentaryType.Good:
                    txtColor = goodColor;
                    txtColor.a = 0f;
                    StartCoroutine(LerpUtils.LerpColor(color => textMesh.color = color, txtColor, goodColor, 0.5f));
                    break;
                case CommentaryType.Bad:
                    txtColor = badColor;
                    txtColor.a = 0f;
                    StartCoroutine(LerpUtils.LerpColor(color => textMesh.color = color, txtColor, badColor, 0.5f));
                    break;
                case CommentaryType.Reaction:
                    txtColor = reactionColor;
                    txtColor.a = 0f;
                    StartCoroutine(LerpUtils.LerpColor(color => textMesh.color = color, txtColor, reactionColor, 0.5f));
                    break;
            }
        }

        StartCoroutine(CinematicAnimation.WaitTime(duration, () =>
        {
            StartCoroutine(LerpUtils.LerpColor(color => textMesh.color = color, reactionColor, txtColor, 0.5f,
                ()=> textObj.SetActive(false)));
        }));


    }

    private GameObject GetPooledObject()
    {
        foreach (GameObject obj in textPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }



    // Métodos públicos para usar desde otros scripts
    public void ShowGoodCommentary() 
    {
        if (goodComentaries.Count > 0)
        {
            string randomText = goodComentaries[Random.Range(0, goodComentaries.Count)];
            ShowCommentary(randomText, CommentaryType.Good);
        }
    }
    
    public void ShowBadCommentary() 
    {
        if (badComentaries.Count > 0)
        {
            string randomText = badComentaries[Random.Range(0, badComentaries.Count)];
            ShowCommentary(randomText, CommentaryType.Bad);
        }
    }
    
    public void ShowReactionCommentary() 
    {
        if (reactionComentaries.Count > 0)
        {
            string randomText = reactionComentaries[Random.Range(0, reactionComentaries.Count)];
            ShowCommentary(randomText, CommentaryType.Reaction);
        }
    }
    
    // Métodos con texto personalizado (mantener compatibilidad)
    public void ShowGoodCommentary(string text) => ShowCommentary(text, CommentaryType.Good);
    public void ShowBadCommentary(string text) => ShowCommentary(text, CommentaryType.Bad);
    public void ShowReactionCommentary(string text) => ShowCommentary(text, CommentaryType.Reaction);
}


