using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootVisual;
    private Button _startButton;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _rootVisual = _uiDocument.rootVisualElement;
        _startButton = _rootVisual.Q<Button>("StartButton");
        _startButton.RegisterCallback<ClickEvent>(e => StartCoroutine(TitleMoveCo()));
    }

    private IEnumerator TitleMoveCo()
    {
        _startButton.AddToClassList("click");
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene(1);
    }
}
