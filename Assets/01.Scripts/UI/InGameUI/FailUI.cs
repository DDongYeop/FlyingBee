using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FailUI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;
        root.Q<Button>("RePlay").RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        root.Q<Button>("Lobby").RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(1));
    }
}