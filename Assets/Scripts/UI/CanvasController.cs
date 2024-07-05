using System;
using Enums;
using Events.EventBusScripts;
using Events.GameEvents;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {

        [SerializeField]
        private CanvasControllerData _properties;
        
        private EventBinding<OnMoveChanged> _onMoveAmountChanged;
        private EventBinding<OnGameStateChangedEvent> _onGameStateChanged;


        private  readonly int _out = Animator.StringToHash("OUT");
        private  readonly int _lose = Animator.StringToHash("LOSE");
        private  readonly int _wın = Animator.StringToHash("WIN");

        private void OnEnable()
        {
            _onMoveAmountChanged = new EventBinding<OnMoveChanged>(SetMoveAmount);
            _onGameStateChanged = new EventBinding<OnGameStateChangedEvent>(OnGameStateChanged);
            
            
            EventBus<OnMoveChanged>.Subscribe(_onMoveAmountChanged);
            EventBus<OnGameStateChangedEvent>.Subscribe(_onGameStateChanged);

            _properties.LevelText.text = $"Level {_properties.GameSaveDataContainer.Data.LevelIndex}";
        }

        private void OnDisable()
        {
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
        
        public void LoadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
