using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TopDown
{
    public class PowerUpsManager : MonoBehaviour
    {

        public static PowerUpsManager instance;
        private void Awake()
        {
            instance = this;
        }

        public GameObject powerUpCanvas;
        public PowerUpScriptableObject[] powerUpsArray;
        public CardScript[] cards;

        public Player targetPlayer;


        List<PowerUpScriptableObject> powerUpsList = new List<PowerUpScriptableObject>();


        [ContextMenu("Generate Powerups")]
        public void GeneratePowerUps()
        {
            powerUpsList = powerUpsArray.ToList();
            foreach (var card in cards)
            {
                int rand = UnityEngine.Random.Range(0, powerUpsList.Count);
                bool isRare = UnityEngine.Random.Range(0f, 1000f) > 900f;
                var powerUp = powerUpsList[rand];
                card.SetupCard(powerUp.mainText, isRare ? powerUp.rareDetailsText : powerUp.detailText, powerUp.sprite, isRare, GetCallback(powerUp.powerupType, isRare));
                powerUpsList.RemoveAt(rand);
            }
            powerUpCanvas.gameObject.SetActive(true);
            GamePause.instance.PauseGame(false);
        }

        public System.Action GetCallback(PowerUps powerUp, bool isRare)
        {
            switch (powerUp)
            {
                case PowerUps.AttackDamange:
                    return null;

                case PowerUps.AttackSpeed:
                    if (isRare)
                    {
                        return () => { targetPlayer.hController.attackCD = 0.2f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.hController.attackCD = 0.4f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                case PowerUps.Heart:
                    if (isRare)
                    {
                        return () => { targetPlayer.GetComponent<PlayerHealth>().SetMaxHealth(4000f); GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.GetComponent<PlayerHealth>().SetMaxHealth(2000f); GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                case PowerUps.MoveSpeed:
                    if (isRare)
                    {
                        return () => { targetPlayer.hController.playerSpeed = 12f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.hController.playerSpeed = 8f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }

                default:
                    return null;
            }
        }




    }
}
