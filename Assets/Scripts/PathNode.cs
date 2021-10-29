using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;
    public int x { get; private set; }
    public int y { get; private set; }

    public int gCost;
    public int hCost;

    public int FCost { get; private set; }

    public PathNode previousNode;
    public bool isWalkable { get; private set; }

    public PathNode(Grid<PathNode> _grid, int _x, int _y)
    {
        grid = _grid;
        x = _x;
        y = _y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        FCost = gCost + hCost;
    }

    public void SetIsWalkable()
    {
        isWalkable = !isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
