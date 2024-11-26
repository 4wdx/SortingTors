using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "new SkinData", menuName = "Configs/SkinData")]
    public class SkinData : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }
        [field: SerializeField] public Material[] Materials { get; private set; }
    }
}