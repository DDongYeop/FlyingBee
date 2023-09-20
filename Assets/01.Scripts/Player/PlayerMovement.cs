using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;
    private Vector3[] _moveDirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private float _currentTime = 0;
    private Vector3 _startPos;
    private Vector3 _endPos;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void Movement(PlayerDirectionState dirState)
    {
        if (!MoveCheck(dirState))
        {
            _playerController.PlayerAni.IsMoving(false);
            return;
        }
        else if (_playerController.State == PlayerState.MOVE)
            return;
        
        _playerController.PlayerAni.Movement(_moveDirs[(int)dirState]);
        StartCoroutine(MovementCo(dirState));
    }

    private IEnumerator MovementCo(PlayerDirectionState dirState)
    {
        _playerController.State = PlayerState.MOVE;
        _startPos = transform.position;
        _endPos = _startPos + _moveDirs[(int)dirState];
        _currentTime = 0;
        
        while (_currentTime < _playerController.PlayerData.MoveSpeed)
        {
            yield return null;
            _currentTime += Time.deltaTime;
            _currentTime = Mathf.Clamp(_currentTime, 0, _playerController.PlayerData.MoveSpeed);
            transform.position = Vector3.Lerp(_startPos, _endPos, _currentTime / _playerController.PlayerData.MoveSpeed);
        }

        transform.position = _endPos;
        _playerController.State = PlayerState.IDLE;
        Movement(dirState);
    }

    private bool MoveCheck(PlayerDirectionState dirState)
    {
        if (Physics2D.Raycast(transform.position, _moveDirs[(int)dirState], _playerController.PlayerData.RayLength, _playerController.PlayerData.WhatIsObstacle))
            return false;
        else
            return true;
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        if (Application.isEditor)
            return;
    
        Handles.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.up * _playerController.PlayerData.RayLength);
        Gizmos.DrawRay(transform.position, Vector3.down * _playerController.PlayerData.RayLength);
        Gizmos.DrawRay(transform.position, Vector3.left * _playerController.PlayerData.RayLength);
        Gizmos.DrawRay(transform.position, Vector3.right * _playerController.PlayerData.RayLength);
        Handles.color = Color.white;
    }
    
#endif
}
