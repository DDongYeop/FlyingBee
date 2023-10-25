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
    private VisualElement _buttons;
    private VisualElement _stageTextField;
    private Button _stageButton;
    private Button _leftButton;
    private Button _rightButton;

    private AudioSource _audioSource;
    [SerializeField] private int _maxStage;
    private int _currentStage = 1;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioSource = GetComponent<AudioSource>();
        
        if (!PlayerPrefs.HasKey("Stage"))
            PlayerPrefs.SetInt("Stage", 1);
    }

    private void OnEnable()
    {
        _rootVisual = _uiDocument.rootVisualElement;
        _buttons = _rootVisual.Q<VisualElement>("Buttons");
        _stage = _rootVisual.Q<VisualElement>("Stage01");
        _stageTextField = _rootVisual.Q<VisualElement>("StageTxtImage");
        _stageLabel = _rootVisual.Q<Label>("StageTxt"); 
        _leftButton = _rootVisual.Q<Button>("LeftArrow");
        _rightButton = _rootVisual.Q<Button>("RightArrow");

        StartCoroutine(UIUnShowCo());
    
        UIMovement(0);
        
        _leftButton.RegisterCallback<ClickEvent>(e => UIMovement(-1));
        _rightButton.RegisterCallback<ClickEvent>(e => UIMovement(1));
        _stage.AddToClassList("Show");
    }

    private void UIMovement(int value) // left, right, unShow << 얘네 넣어주고 뺴고 해야댐 
    {
        _audioSource.Play();
        _currentStage += value;
        if (_currentStage <= 0 || _currentStage > _maxStage)
        {
            _currentStage = Mathf.Clamp(_currentStage, 1, _maxStage);
            return;
        }
        
        if (value == 1)
        {
            _stage.AddToClassList("left");
            _stage = _rootVisual.Q<VisualElement>($"Stage0{_currentStage}");
            _stage.AddToClassList("Show");
            _stageButton = _stage.Q<Button>("Map");
            _stageButton.RegisterCallback<ClickEvent>(e => StartCoroutine(ButtonClick()));
            _stageLabel.text = $"스테이지{_currentStage}";
        }
        else
        {
            _stage.RemoveFromClassList("Show");
            _stage = _rootVisual.Q<VisualElement>($"Stage0{_currentStage}");
            _stage.RemoveFromClassList("left");
            _stageButton = _stage.Q<Button>("Map");
            _stageButton.RegisterCallback<ClickEvent>(e => StartCoroutine(ButtonClick()));
            _stageLabel.text = $"스테이지{_currentStage}";
        }
    }

    private IEnumerator ButtonClick()
    {
        _audioSource.Play();
        _stage.AddToClassList("unShow");
        _buttons.AddToClassList("unshow");
        _stageTextField.AddToClassList("unshow");
        yield return new WaitForSeconds(.55f);
        SceneManager.LoadScene(_currentStage + 1);
    }

    private IEnumerator UIUnShowCo()
    {
        yield return new WaitForSeconds(0.1f);
        
        _stage.RemoveFromClassList("unShow");
        _buttons.RemoveFromClassList("unshow");
        _stageTextField.RemoveFromClassList("unshow");
    }
}
