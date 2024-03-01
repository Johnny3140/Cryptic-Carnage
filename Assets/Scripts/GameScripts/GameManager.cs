using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Add this line

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern
    public Text scoreText;
    public Text roundText;
    
    private int score = 0;
    private int currentRound = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateUI();
        StartCoroutine(StartNewRound());
    }

    IEnumerator StartNewRound()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Adjust the delay between rounds as needed
            currentRound++;
            UpdateUI();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        roundText.text = "Round: " + currentRound;
    }
}
