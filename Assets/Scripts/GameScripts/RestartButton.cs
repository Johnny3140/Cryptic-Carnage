using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        // Reload the main game scene
        SceneManager.LoadScene("MoveLook");
    }
}
