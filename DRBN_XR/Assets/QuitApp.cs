using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Si on est dans l'éditeur, on arrête juste la lecture
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Sinon on quitte l'application
        Application.Quit();
#endif
    }
}