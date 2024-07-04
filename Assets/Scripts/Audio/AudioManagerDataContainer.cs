using SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioManagerDataContainer", menuName = "ScriptableObjects/AudioManagerDataContainer")]
    public class AudioManagerDataContainer : ScriptableObject
    {
        public GameSaveDataContainer GameSaveDataContainer;
        public AudioClip MainSound;
        public AudioClip CollectItemSound; 
        public AudioClip OnClickFrogSound;
        public AudioClip OnNewHexSound;
        public AudioClip OnWrongSound;
        public AudioClip OnTiggerSound;
    }
}
