using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDown
{
    [CreateAssetMenu(menuName = "TopDown/Powerup", fileName = "powerup")]
    public class PowerUpScriptableObject : ScriptableObject
    {
        public Sprite sprite;
        public string mainText;
        public string detailText;
        public string rareDetailsText;
        public PowerUps powerupType;
    }

    public enum PowerUps
    {
        AttackDamange,
        AttackSpeed,
        Heart,
        MoveSpeed,
        Shield,
        Random
    }
}
