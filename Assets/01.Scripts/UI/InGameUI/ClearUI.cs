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
        _rePlayButton = _root.Q<Button>("NextStage");
        _rePlayButton.RegisterCallback<ClickEvent>(NextStage);
    }
    
    private void NextStage(ClickEvent evt)
    {
        int currentStage = SceneManager.GetActiveScene().buildIndex;
        if (Application.CanStreamedLevelBeLoaded(currentStage + 1))
            ++currentStage;
        else
            currentStage = 2;
        SceneManager.LoadScene(currentStage);
    }
}
