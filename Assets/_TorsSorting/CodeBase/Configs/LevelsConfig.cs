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
            while (rank * _levelsInRank <= levelIndex)
            {
                rank++;
                if (rank >= 7)
                    return 7;
            }
            
            return rank;
        }
    }
}