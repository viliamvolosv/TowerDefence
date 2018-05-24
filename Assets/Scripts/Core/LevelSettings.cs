using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefence/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    public int StartGold = 100;
    public int StartHp = 10;
    public WaveSettings[] Waves;
}