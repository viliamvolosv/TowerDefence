using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public LevelSettings LevelSettings;

    private int _currentGold = 0;

    public int CurrentGold
    {
        get { return _currentGold; }
        private set
        {
            _currentGold = value;
            OnGoldAmountChange();
        }
    }

    public enum GameStateEnum
    {
        Build,
        Wave,
        End
    }

    private GameStateEnum _gameState;

    public GameStateEnum GameState
    {
        get { return _gameState; }
        set
        {
            _gameState = value;
            OnChageGameState();
        }
    }
    
    [Header("Events")]
    public UnityEvent OnGoldAmountChangeEvent;
    public UnityEvent<GameStateEnum> OnGameStateChangeEvent;
    

    void Start()
    {
        Assert.IsNotNull(LevelSettings, " You need to setup level settings");
        CurrentGold = LevelSettings.StartGold;
        GameState = GameStateEnum.Build;
    }

    private void OnChageGameState()
    {
        if (OnGameStateChangeEvent !=null)
        {
            OnGameStateChangeEvent.Invoke(GameState);
        }
    }

    private void OnGoldAmountChange()
    {
        if (OnGoldAmountChangeEvent !=null)
        {
            OnGoldAmountChangeEvent.Invoke();
        }
    }
}