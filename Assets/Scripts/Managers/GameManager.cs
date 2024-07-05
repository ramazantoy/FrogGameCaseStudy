using System.Collections.Generic;
using Enums;
using Events.EventBusScripts;
using Events.GameEvents;
using FrogScripts;
using SaveSystem;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// The GameManager class is responsible for managing the game state,
    /// handling frog interactions, and updating the level status based on player moves.
    /// It subscribes to and handles various game events such as setting move amounts,
    /// frog movements, and click events on frogs.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameSaveDataContainer _gameSaveDataContainer;
        private int _currentMoveAmount;

        private EventBinding<OnSetMoveAmountEvent> _onSetMoveAmount;

        private EventBinding<OnFrogMovementDoneEvent> _onFrogMovementDoneEvent;

        private EventBinding<OnClickFrogEvent> _onClickFrogEvent;


        private  List<Frog> _activeFrogs = new List<Frog>();

        private void AddFrog(Frog frog)
        {
            if(_activeFrogs.Contains(frog)) return;
            
            _activeFrogs.Add(frog);
        }

        private void RemoveFrog(Frog frog)
        {
            if (_activeFrogs.Contains(frog))
            {
                _activeFrogs.Remove(frog);
            }

            if (_activeFrogs.Count <= 0)
            {
                CheckLevelStatus();
            }
        }

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

        private void OnClickFrog(OnClickFrogEvent args)
        {
            AddFrog(args.Frog);
            _currentMoveAmount--;
            if (_currentMoveAmount <= 0) _currentMoveAmount = 0;
            PublishMoveAmount();
        }

        private void CheckLevelStatus()
        {
            if(GameState!=GameState.Playing) return;
            
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

        private void OnFrogMovementDone(OnFrogMovementDoneEvent args)
        {
            
            RemoveFrog(args.Frog);
        
        }
        
    }
    
}