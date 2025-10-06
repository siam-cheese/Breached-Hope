using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public KeyCode menuKey = KeyCode.Escape;

    public GameObject pauseMenu;
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(menuKey)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void UnpauseGame () {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void RestartGame() {
        Scene curScene = SceneManager.GetActiveScene();
        string sceneName = curScene.name;
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
