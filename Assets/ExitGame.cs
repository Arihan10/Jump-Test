using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void QuitGame() {
        // Quit the game, networking shit here
        //currently it is stuck on loading screen

        SceneManager.LoadScene(0);
    }
}
