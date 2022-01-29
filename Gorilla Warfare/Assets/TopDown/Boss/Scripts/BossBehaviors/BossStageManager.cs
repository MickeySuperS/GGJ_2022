using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(fileName = "StageManager", menuName = "TopDown/BossManager")]
    public class BossStageManager : ScriptableObject
    {
        public BossStage[] stages;
        public int currentStageIndex;
    }
}
