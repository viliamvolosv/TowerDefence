using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public LevelSettings LevelSettings;
    public Transform BaseTrs;
    private Damageable _baseDamageable;
    private int _currentGold = 0;
    private int _currentWave = 0;

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

    public Damageable BaseDamageable
    {
        get { return _baseDamageable; }
    }

    public int CurrentWave
    {
        get { return _currentWave; }
    }

    public UnityAction OnGoldAmountChangeAction;
    public UnityAction<GameStateEnum> OnGameStateChangeAction;

    private void Awake()
    {
        Assert.IsNotNull(LevelSettings, " You need to setup level settings");
        Assert.IsNotNull(BaseTrs);
        _baseDamageable = BaseTrs.GetComponent<Damageable>();
        Assert.IsNotNull(_baseDamageable);
    }


    void Start()
    {
        CurrentGold = LevelSettings.StartGold;
        GameState = GameStateEnum.Build;
    }

    private void OnChageGameState()
    {
        if (OnGameStateChangeAction != null)
        {
            OnGameStateChangeAction(GameState);
        }
    }

    private void OnGoldAmountChange()
    {
        if (OnGoldAmountChangeAction != null)
        {
            OnGoldAmountChangeAction();
        }
    }
}