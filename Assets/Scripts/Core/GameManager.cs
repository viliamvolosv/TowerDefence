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
        set
        {
            _currentGold = value;
            OnGoldAmountChange();
        }
    }

    public UnityEvent OnGoldAmountChangeEvent;


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

    void Start()
    {
        Assert.IsNotNull(LevelSettings, " You need to setup level settings");
        CurrentGold = LevelSettings.StartGold;
        GameState = GameStateEnum.Build;
    }

    private void OnChageGameState()
    {
    }

    private void OnGoldAmountChange()
    {
        if (OnGoldAmountChangeEvent !=null)
        {
            OnGoldAmountChangeEvent.Invoke();
        }
    }
}