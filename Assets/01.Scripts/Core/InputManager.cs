using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<InputManager>();
            return _instance;
        }
    }

    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Update()
    {
        Swipe();
    }

    private void Swipe()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startPos = Input.GetTouch(0).position;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            
        }
    }
}
