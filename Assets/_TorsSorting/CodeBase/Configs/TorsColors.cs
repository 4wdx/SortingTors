using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "new TorsColors", menuName = "Configs/TorsColors")]
    public class TorsColors : ScriptableObject
    {
        [field: SerializeField] public Color[] Colors { get; private set; }        
    }
}