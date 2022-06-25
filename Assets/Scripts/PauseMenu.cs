using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    float  tempForwardSpeed;
    float tempDirectionY;

    public  GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerController.forwardSpeed = tempForwardSpeed;

        PlayerController.gravity = -4;
        PlayerController.jumpForce = 8;
        PlayerController.direction.y = tempDirectionY;

        
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        tempForwardSpeed = PlayerController.forwardSpeed;
        PlayerController.forwardSpeed = 0;
        
        PlayerController.gravity = 0;
        PlayerController.jumpForce = 0;
        tempDirectionY = PlayerController.direction.y;
        PlayerController.direction.y = 0;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Resume();
    }
}
