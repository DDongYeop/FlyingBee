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
        root.Q<Button>("NextStage").RegisterCallback<ClickEvent>(NextStage);
        root.Q<Button>("NextStage").RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(1));
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
