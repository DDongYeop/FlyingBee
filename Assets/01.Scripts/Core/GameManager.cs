using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            return _instance;
        }
    }
    
    private SavedGame _saveData = new SavedGame();
    public SavedGame SaveData
    {
        get
        {
            _saveData.TryLoad();
            return _saveData;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            Debug.LogError("Multiple GameManager is running");
        _instance = this;
        _saveData.TryLoad();
    }

    private void OnDestroy()
    {
        _saveData.Save();
    }

    public int BestScore
    {
        get => _saveData.BestScore;
        set
        {
            if (_saveData.BestScore < value)
                _saveData.BestScore = value;
        }
    }

    public int CurrentScore
    {
        get => _saveData.CurrentScore;
        set => _saveData.CurrentScore = value;
    }
}
