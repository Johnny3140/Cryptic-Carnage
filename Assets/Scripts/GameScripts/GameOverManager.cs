using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI gameOverText;

    void Start()
    {
        DisplayResults();
    }

    void DisplayResults()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        float survivalTime = PlayerPrefs.GetFloat("SurvivalTime", 0f);

        scoreText.text = "Final Score: " + finalScore;
        survivalTimeText.text = "Survival Time: " + FormatTime(survivalTime);
        
        // Display "Game Over" message
        gameOverText.text = "Game Over";
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
