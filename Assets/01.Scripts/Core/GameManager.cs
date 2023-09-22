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
                _instance = FindObjectOfType<GameManager>();
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
        _saveData.TryLoad();
        Application.targetFrameRate = -1;
    }

    private void OnDestroy()
    {
        _saveData.Save();
    }


    public int Level
    {
        get => SaveData.Level;
        set => SaveData.Level = value;
    }
}
