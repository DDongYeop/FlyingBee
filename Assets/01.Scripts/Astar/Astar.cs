using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public static Astar Instance;

    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private Vector2 _leftTop, _rightBottom;
    private List<List<Node>> _map = new List<List<Node>>();
    private List<List<int>> _visited = new List<List<int>>();
    private List<Vector2Int> _road;

    private Vector2Int[] _addPos = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple Astar is running");
        Instance = this;
        Init();
    }

    private void Init()
    {
        for (int j = (int)_leftTop.y; j >= 0; --j)
        {
            _map.Add(new List<Node>());
            for (int i = 0; i <= _leftTop.x - _rightBottom.x; ++i)
            {
                _map[j].Add(new Node(false, j, i));
                if (Physics2D.BoxCast(new Vector2(j, i), new Vector2(0.4f, 0.4f), 0, Vector2.zero, 0, _whatIsWall))
                    _map[j][i].IsWall = true; //수정
            }
        }
    }

    public List<Vector2Int> AstarLoop(Vector2Int startPos, Vector2Int endPos)
    {
        _visited = new List<List<int>>();
        for (int j = (int)_leftTop.y; j >= 0; --j)
        {
            _visited.Add(new List<int>());
            for (int i = 0; i <= _leftTop.x - _rightBottom.x; ++i)
                _visited[j].Add(0); //수정
        }
        
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(endPos);
        _visited[endPos.x][endPos.y] = 1;

        while (queue.Count != 0)
        {
            Vector2Int front = queue.Dequeue();

            for (int i = 0; i < 4; ++i)
            {
                Vector2Int current = front + _addPos[i];
                if (current.x < 0 || current.y < 0 || current.x > _rightBottom.x || current.y > _rightBottom.y)
                    continue;
                if (_map[current.x][current.y].IsWall || _visited[current.x][current.y] != 0)
                    continue;

                _visited[current.x][current.y] = _visited[front.x][front.y] + 1;
                
                if (current == startPos)
                    break;
            }
        }

        _road.Add(startPos);
        while (true)
        {
            Vector2Int pos = _road[_road.Count - 1];
            int visitCnt = _visited[pos.x][pos.y];

            for (int i = 0; i < 4; ++i)
            {
                Vector2Int addPosision = pos + _addPos[i];
                if (_visited[addPosision.x][addPosision.y] == visitCnt - 1)
                {
                    _road.Add(addPosision);
                    break;
                }
            }
        }
        
        _road.Reverse();
        return _road;
    }
}
