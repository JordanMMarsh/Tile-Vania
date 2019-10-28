using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int startingPlayerLives = 3;
    [SerializeField] Text playerLivesText;
    int playerLives;

    private void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        if (!playerLivesText)
        {
            playerLivesText = FindObjectOfType<Text>();
        }
    }

    private void Start()
    {
        playerLives = startingPlayerLives;
        playerLivesText.text = playerLives.ToString();
    }

    public void KillPlayer()
    {
        if (playerLives > 0)
        {
            playerLives--;
            playerLivesText.text = playerLives.ToString();
        }
        else
        {
            FindObjectOfType<SceneLoader>().GameOver();
            Destroy(gameObject);
        }

    }
}
