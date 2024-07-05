using System.Collections.Generic;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [System.Serializable]
    public class CanvasControllerData
    {
        public Animator CanvasAnimator;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI MovementText;
        public GameSaveDataContainer GameSaveDataContainer;
        public List<Image> ToggleImages;
        public List<Sprite> ToggleSprites;
        public Button OpenSettingsButton;
        public Button CloseSettingsButton;
        public Button RestartButton;
        public Button SoudFxButton;
        public Button MusicButton;

    }
}
