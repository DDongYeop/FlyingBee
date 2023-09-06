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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _playerMovement.Movement(PlayerDirectionState.UP);
        if (Input.GetKeyDown(KeyCode.A))
            _playerMovement.Movement(PlayerDirectionState.LEFT);
        if (Input.GetKeyDown(KeyCode.S))
            _playerMovement.Movement(PlayerDirectionState.DOWN);
        if (Input.GetKeyDown(KeyCode.D))
            _playerMovement.Movement(PlayerDirectionState.RIGHT);
    }
}
