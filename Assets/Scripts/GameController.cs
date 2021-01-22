using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // use to restart the game

public class GameController : MonoBehaviour {
    public GameObject gameOverPanel;

    public void ShowGameOver() {
        gameOverPanel.SetActive(true);
    }
    
    public void RestartGame() {
        //restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
}
