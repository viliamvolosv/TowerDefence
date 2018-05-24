using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUIBehavior : MonoBehaviour
{
    public Text GoldCountText;
    public Text BaseHpText;
    public Text WavesInfo;
    public Button StartNextWaveButton;
    public GameManager GameManager;

    // Use this for initialization
    void Start()
    {
        Assert.IsNotNull(GoldCountText);
        Assert.IsNotNull(GameManager);
        Assert.IsNotNull(BaseHpText);
        Assert.IsNotNull(WavesInfo);
        Assert.IsNotNull(StartNextWaveButton);

        GameManager.OnGoldAmountChangeAction += OnGoldAmountChangeEvent;
        GameManager.OnGameStateChangeAction += OnGameStateChangeAction;
        GameManager.BaseDamageable.OnChangeHpAction += OnBaseDamage;
        BaseHpText.text = "Base HP: " + GameManager.BaseDamageable.CurrentHp + " / " +
                          GameManager.BaseDamageable.StartHp;
        WavesInfo.text = "Waves: " + (GameManager.CurrentWaveCount + 1) + " /" + GameManager.LevelSettings.Waves.Length;
        StartNextWaveButton.onClick.AddListener(GameManager.StartNextWave);
    }

    private void OnGameStateChangeAction(GameManager.GameStateEnum state)
    {
        StartNextWaveButton.gameObject.SetActive(state == GameManager.GameStateEnum.Build);
        WavesInfo.text = "Waves: " + (GameManager.CurrentWaveCount + 1) + " /" + GameManager.LevelSettings.Waves.Length;
    }

    void OnGoldAmountChangeEvent()
    {
        GoldCountText.text = "Gold: " + GameManager.CurrentGold;
    }

    void OnBaseDamage(Damageable damageable)
    {
        BaseHpText.text = "Base HP: " + damageable.CurrentHp + " / " + damageable.StartHp;
    }

    private void OnDestroy()
    {
        GameManager.OnGoldAmountChangeAction -= OnGoldAmountChangeEvent;
        GameManager.BaseDamageable.OnChangeHpAction -= OnBaseDamage;
        StartNextWaveButton.onClick.RemoveAllListeners();
    }
}