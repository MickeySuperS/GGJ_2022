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
                    if (isRare)
                    {
                        return () => { targetPlayer.hController.damageValue += 30; targetPlayer.gController.damageValue += 60; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.hController.damageValue += 10; targetPlayer.gController.damageValue += 20; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }

                case PowerUps.AttackSpeed:
                    if (isRare)
                    {
                        return () => { targetPlayer.hController.attackCD -= 0.3f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.hController.attackCD -= 0.2f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                case PowerUps.Heart:
                    if (isRare)
                    {
                        return () => { targetPlayer.GetComponent<PlayerHealth>().SetMaxHealth(2500f); GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.GetComponent<PlayerHealth>().SetMaxHealth(1000f); GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                case PowerUps.MoveSpeed:
                    if (isRare)
                    {
                        return () => { targetPlayer.hController.playerSpeed += 4f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.hController.playerSpeed += 2f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }

                case PowerUps.Shield:
                    if (isRare)
                    {
                        return () => { targetPlayer.hController.damageFactor -= 0.5f; targetPlayer.gController.damageFactor -= 0.5f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }
                    else
                    {
                        return () => { targetPlayer.hController.damageFactor -= 0.25f; targetPlayer.gController.damageFactor -= 0.25f; GamePause.instance.ResumeGame(); powerUpCanvas.gameObject.SetActive(false); };
                    }


                default:
                    return null;
            }
        }




    }
}
