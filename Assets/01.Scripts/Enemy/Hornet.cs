using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hornet : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    private Transform _playerTrm;
    
    private float _currentTime = 0;

    private void OnEnable()
    {
        _playerTrm = FindObjectOfType<PlayerMovement>().transform;
        StopAllCoroutines();
        StartCoroutine(MovementCo());
    }

    private IEnumerator MovementCo()
    {
        while (true)
        {
            yield return null;
            List<Vector2Int> moveRoad = Astar.Instance.AstarLoop(new Vector2Int((int)transform.position.x,(int)transform.position.y), new Vector2Int((int)_playerTrm.position.x,(int)_playerTrm.position.y));
            if (moveRoad.Count < 2)
                continue;
            
            _currentTime = 0;
            transform.position = (Vector2)moveRoad[0];

            while (_currentTime < _moveTime)
            {
                yield return null;
                _currentTime += Time.deltaTime;

                float time = _currentTime / _moveTime;
                transform.position = Vector2.Lerp(moveRoad[0], moveRoad[1], time);
            }
            transform.position = (Vector2)moveRoad[1];
        }
    }
}
