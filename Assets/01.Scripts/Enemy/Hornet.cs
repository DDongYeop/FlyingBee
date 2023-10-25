using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hornet : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    private Transform _playerTrm;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D; 
    private float _currentTime = 0;

    private IEnumerator Start()
    {
        _playerTrm = FindObjectOfType<PlayerMovement>().transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        yield return new WaitForSeconds(10f);
        _spriteRenderer.enabled = true;
        _collider2D.enabled = true;
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
            else if (moveRoad.Count < 3)
            {
                UIManager.Instance.UIActive(true, (int)UIType.FAIL);
                break;
            }
            
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
