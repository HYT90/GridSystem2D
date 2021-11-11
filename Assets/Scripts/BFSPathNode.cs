using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFSPathNode
{
    private Grid<BFSPathNode> grid;
    public int x { get; private set; }
    public int y { get; private set; }

    public int distance { get; private set; }

    public BFSPathNode previousNode;

    public bool isStart { get; private set; }
    public bool isWalkable { get; private set; }
    public bool isFinalPath { get; private set; }

    public BFSPathNode(Grid<BFSPathNode> _grid, int _x, int _y)
    {
        grid = _grid;
        x = _x;
        y = _y;
        isWalkable = true;
    }

    public void ShortDistance(int d)
    {
        distance = d + 1;
    }

    public void SetIsStart()
    {
        isStart = true;
    }

    public void SetIsWalkable()
    {
        ClearPath(grid);
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
        return distance.ToString();
    }

    public static void ClearPath(Grid<BFSPathNode> grid)
    {
        for(int i = 0; i < grid.GetWidth(); i++)
        {
            for(int j = 0; j < grid.GetHeight(); ++j)
            {
                grid.GetGridObject(i, j).isStart = false;
                grid.GetGridObject(i, j).previousNode = null;
                grid.GetGridObject(i, j).distance = 0;
                grid.GetGridObject(i, j).isFinalPath = false;
            }
        }
    }

    public void ClearObstacle()
    {
        for (int i = 0; i < grid.GetWidth(); i++)
        {
            for (int j = 0; j < grid.GetHeight(); ++j)
            {
                grid.GetGridObject(i, j).distance = 0;
                grid.GetGridObject(i, j).isFinalPath = false;
                grid.GetGridObject(i, j).isStart = false;
                grid.GetGridObject(i, j).isWalkable = true;
                grid.TriggerGridObjectChanged(i, j);
            }
        }
    }
}
