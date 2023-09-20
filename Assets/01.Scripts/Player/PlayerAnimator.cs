using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    
    private int _moveX = Animator.StringToHash("MoveX");
    private int _moveY = Animator.StringToHash("MoveY");
    private int _moving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Movement(Vector2 pos)
    {
        IsMoving(true);
        _animator.SetFloat(_moveX, pos.x);
        if (pos.y != 0)
            _animator.SetFloat(_moveY, pos.y);
    }

    public void IsMoving(bool value)
    {
        _animator.SetBool(_moving, value);
    }
}
