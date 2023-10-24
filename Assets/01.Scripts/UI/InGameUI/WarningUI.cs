using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WarningUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;
    private Label _leftLabel;
    private Label _rightLabel;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        _leftLabel = _root.Q<Label>("LeftLabel");
        _rightLabel = _root.Q<Label>("RightLabel");
        StopAllCoroutines();
        StartCoroutine(LabelCo());
    }

    private IEnumerator LabelCo()
    {
        for (int i = 0; i < 3; ++i)
        {
            _leftLabel.AddToClassList("show");
            _rightLabel.AddToClassList("show");
            yield return new WaitForSeconds(0.2f);
            _leftLabel.RemoveFromClassList("show");
            _rightLabel.RemoveFromClassList("show");
            yield return new WaitForSeconds(0.2f);
        }
        
        gameObject.SetActive(false);
    }
}
