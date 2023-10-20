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
    private List<Vector2Int> _road = new List<Vector2Int>();

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
        for (int j = 0; j <= _leftTop.y; ++j)
        {
            _map.Add(new List<Node>());
            for (int i = 0; i <= _rightBottom.x; ++i)
            {
                _map[j].Add(new Node(false, j, i));
                if (Physics2D.OverlapCircle(new Vector2(i, j), 0.4f, _whatIsWall))
                    _map[j][i].IsWall = true; //수정
            }
        }
    }

    public List<Vector2Int> AstarLoop(Vector2Int startPos, Vector2Int endPos)
    {
        _visited.Clear();
        for (int j = 0; j <= _leftTop.y; ++j)
        {
            _visited.Add(new List<int>());
            for (int i = 0; i <= _rightBottom.x; ++i)
                _visited[j].Add(0); 
        }
        
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(new Vector2Int(endPos.y, endPos.x));
        _visited[endPos.y][endPos.x] = 1;

        while (queue.Count != 0)
        {
            Vector2Int front = queue.Dequeue();

            for (int i = 0; i < 4; ++i)
            {
                Vector2Int current = front + _addPos[i];
                if (current.x < 0 || current.y < 0 || current.x >= _leftTop.y || current.y >= _rightBottom.x)
                    continue;
                if (_map[current.x][current.y].IsWall || _visited[current.x][current.y] != 0)
                    continue;

                _visited[current.x][current.y] = _visited[front.x][front.y] + 1;
                queue.Enqueue(current);
                current = new Vector2Int(current.y, current.x);
                if (current == startPos)
                    break;
            }
        }

        _road.Clear();
        _road.Add(startPos);
        int cnt = _visited[startPos.y][startPos.x];
        
        while (cnt > 0)
        {
            cnt--;
            Vector2Int pos = _road[_road.Count - 1];
            int visitCnt = _visited[pos.y][pos.x];

            for (int i = 0; i < 4; ++i)
            {
                Vector2Int addPosition = pos + _addPos[i];
                if (addPosition.x < 0 || addPosition.y < 0 )
                    continue;
                if (_visited[addPosition.y][addPosition.x] == visitCnt - 1)
                {
                    _road.Add(addPosition);
                    break;
                }
            }
        }

        return _road;
    }
}
