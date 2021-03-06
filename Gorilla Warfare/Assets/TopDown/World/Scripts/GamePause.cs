using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TopDown
{


    public class GamePause : MonoBehaviour
    {
        // Start is called before the first frame update

        public static GamePause instance;
        private void Awake()
        {
            instance = this;
        }

        public static bool gameIsPaused;

        public GameObject PauseUI;

        public AudioClip pauseAudio;
        public AudioSource worldAudioSource;
        public AudioMixerSnapshot pausedSnapshot, unPausedSnapshot;

        void Start()
        {
            gameIsPaused = false;
            pausedForPowerup = false;
            ResumeGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (!pausedForPowerup && Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = !gameIsPaused;
                if (gameIsPaused)
                    PauseGame();
                else
                    ResumeGame();
            }
        }

        public void PauseGame()
        {
            pausedSnapshot.TransitionTo(0.1f);
            PauseUI.SetActive(true);
            //worldAudioSource.PlayOneShot(pauseAudio);
            Time.timeScale = 0;
            gameIsPaused = true;
            CursorChanger.instance.ResetCursor();
        }

        bool pausedForPowerup = false;
        public void PauseGame(bool withUI = true)
        {
            pausedForPowerup = !withUI;
            if (withUI)
            {
                pausedSnapshot.TransitionTo(0.1f);
                PauseUI.SetActive(true);
            }
            //worldAudioSource.PlayOneShot(pauseAudio);
            Time.timeScale = 0;
            gameIsPaused = true;
            CursorChanger.instance.ResetCursor();
        }

        public void ResumeGame()
        {
            pausedForPowerup = false;
            unPausedSnapshot.TransitionTo(0f);
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            gameIsPaused = false;
            CursorChanger.instance.ApplyCurrentCursor();
        }

        public void ResumeGame(bool withUI = true)
        {
            pausedForPowerup = false;
            if (withUI)
            {
                unPausedSnapshot.TransitionTo(0f);
                PauseUI.SetActive(false);
            }
            Time.timeScale = 1;
            gameIsPaused = false;
            CursorChanger.instance.ApplyCurrentCursor();
        }
    }
}