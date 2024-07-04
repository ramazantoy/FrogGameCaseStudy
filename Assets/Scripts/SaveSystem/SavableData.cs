using Unity.Collections;
using UnityEngine;

namespace SaveSystem
{
    public abstract class SavableData : ScriptableObject
    { 
        [ReadOnly]
        public string Id;

  
        public void SetID()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = System.Guid.NewGuid().ToString();
            }
        }
        
        public abstract void Save();
        public abstract void Load();
    }
}