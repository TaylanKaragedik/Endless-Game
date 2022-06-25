using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        
        SceneManager.LoadScene("Level");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }


}
