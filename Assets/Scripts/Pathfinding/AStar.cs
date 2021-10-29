using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closeList;

    public AStar(int _width, int _height)
    {
        grid = new Grid<PathNode>(_width, _height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        openList = new List<PathNode> { startNode};
        closeList = new List<PathNode>();

        for(int x = 0; x<grid.GetWidth(); ++x)
        {
            for(int y = 0; y<grid.GetHeight(); ++y)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetTheLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach(PathNode neighbourNode in GetNeighbourlist(currentNode))
            {
                if (closeList.Contains(neighbourNode))
                {
                    continue;
                }
                if (!neighbourNode.isWalkable)
                {
                    closeList.Add(neighbourNode);
                    continue;
                }

                int moveGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(moveGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = moveGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode))
                        openList.Add(neighbourNode);
                }
            }
        }

        //當openList沒有物件時等於沒有有效路徑 
        return null;
    }

    private List<PathNode> GetNeighbourlist(PathNode _currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (_currentNode.y < grid.GetHeight() - 1)//y軸可以加
        {
            neighbourList.Add(GetPathNode(_currentNode.x, _currentNode.y + 1));
            if(_currentNode.x > 0)
            {
                neighbourList.Add(GetPathNode(_currentNode.x - 1, _currentNode.y + 1));
            }
            if (_currentNode.x < grid.GetWidth() - 1)
            {
                neighbourList.Add(GetPathNode(_currentNode.x + 1, _currentNode.y + 1));
            }
        }
        if (_currentNode.y > 0)//y軸可以減
        {
            neighbourList.Add(GetPathNode(_currentNode.x, _currentNode.y - 1));
            if (_currentNode.x > 0)
            {
                neighbourList.Add(GetPathNode(_currentNode.x - 1, _currentNode.y - 1));
            }
            if (_currentNode.x < grid.GetWidth() - 1)
            {
                neighbourList.Add(GetPathNode(_currentNode.x + 1, _currentNode.y - 1));
            }
        }
        if (_currentNode.y == 0 || _currentNode.y == grid.GetHeight() - 1)//y軸不變
        {
            if (_currentNode.x > 0)
            {
                neighbourList.Add(GetPathNode(_currentNode.x - 1, _currentNode.y));
            }
            if (_currentNode.x < grid.GetWidth() - 1)
            {
                neighbourList.Add(GetPathNode(_currentNode.x + 1, _currentNode.y));
            }
        }

        return neighbourList;
    }

    private PathNode GetPathNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        var finalPath = new List<PathNode>();
        PathNode pathNode = endNode;
        
        while(pathNode != null)
        {
            finalPath.Add(pathNode);
            pathNode = pathNode.previousNode;
        }

        finalPath.Reverse();

        return finalPath;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetTheLowestFCostNode(List<PathNode> _openList)
    {
        PathNode lowestNode = _openList[0];

        for(int i = 1; i<_openList.Count; i++)
        {
            if(lowestNode.FCost > _openList[i].FCost)
            {
                lowestNode = _openList[i];
            }
        }

        return lowestNode;
    }
}
