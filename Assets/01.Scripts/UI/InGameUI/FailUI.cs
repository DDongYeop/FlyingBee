using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FailUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;
    private Button _nextStageButton;
    private Button _lobbyButton;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }
}
