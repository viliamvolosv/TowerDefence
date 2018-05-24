using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{

	public LevelSettings LevelSettings;

	private int _currentGold = 0;

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

	void Start () {
		Assert.IsNotNull(LevelSettings," You need to setup level settings");
		_currentGold = LevelSettings.StartGold;
		GameState = GameStateEnum.Build;

	}

	private void  OnChageGameState()
	{
		
	}
}
