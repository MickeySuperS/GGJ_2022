using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace TopDown
{
    public class WinLoseScreen : MonoBehaviour
    {
        public static WinLoseScreen instace;

        private void Awake()
        {
            instace = this;
        }


        public bool gameEnded;

        public string winText = "YOU WON";
        public string lostText = "YOU LOST";


        public TextMeshProUGUI winLostText;
        public GameObject endGameCanvas;

        bool playerWon = false;
        public void SetWin(bool value)
        {
            playerWon = value;
            winLostText.text = playerWon ? winText : lostText;
        }

        public void EndGame()
        {
            endGameCanvas.gameObject.SetActive(true);
            gameEnded = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public GamePause gamePause;
        public void GotoMenu()
        {
            gamePause.ResumeGame();
            SceneManager.LoadScene(0);
        }
    }
}
