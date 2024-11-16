using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "new LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private int _levelsInRank;

        public int GetDifficulty(int levelIndex)
        {
            if (levelIndex == 0)
                return 0;
            
            int rank = 0;
            rank = (int)(levelIndex / _levelsInRank);
            rank = Mathf.Clamp(rank, 1, 7);
            return rank;
        }
    }
}