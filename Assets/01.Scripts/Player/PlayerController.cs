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
    [HideInInspector] public PlayerAnimator PlayerAni;
    [SerializeField] private Transform _typhoonPos;

    private int _score = 0;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _inGameUI.CorrectionScore(Score);
        }
    }

    [Header("Other")] 
    private InGameUI _inGameUI;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        InputManager.Instance.SwipeInput += PlayerInput;

        PlayerAni = transform.GetChild(0).GetComponent<PlayerAnimator>();
        _inGameUI = FindObjectOfType<InGameUI>();
    }

    private void Update()
    {
        if (_typhoonPos.position.y < transform.position.y)
            NaturalDisasterManager.Instance.Typhoon();
    }

    private void PlayerInput(PlayerDirectionState dirState)
    {
        _playerMovement.Movement(dirState);
    }
}
