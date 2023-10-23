using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<GameObject> _uis;

    private bool _isShow = false;
    private int _currentIndex = -1; 
    
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple UIManager is running");
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UIActive(true, (int)UIType.GOBACK);
    }

    public void UIActive(bool value, int index = -1)
    {
        if (value)
        {
            if (_isShow)
                return;
        }
        else
        {
            if (_currentIndex == -1)
                return;
        }
        _uis[_currentIndex].SetActive(value);
        _isShow = value;
        _currentIndex = index;
    }
}
