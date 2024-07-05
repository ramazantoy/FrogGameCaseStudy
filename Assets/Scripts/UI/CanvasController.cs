using System;
using Enums;
using Events.EventBusScripts;
using Events.GameEvents;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// The CanvasController class handles the UI interactions and updates in the game.
    /// It manages button clicks, updates the display of moves, and reacts to changes in the game state.
    /// It also controls the visibility and state of various UI elements such as settings and restart buttons.
    /// </summary>
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private CanvasControllerData _properties;

        private EventBinding<OnMoveChanged> _onMoveAmountChanged;
        private EventBinding<OnGameStateChangedEvent> _onGameStateChanged;

        public static bool IsAnyWindowOpen = false;


        private readonly int _out = Animator.StringToHash("OUT");
        private readonly int _lose = Animator.StringToHash("LOSE");
        private readonly int _wın = Animator.StringToHash("WIN");
        private static readonly int SetIn = Animator.StringToHash("SET_IN");
        private static readonly int SetOut = Animator.StringToHash("SET_OUT");

        private void Start()
        {
            SetToggleViews();
        }

        private void OnEnable()
        {
            InitButtons();
            _onMoveAmountChanged = new EventBinding<OnMoveChanged>(SetMoveAmount);
            _onGameStateChanged = new EventBinding<OnGameStateChangedEvent>(OnGameStateChanged);


            EventBus<OnMoveChanged>.Subscribe(_onMoveAmountChanged);
            EventBus<OnGameStateChangedEvent>.Subscribe(_onGameStateChanged);

            _properties.LevelText.text = $"Level {_properties.GameSaveDataContainer.Data.LevelIndex}";
        }

        private void OnDisable()
        {
            UnInitButtons();
            EventBus<OnMoveChanged>.Unsubscribe(_onMoveAmountChanged);
            EventBus<OnGameStateChangedEvent>.Unsubscribe(_onGameStateChanged);
        }

        private void SetMoveAmount(OnMoveChanged args)
        {
            _properties.MovementText.text = $"{args.MoveAmount} MOVES";
        }

        private void OnGameStateChanged()
        {
            var state = GameManager.GameState;
            _properties.CanvasAnimator.SetTrigger(_out);
            if (state == GameState.Fail)
            {
                _properties.CanvasAnimator.SetTrigger(_lose);
                return;
            }

            _properties.CanvasAnimator.SetTrigger(_wın);
        }


        private void SetToggleViews()
        {
            var isMusicOn = _properties.GameSaveDataContainer.Data.IsMusicOn;
            var soundFxOn = _properties.GameSaveDataContainer.Data.IsSoundEffectsOn;

            _properties.ToggleImages[0].sprite =
                isMusicOn ? _properties.ToggleSprites[0] : _properties.ToggleSprites[1];
            _properties.ToggleImages[1].sprite =
                soundFxOn ? _properties.ToggleSprites[0] : _properties.ToggleSprites[1];
        }

        private void InitButtons()
        {
            _properties.OpenSettingsButton.onClick.AddListener(OnSettingsOpen);
            _properties.CloseSettingsButton.onClick.AddListener(OnSettingsClosed);
            _properties.MusicButton.onClick.AddListener(OnMusicChanged);
            _properties.SoudFxButton.onClick.AddListener(OnSoundFxChanged);
            _properties.RestartButton.onClick.AddListener(LoadGame);
        }

        private void UnInitButtons()
        {
            _properties.OpenSettingsButton.onClick.RemoveAllListeners();
            _properties.CloseSettingsButton.onClick.RemoveAllListeners();
            _properties.MusicButton.onClick.RemoveAllListeners();
            _properties.SoudFxButton.onClick.RemoveAllListeners();
            _properties.RestartButton.onClick.RemoveAllListeners();
            
        }

        private void OnMusicChanged()
        {
            _properties.GameSaveDataContainer.Data.IsMusicOn = !_properties.GameSaveDataContainer.Data.IsMusicOn;

            SetToggleViews();
            EventBus<OnMusicSettingsChanged>.Publish(new OnMusicSettingsChanged());
        }

        private void OnSoundFxChanged()
        {
            _properties.GameSaveDataContainer.Data.IsSoundEffectsOn = !_properties.GameSaveDataContainer.Data.IsSoundEffectsOn;
            
            SetToggleViews();
        }

        private void OnSettingsOpen()
        {
            _properties.CanvasAnimator.SetTrigger(SetIn);
            IsAnyWindowOpen = true;
        }

        private void OnSettingsClosed()
        {
            _properties.CanvasAnimator.SetTrigger(SetOut);
            IsAnyWindowOpen = false;
        }

        public void LoadGame()
        {
            EventBus<OnGameStateChangedEvent>.Publish(new OnGameStateChangedEvent());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}