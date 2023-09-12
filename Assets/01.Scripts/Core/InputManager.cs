using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<InputManager>();
            return _instance;
        }
    }

    public event Action<PlayerDirectionState> SwipeInput; 

    private float _swipeDistance;
    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Awake()
    {
        _swipeDistance = Screen.width * 0.3f;
    }

    private void Update()
    {
#if UNITY_EDITOR
        KeyBoard();
#elif UNITY_ANDROID
        Swipe();
#endif
    }

    private void KeyBoard()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        DirectionCheck(dir);
    }

    private void Swipe()
    {
        // 조작감 수정
        // 터치 하면 그 위치 우선 잡아주고 계속 갱신해주면서 특정 거리 넘기면 그 방향으로 움직임. 
        // 움직이고 나선 마지막 거기에 다 같은 위치 넣어주기. 
        
        if (Input.touchCount <= 0)
            return;
        
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startPos = Input.GetTouch(0).position;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endPos = Input.GetTouch(0).position;
            
            if (Vector2.Distance(_startPos, _endPos) < _swipeDistance)
                return;

            Vector2 direction = (_startPos - _endPos).normalized;
            direction.x = Mathf.Round(direction.x);
            direction.y = Mathf.Round(direction.y);
            DirectionCheck(direction * -1);
        }
    }

    private void DirectionCheck(Vector2 dir)
    {
        if (dir == Vector2.up)
            SwipeInput?.Invoke(PlayerDirectionState.UP);
        else if (dir == Vector2.down)
            SwipeInput?.Invoke(PlayerDirectionState.DOWN);
        else if (dir == Vector2.left)
            SwipeInput?.Invoke(PlayerDirectionState.LEFT);
        else if (dir == Vector2.right)
            SwipeInput?.Invoke(PlayerDirectionState.RIGHT);
    }
}
