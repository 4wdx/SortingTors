using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Utils
{
    public class Updater : MonoBehaviour
    {
        private List<ITickable> _tickables = new();
        
        public void Add(ITickable tickable) => 
            _tickables.Add(tickable);

        public void Remove(ITickable tickable) => 
            _tickables.Remove(tickable);

        private void Update()
        {
            foreach (ITickable tickable in _tickables)
            {
                tickable.Tick(Time.deltaTime);
            }
        }
    }
}