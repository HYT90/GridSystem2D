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
    public bool isFinalPath { get; private set; }

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
        ClearPath();
        isWalkable = false;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void SetIsFinalPath()
    {
        isFinalPath = true;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return x + "," + y;
    }

    public void ClearPath()
    {
        for(int i = 0; i < grid.GetWidth(); i++)
        {
            for(int j = 0; j < grid.GetHeight(); ++j)
            {
                grid.GetGridObject(i, j).isFinalPath = false;
            }
        }
    }
}
