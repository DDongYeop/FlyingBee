using System;

[Serializable]
public class Node
{
    public Node()
    {
        
    }

    public Node(bool isWall, int x, int y)
    {
        IsWall = isWall;
        X = x;
        Y = y;
    }
    
    public bool IsWall;

    public int X, Y, G;
}
