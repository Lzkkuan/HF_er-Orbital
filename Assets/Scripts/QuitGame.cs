using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
        Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
