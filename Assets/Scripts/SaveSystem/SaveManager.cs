using System;
using System.Collections.Generic;
using Events.EventBusScripts;
using Events.GameEvents;
using Unity.VisualScripting;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private List<SavableData> _savableDatas;


        private EventBinding<OnGameStateChangedEvent> _onGameStateChanged;
        private void Start()
        {
            LoadData();
            
            _onGameStateChanged = new EventBinding<OnGameStateChangedEvent>(Save);
            
            EventBus<OnGameStateChangedEvent>.Subscribe(_onGameStateChanged);
        }

        private void OnDisable()
        {
            EventBus<OnGameStateChangedEvent>.Unsubscribe(_onGameStateChanged);
        }

        private void LoadData()
        { 
            SaveSystem.LoadFromFile();

            foreach (var savableData in _savableDatas)
            {
                savableData.Load();
            }
            
        }

        private void Save()
        {
            foreach (var savableData in _savableDatas)
            {
                savableData.Save();
            }
            
            SaveSystem.SaveToFile();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            
            Save();

#if UNITY_EDITOR
            Debug.LogWarning("Data saved on application focus");
#endif
        }

        private void OnApplicationQuit()
        {
            Save();

#if UNITY_EDITOR
            Debug.LogWarning("Data saved on application quit");
#endif
        }

   
    }
}