using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<Obstacle>(out var obstacle);
        
        if (obstacle == null)
            return;

        switch (obstacle.ObstacleData.Type)
        {
            case ObstacleType.COIN:
                _playerController.Score++;
                obstacle.OnCollision?.Invoke();
                Destroy(other.gameObject, 2f);
                break;
            case ObstacleType.ENDPOS:
                UIManager.Instance.UIActive(true, (int)UIType.CLEAR);
                break;
        }
    }
}
