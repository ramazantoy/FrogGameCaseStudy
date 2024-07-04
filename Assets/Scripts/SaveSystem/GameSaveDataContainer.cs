using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "GameSaveData",menuName = "ScriptableObjects/GameSaveData")]
    public  class GameSaveDataContainer : SavableData
    {
        public GameSaveData Data;
        public override void Save()
        {
            Data.Save(Id);
        }

        public override void Load()
        {
            Data = SaveDataHelper.Load(Id, new GameSaveData());
        }
    
    }


    [System.Serializable]
    public class GameSaveData
    {
        private bool _isMusicOn;
        private bool _isSoundEffectsOn;


        public GameSaveData()
        {
            _isMusicOn = true;
            _isSoundEffectsOn = true;
        }
        public bool IsMusicOn
        {
            get => _isMusicOn;
            set => _isMusicOn = value;
        }
        
        public bool IsSoundEffectsOn
        {
            get => _isSoundEffectsOn;
            set => _isSoundEffectsOn= value;
        }

        
    
    
    }
}