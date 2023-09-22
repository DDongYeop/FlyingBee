using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SavedGame
{
    private const string _saveKey = "Save";
    private bool _loaded = false;

    [FormerlySerializedAs("CurrentLevel")] [SerializeField] public int Level = 0;

    public void TryLoad()
    {
        if (!_loaded && PlayerPrefs.HasKey(_saveKey))
        {
            string jsonString = PlayerPrefs.GetString(_saveKey);
            JsonUtility.FromJsonOverwrite(jsonString, this);
            _loaded = true;
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(_saveKey, jsonString);
    }
}
