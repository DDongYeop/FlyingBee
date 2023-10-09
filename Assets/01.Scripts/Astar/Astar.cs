using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Astar : MonoBehaviour
{
    public Vector2Int StartPos, TargetPos, TopRight, BottomLeft; //TopRight와 BottomLeft는 나중에 수정. 
    public List<Node> FinalNodeLsit;
    [SerializeField] private LayerMask _obstacleLayer;

    private int _sizeX, _sizeY;
    private Node[,] _nodeArray;
    private Node _startNode, _targetNode, _currentNode;
    private List<Node> _openList, _closedList;
    private int[] _addX = { 1, -1, 0, 0 };
    private int[] _addY = { 0, 0, 1, -1 };
    private int cost = 10;

    public void AstarInit()
    {
        _sizeX = TopRight.x - BottomLeft.x + 1;
        _sizeY = TopRight.y - BottomLeft.y + 1;
        _nodeArray = new Node[_sizeX, _sizeY];

        for (int i = 0; i < _sizeX; ++i)
        {
            for (int j = 0; j < _sizeY; ++j)
            {
                bool isWall = false;
                if (Physics2D.OverlapCircle(new Vector2(i, j), 0.45f, _obstacleLayer)) //OverlapCircleNonAlloc로 바꿔주면 굿 
                    isWall = true;
            }
        }

        //위치 정해주기
        _startNode = _nodeArray[StartPos.x - BottomLeft.x, StartPos.y - BottomLeft.y];
        _targetNode = _nodeArray[TargetPos.x - BottomLeft.x, TargetPos.y - BottomLeft.y];

        _openList = new List<Node>() { _startNode };
        _closedList = new List<Node>();
        FinalNodeLsit = new List<Node>();
    }

    public List<Node> AstarLoop()
    {
        //위치 정하는거 해야댐
        
        while (_openList.Count > 0)
        {
            _currentNode = _openList[0];
            
            foreach (var node in _openList) //priority queue가 있다면 교체하면 좋을듯. 
            {
                if (node.F <= _currentNode.F && node.H < _currentNode.H)
                    _currentNode = node;
            }

            _openList.Remove(_currentNode);
            _closedList.Add(_currentNode);

            if (_currentNode == _targetNode) //도착
            {
                Node targetCurrentNode = _targetNode;
                while (targetCurrentNode != _startNode)
                {
                    FinalNodeLsit.Add(targetCurrentNode);
                    targetCurrentNode = targetCurrentNode.ParentNode;
                }
                FinalNodeLsit.Add(_startNode);
                FinalNodeLsit.Reverse();
                
                return FinalNodeLsit;
            }

            for (int i = 0; i < 4; ++i)
                OpenListAdd(_currentNode.X + _addX[i], _currentNode.Y + _addY[i]);
        }

        return null;
    }

    private void OpenListAdd(int checkX, int checkY)
    {
        if (checkX >= BottomLeft.x && checkX <= TopRight.x && checkY <= TopRight.y && checkY >= BottomLeft.y &&
            !_nodeArray[checkX, checkY].IsWall && _closedList.Contains(_nodeArray[checkX, checkY]))
        {
            Node neighborNode = _nodeArray[checkX, checkY];
            int moveCost = _currentNode.G + cost;

            if (moveCost < neighborNode.G || !_openList.Contains(neighborNode))
            {
                neighborNode.G = moveCost;
                neighborNode.H = (Mathf.Abs(neighborNode.X - _targetNode.X) + Mathf.Abs(neighborNode.Y - _targetNode.Y)) * 10;
                neighborNode.ParentNode = _currentNode;
                
                _openList.Add(neighborNode);
            }
        }
    }
}
