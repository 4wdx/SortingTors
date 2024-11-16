using System;
using CodeBase.Utils;

namespace CodeBase.Root.Services
{
    public interface IGenerationService
    {
        PinData[] Generate(int levelDifficulty);
    }

    public struct PinData
    {
        public int[] TorsColors { get; private set; }
        public bool Enabled { get; private set; }

        public void Initialize(int torsInPin)
        {
            if (Enabled == true)
                throw new Exception("Can not re-initialize PinData");
            
            TorsColors = new int[torsInPin];
            Enabled = true;
        }
    }
}