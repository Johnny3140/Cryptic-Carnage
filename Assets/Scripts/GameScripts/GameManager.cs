using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private int score = 0;
    private float timer = 0f;
    private bool isPlayerAlive = true;
    public int initialScore;
    public float initialTimer;

    void Awake()
{
    if (instance == null)
        instance = this;
    else
        Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
    // Store initial values
    initialScore = 0;
    initialTimer = 0f;

    // Find TextMeshPro references
    FindTextMeshProReferences();
}


    void FindTextMeshProReferences()
{
    // Find references to scoreText and timerText
    TextMeshProUGUI[] textMeshPros = FindObjectsOfType<TextMeshProUGUI>();

    foreach (TextMeshProUGUI textMeshPro in textMeshPros)
    {
        if (textMeshPro.name.Equals("Score")) // Adjust with the actual name
        {
            scoreText = textMeshPro;
        }
        else if (textMeshPro.name.Equals("Timer")) // Adjust with the actual name
        {
            timerText = textMeshPro;
        }
    }
}


    void Start()
    {
        StartCoroutine(StartNewRound());
        StartCoroutine(UpdateTimer());
    }

    IEnumerator StartNewRound()
    {
        while (isPlayerAlive)
        {
            yield return new WaitForSeconds(5f); // Adjust the delay between rounds as needed
            score += 10; // Adjust the score increment as needed
            UpdateUI();
        }
    }

    IEnumerator UpdateTimer()
    {
        while (isPlayerAlive)
        {
            timer += 1f;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        // Check if scoreText and timerText are assigned
        if (scoreText != null && timerText != null)
        {
            scoreText.text = "Score: " + score;
            timerText.text = "Time: " + FormatTime(timer);
        }
        else
        {
            Debug.LogError("scoreText or timerText is not assigned in the GameManager.");
            // Log the state of references for debugging
            Debug.Log("scoreText: " + scoreText);
            Debug.Log("timerText: " + timerText);

            // Attempt to find references again
            FindTextMeshProReferences();
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Wait for a moment before showing the game over screen
        yield return new WaitForSeconds(0.5f);

        // Load the game over scene  
        SceneManager.LoadScene("GameOverScene");
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.SetFloat("SurvivalTime", timer);
    }

    public void RestartGame()
    {
        // Check if the UI references are not null
        if (scoreText == null || timerText == null)
        {
            Debug.LogError("scoreText or timerText is not assigned in the GameManager.");
            return;
        }

        // Reset variables to initial values
        score = initialScore;
        timer = initialTimer;
        isPlayerAlive = true;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
