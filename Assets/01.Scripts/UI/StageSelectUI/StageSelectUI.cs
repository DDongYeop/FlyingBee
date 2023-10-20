using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StageSelectUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootVisual;

    private Label _stageLabel;
    private VisualElement _stage;
    private Button _stageButton;
    private Button _leftButton;
    private Button _rightButton;

    [SerializeField] private int _maxStage;
    private int _currentStage = 1;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        
        if (!PlayerPrefs.HasKey("Stage"))
            PlayerPrefs.SetInt("Stage", 1);
    }

    private void OnEnable()
    {
        _rootVisual = _uiDocument.rootVisualElement;
        _stage = _rootVisual.Q<VisualElement>("Stage01");
        _stage.AddToClassList("Show");
        _stageLabel = _rootVisual.Q<Label>("StageTxt"); 
        _leftButton = _rootVisual.Q<Button>("LeftArrow");
        _rightButton = _rootVisual.Q<Button>("RightArrow");
    
        UIMovement(0);
        
        _leftButton.RegisterCallback<ClickEvent>(e => UIMovement(-1));
        _rightButton.RegisterCallback<ClickEvent>(e => UIMovement(1));
    }

    private void UIMovement(int value)
    {
        _currentStage += value;
        _currentStage = Mathf.Clamp(_currentStage, 1, _maxStage);
        print(_currentStage);
        
        _stage.RemoveFromClassList("Show");
        _stage = _rootVisual.Q<VisualElement>($"Stage0{_currentStage}");
        _stage.AddToClassList("Show");
        _stageButton = _rootVisual.Q<Button>("Map");
        _stageButton.RegisterCallback<ClickEvent>(e => SceneManager.LoadScene(_currentStage + 1));
        _stageLabel.text = $"스테이지{_currentStage}";
    }
}
