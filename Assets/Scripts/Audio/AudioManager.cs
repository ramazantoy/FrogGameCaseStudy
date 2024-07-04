using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioManagerDataContainer _audioManagerDataContainer;

        [SerializeField]
        private AudioSource _audioSource;
        

        private void Start()
        {
            _audioSource.clip = _audioManagerDataContainer.MainSound;
      
            SetMusicSettings();
        }

        private void OnEnable()
        {
            
            
        }

        private void OnDisable()
        {

        }

        private void SetMusicSettings()
        {
            if (_audioManagerDataContainer.GameSaveDataContainer.Data.IsMusicOn)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
        
        private void PlayCollectItemSound()
        {
            if(!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return; 
            
            _audioSource.PlayOneShot(_audioManagerDataContainer.CollectItemSound);
        }

        private void PlayOnClickSound()
        {
            if(!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;
            _audioSource.PlayOneShot(_audioManagerDataContainer.OnPanelChangeSound);
        }
    }
}
