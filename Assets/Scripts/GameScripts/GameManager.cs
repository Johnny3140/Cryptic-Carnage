using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentRound = 0;
    public int score = 0;

    public Text roundText;
    public Text scoreText;

    public delegate void RoundStartAction();
    public static event RoundStartAction OnRoundStart;

    public ZombieSpawner zombieSpawner; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartNewRound();
    }

    void Update()
{
    // Check conditions to advance to the next round 
    if (AllZombiesDead())
    {
        StartNewRound();
    }

    // Update UI elements
    UpdateUI();
}

bool AllZombiesDead()
{
    // Check if there are no more zombies in the scene
    return GameObject.FindGameObjectsWithTag("Zombie").Length == 0;
}

    void StartNewRound()
    {
        currentRound++;

        if (OnRoundStart != null)
        {
            OnRoundStart();
        }

        // Update UI
        UpdateUI();
    }

    public void IncreaseScore(int points)
    {
        score += points;

        // Update UI
        UpdateUI();
    }

    void UpdateUI()
    {
        if (roundText != null)
        {
            roundText.text = "Round: " + currentRound;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
