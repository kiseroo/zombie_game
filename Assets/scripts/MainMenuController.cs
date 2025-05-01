using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
 

    public void PlayGame()
    {
      string clicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("Gameplay");

        GameManager.Instance.setClicked(clicked);
    }
}
