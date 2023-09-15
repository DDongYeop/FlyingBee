using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _root;
    private Label _scoreTxt;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        _scoreTxt = _root.Q<Label>();

        _scoreTxt.text = "0";
    }

    public void CorrectionScore(int score)
    {
        _scoreTxt.text = score.ToString();
    }
}
