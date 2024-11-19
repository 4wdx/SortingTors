using System;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "new TorsColors", menuName = "Configs/TorsColors")]
    public class TorsColors : ScriptableObject
    {
        [SerializeField] private Color[] _colors;

        public Color GetColor(int index)
        {
            if (index < 1 || index >= _colors.Length)
                throw new Exception("Invalid Index for color: " + index + ". Index should be 1-" + _colors.Length);
            
            return _colors[index - 1];
        } 
    }
}