using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUIBehavior : MonoBehaviour
{

	public Text GoldCountText;
	public GameManager GameManager;

	// Use this for initialization
	void Start () {
		Assert.IsNotNull(GoldCountText);
		Assert.IsNotNull(GameManager);

		GameManager.OnGoldAmountChangeEvent.AddListener(OnGoldAmountChangeEvent);
	}

	void OnGoldAmountChangeEvent()
	{
		GoldCountText.text = "Gold: " + GameManager.CurrentGold;
	}

	private void OnDestroy()
	{
		GameManager.OnGoldAmountChangeEvent.RemoveListener(OnGoldAmountChangeEvent);
	}
}
