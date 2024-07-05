using System;
using Enums;
using Events.EventBusScripts;
using Events.GameEvents;
using SaveSystem;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameSaveDataContainer _gameSaveDataContainer;
        private int _currentMoveAmount;

        private EventBinding<OnSetMoveAmountEvent> _onSetMoveAmount;

        private EventBinding<OnFrogMovementDoneEvent> _onFrogMovementDoneEvent;

        private EventBinding<OnClickFrogEvent> _onClickFrogEvent;

        public static GameState GameState { get; private set; }


        private void OnEnable()
        {
            _onSetMoveAmount = new EventBinding<OnSetMoveAmountEvent>(SetMoveAmount);
            _onFrogMovementDoneEvent = new EventBinding<OnFrogMovementDoneEvent>(OnFrogMovementDone);
            _onClickFrogEvent = new EventBinding<OnClickFrogEvent>(OnClickFrog);

            EventBus<OnSetMoveAmountEvent>.Subscribe(_onSetMoveAmount);
            EventBus<OnFrogMovementDoneEvent>.Subscribe(_onFrogMovementDoneEvent);
            EventBus<OnClickFrogEvent>.Subscribe(_onClickFrogEvent);
        }

        private void OnDisable()
        {
            EventBus<OnSetMoveAmountEvent>.Unsubscribe(_onSetMoveAmount);
            EventBus<OnFrogMovementDoneEvent>.Unsubscribe(_onFrogMovementDoneEvent);
            EventBus<OnClickFrogEvent>.Unsubscribe(_onClickFrogEvent);        }

        private void SetMoveAmount(OnSetMoveAmountEvent args)
        {
            GameState = GameState.Playing;
            _currentMoveAmount = args.MoveAmount;
          
            PublishMoveAmount();
        }

        private void PublishMoveAmount()
        {
                
            EventBus<OnMoveChanged>.Publish(new OnMoveChanged()
            {
                MoveAmount = _currentMoveAmount
            });
        }

        private void OnClickFrog()
        {
            _currentMoveAmount--;
            PublishMoveAmount();
        }

        private void OnFrogMovementDone()
        {
            if (GameFuncs.IsLevelDone())
            {
                _gameSaveDataContainer.Data.LevelIndex++;
                GameState = GameState.Win;
                EventBus<OnGameStateChangedEvent>.Publish(new OnGameStateChangedEvent());
            }
            else if (_currentMoveAmount <= 0)
            {
                GameState = GameState.Fail;
                EventBus<OnGameStateChangedEvent>.Publish(new OnGameStateChangedEvent());
            }

         
        }
    }
    
}