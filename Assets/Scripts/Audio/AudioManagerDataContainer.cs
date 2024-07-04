using SaveSystem;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioManagerDataContainer", menuName = "ScriptableObjects/AudioManagerDataContainer")]
    public class AudioManagerDataContainer : ScriptableObject
    {
        public GameSaveDataContainer GameSaveDataContainer;
        public AudioClip MainSound;
        public AudioClip CollectItemSound;
        public AudioClip OnPanelChangeSound;
    }
}
