using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FailUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private AudioSource _audioSource;

    private VisualElement _visual;
    private Button _rePlayButton;
    private Button _lobbyButton;
    
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;
        _visual = root.Q<VisualElement>("Visual");
        _rePlayButton = root.Q<Button>("RePlay");
        _rePlayButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(FailDisable(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name))));
        _lobbyButton = root.Q<Button>("Lobby");
        _lobbyButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(FailDisable(() => SceneManager.LoadScene(1))));
        
        StartCoroutine(FailEnable());
    }
    
    private IEnumerator FailEnable()
    {
        yield return new WaitForSeconds(0.1f);
        _rePlayButton.RemoveFromClassList("left");
        _lobbyButton.RemoveFromClassList("right");
        _visual.RemoveFromClassList("up");
    }
    
    private IEnumerator FailDisable(Action action)
    {
        _audioSource.Play();
        _rePlayButton.AddToClassList("left");
        _lobbyButton.AddToClassList("right");
        _visual.AddToClassList("up");
        yield return new WaitForSeconds(.4f);
        action?.Invoke();
    }
}