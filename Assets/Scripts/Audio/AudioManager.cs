using Events.EventBusScripts;
using Events.GameEvents;
using Unity.VisualScripting;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioManagerDataContainer _audioManagerDataContainer;

        [SerializeField] private AudioSource _audioSource;


        private EventBinding<OnClickFrogEvent> _onClickEvent;
        private EventBinding<OnCollectItemEvent> _onCollectItemEvent;
        private EventBinding<OnNewHexEvent> _onNewHexEvent;
        private EventBinding<OnWrongEvent> _onWrongEvent;
        private EventBinding<OnTriggerEvent> _onTriggerEvent;
        private void Start()
        {
           _audioSource.clip = _audioManagerDataContainer.MainSound;

           SetMusicSettings();
        }

        private void OnEnable()
        {
            _onClickEvent = new EventBinding<OnClickFrogEvent>(PlayOnClickFrogSound);
            _onCollectItemEvent = new EventBinding<OnCollectItemEvent>(PlayCollectItemSound);
            _onNewHexEvent = new EventBinding<OnNewHexEvent>(PlayOnNewHexSound);
            _onWrongEvent = new EventBinding<OnWrongEvent>(PlayWrongSound);
            _onTriggerEvent = new EventBinding<OnTriggerEvent>(PlayTriggerSound);

            EventBus<OnClickFrogEvent>.Subscribe(_onClickEvent);
            EventBus<OnCollectItemEvent>.Subscribe(_onCollectItemEvent);
            EventBus<OnNewHexEvent>.Subscribe(_onNewHexEvent);
            EventBus<OnWrongEvent>.Subscribe(_onWrongEvent);
            EventBus<OnTriggerEvent>.Subscribe(_onTriggerEvent);
        }

        private void OnDisable()
        {
            EventBus<OnClickFrogEvent>.Unsubscribe(_onClickEvent);
            EventBus<OnCollectItemEvent>.Unsubscribe(_onCollectItemEvent);
            EventBus<OnNewHexEvent>.Unsubscribe(_onNewHexEvent);
            EventBus<OnWrongEvent>.Unsubscribe(_onWrongEvent);
            EventBus<OnTriggerEvent>.Unsubscribe(_onTriggerEvent);
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
            if (!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;

          _audioSource.PlayOneShot(_audioManagerDataContainer.CollectItemSound);
        }

        private void PlayOnClickFrogSound()
        {
            if (!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;
            _audioSource.PlayOneShot(_audioManagerDataContainer.OnClickFrogSound);
        }

        private void PlayOnNewHexSound()
        {
            if (!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;
            _audioSource.PlayOneShot(_audioManagerDataContainer.OnNewHexSound);
        }

        private void PlayWrongSound()
        {
            if (!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;
            _audioSource.PlayOneShot(_audioManagerDataContainer.OnWrongSound);
        }

        private void PlayTriggerSound()
        {
            if (!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;
            _audioSource.PlayOneShot(_audioManagerDataContainer.OnTiggerSound);
        }
    }
}