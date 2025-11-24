using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GameManager gameManager = (GameManager)target;
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("Start Game", GUILayout.Height(30)))
        {
            if (Application.isPlaying)
            {
                gameManager.StartGame();
            }
            else
            {
                Debug.LogWarning("GameManager: Debes estar en Play Mode para iniciar el juego.");
            }
        }
    }
}
