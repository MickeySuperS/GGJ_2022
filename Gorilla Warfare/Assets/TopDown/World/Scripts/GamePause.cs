using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameIsPaused;

    public GameObject PauseUI;

    void Start()
    {
        gameIsPaused = false;
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if (gameIsPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        gameIsPaused = false;
    }
}
