using System;
using System.Collections;
using System.Collections.Generic;
using _3._Scripts;
using _3._Scripts.Architecture;
using _3._Scripts.Game;
using _3._Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private UIProgressbar progressbar;
    private int _scoreValue;
    public int levelGoal { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        LoadGoal();
    }
    private void Start()
    {
        _scoreValue = 0;
    }
    public void AddScore()
    {
        _scoreValue++;
        MoneyWidget.money += 1;
        progressbar.UpdateValue(_scoreValue, levelGoal);
    }

    private void LoadGoal()
    {
        try
        {
            levelGoal = Configuration.instance.levelsGoalConfig.GetGoal();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
}