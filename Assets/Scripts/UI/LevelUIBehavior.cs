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

    public static TowerBase SelectedTowerBase;
    public static bool AboveTile = false;

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

    public void CreateTower()
    {
        if (SelectedTowerBase != null)
            DestroyTower();
        var go = Instantiate(GameManager.TowerPrefab);
        SelectedTowerBase = go.GetComponent<TowerBase>();
    }

    void DestroyTower()
    {
        Destroy(SelectedTowerBase.gameObject);
        SelectedTowerBase = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && SelectedTowerBase != null)
            DestroyTower();
        if (SelectedTowerBase != null && !AboveTile)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            SelectedTowerBase.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }
}