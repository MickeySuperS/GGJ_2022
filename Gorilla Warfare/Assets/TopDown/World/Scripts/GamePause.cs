using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GamePause : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameIsPaused;

    public GameObject PauseUI;

    public AudioClip pauseAudio;
    public AudioSource worldAudioSource;
    public AudioMixerSnapshot pausedSnapshot, unPausedSnapshot;

    void Start()
    {
        gameIsPaused = false;
        ResumeGame();
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
        worldAudioSource.PlayOneShot(pauseAudio);
        pausedSnapshot.TransitionTo(0.1f);
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        unPausedSnapshot.TransitionTo(0f);
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        gameIsPaused = false;
    }
}
