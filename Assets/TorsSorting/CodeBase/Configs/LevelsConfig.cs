using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "new LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private int[] _levelsInRank;

        public int GetDifficulty(int levelIndex)
        {
            if (levelIndex == 0)
                return 0;
            
            int rank = 0;
            while (levelIndex < _levelsInRank.Length)
            {
                if (rank >= _levelsInRank.Length)
                    break;
                
                levelIndex -= _levelsInRank[rank];
                rank++;
            }

            return rank;
        }
    }
}