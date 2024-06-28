using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pMenu;
    public static bool paused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                ResumeGame();
            } else {
                PauseGame();
            }            
        }
    }
    public void PauseGame() {
        pMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void ResumeGame() {
        pMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    public void RestartGame() {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Main");
        paused = false;
    }
    public void MainMenu() {
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameMenuScene");
        paused = false;
    }
    public void QuitGame() { 
         Application.Quit(); //only works when you build the game! -huy
    }
}
