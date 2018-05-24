using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public LevelSettings LevelSettings;
    public Transform BaseTrs;
    public Transform SpawTrs;
    private Damageable _baseDamageable;
    private int _currentGold = 0;
    private int _currentWaveCount = -1;
    private WaveSettings _waveSettings;

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

    public int CurrentWaveCount
    {
        get { return _currentWaveCount; }
    }

    public UnityAction OnGoldAmountChangeAction;
    public UnityAction<GameStateEnum> OnGameStateChangeAction;

    private void Awake()
    {
        Assert.IsNotNull(SpawTrs);
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

    public void StartNextWave()
    {
        _currentWaveCount++;
        _waveSettings = LevelSettings.Waves[_currentWaveCount];
        GameState = GameStateEnum.Wave;
        StartCoroutine(EnemySpawnEnumerator());
    }

    private IEnumerator EnemySpawnEnumerator()
    {
        for (int i = 0; i < _waveSettings.Count; i++)
        {
            yield return new WaitForSeconds(_waveSettings.TimeIntervalSec);
            var go = Instantiate(_waveSettings.EnemyPrefab, SpawTrs.position, SpawTrs.rotation);
            var enemy = go.GetComponent<EnemyBase>();
            enemy.Target = BaseTrs;
        }

        GameState = GameStateEnum.Build;
    }
}