using System;

[Serializable]
public class Node
{
    public Node(bool isWall, int x, int y)
    {
        IsWall = isWall;
        X = x;
        Y = y;
    }
    
    public bool IsWall;
    public Node ParentNode;

    public int X, Y, G, H;
    public int F { get { return G + H; } }
}
