using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ClearUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;
    private Button _rePlayButton;
    private Button _lobbyButton;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        _lobbyButton = _root.Q<Button>("Lobby");
        _lobbyButton.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(1));
        _rePlayButton = _root.Q<Button>("RePlay");
        _rePlayButton.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
}
