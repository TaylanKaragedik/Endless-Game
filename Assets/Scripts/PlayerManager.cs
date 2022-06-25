using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;
    public Text coinsText;
    public Text scoreText;
    public Text highScoreText;
    public Text gop_ScoreText;
    float startTime;
    float elapsedTime;
    float score;


 
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        gameOver = false;
        Time.timeScale = 1;
        PlayerController.forwardSpeed = 1.3f;
        PlayerController.jumpForce = 8;
        isGameStarted = false;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameOver)
        {
            if (score > PlayerPrefs.GetFloat("_highScore"))
            {
                PlayerPrefs.SetFloat("_highScore", score);
            }

            highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("_highScore").ToString("0");
            gop_ScoreText.text = "Score: " + (score).ToString("0");

            Time.timeScale = 0;
            TileManager.destroyCount = 0;
            PlayerController.forwardSpeed = 0;
            PlayerController.jumpForce = -100;
            gameOverPanel.SetActive(true);

           
        }

        coinsText.text = "Energy: " + numberOfCoins;

        if (isGameStarted == true)
        {
            elapsedTime = Time.time - startTime;

            if (PlayerController.forwardSpeed != 0)
            {
                score = PlayerController.forwardSpeed * elapsedTime * 2;
                scoreText.text = "Score: " + (score).ToString("0");
            }  
        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText); 
        }
    }
}
