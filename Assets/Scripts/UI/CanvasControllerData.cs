using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    [System.Serializable]
    public class CanvasControllerData
    {
        public Animator CanvasAnimator;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI MovementText;
        public GameSaveDataContainer GameSaveDataContainer;

    }
}
