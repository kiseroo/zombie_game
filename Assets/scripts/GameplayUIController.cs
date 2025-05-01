using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Add TextMeshPro namespace

public class GameplayUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;
    
    [SerializeField]
    private TextMeshProUGUI gameOverText; // Changed to TextMeshProUGUI

    private void Awake()
    {
        // Make sure game over panel is hidden at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        // Subscribe to player's game over event
        Player.gameOver += ShowGameOverMessage;
    }

    private void OnDisable()
    {
        // Unsubscribe from player's game over event
        Player.gameOver -= ShowGameOverMessage;
    }

    // Shows the game over message when player dies
    void ShowGameOverMessage()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (gameOverText != null)
                gameOverText.text = "GAME OVER";
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
