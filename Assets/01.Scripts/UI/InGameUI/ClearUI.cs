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
    private VisualElement _visual;
    private Button _nextButton;
    private Button _lobbyButton;
    private AudioSource _audioSource;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        _lobbyButton = _root.Q<Button>("Lobby");
        _visual = _root.Q<VisualElement>("Visual");
        _lobbyButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ClearDisable(() => SceneManager.LoadScene(1)))); 
        _nextButton = _root.Q<Button>("NextStage");
        _nextButton.RegisterCallback<ClickEvent>(NextStage);

        StartCoroutine(ClearEnable());
    }
    
    private void NextStage(ClickEvent evt)
    {
        int currentStage = SceneManager.GetActiveScene().buildIndex;
        if (Application.CanStreamedLevelBeLoaded(currentStage + 1))
            ++currentStage;
        else
            currentStage = 2;
        
        StartCoroutine(ClearDisable(() => SceneManager.LoadScene(currentStage)));
    }

    private IEnumerator ClearEnable()
    {
        yield return new WaitForSeconds(0.1f);
        _nextButton.RemoveFromClassList("left");
        _lobbyButton.RemoveFromClassList("right");
        _visual.RemoveFromClassList("up");
    }
    
    private IEnumerator ClearDisable(Action action)
    {
        _audioSource.Play();
        _nextButton.AddToClassList("left");
        _lobbyButton.AddToClassList("right");
        _visual.AddToClassList("up");
        yield return new WaitForSeconds(.4f);
        action?.Invoke();
    }
}
