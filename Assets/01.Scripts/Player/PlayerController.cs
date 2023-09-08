using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerState State = PlayerState.IDLE;

    public PlayerSO PlayerData;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        InputManager.Instance.SwipeInput += PlayerInput;
    }

    private void PlayerInput(PlayerDirectionState dirState)
    {
        _playerMovement.Movement(dirState);
    }
}
