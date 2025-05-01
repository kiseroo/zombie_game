using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string clicked;

    public string getInstance()
    {
        return clicked;
    }
    public void setClicked(string Clicked)
    {
        clicked = Clicked;
    }

    [SerializeField]
    public GameObject[] players;

    [SerializeField]
    private AudioClip jumpSound; // Jump sound effect

    [SerializeField]
    private AudioClip landSound; // Landing sound effect

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            GameObject player = null;

            if (clicked == "Button1")
                player = Instantiate(players[0]);
            if (clicked == "Button2")
                player = Instantiate(players[1]);

            if (player != null)
            {
                // Assign jump and landing sounds to the player
                Player playerScript = player.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.jumpSound = jumpSound;
                    playerScript.landSound = landSound;
                }
            }
        }
    }
}