using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    private Grid<BFSPathNode> grid;

    public BFS(int _width, int _height)
    {
        grid = new Grid<BFSPathNode>(_width, _height, 10f, Vector3.zero, (Grid<BFSPathNode> g, int x, int y) => new BFSPathNode(g, x, y));
    }

    public Grid<BFSPathNode> GetGrid()
    {
        return grid;
    }

    public List<BFSPathNode> FindPath(int sx, int sy, int ex, int ey)
    {
        var start = grid.GetGridObject(sx, sy);
        var end = grid.GetGridObject(ex, ey);

        start.SetIsStart();

        List<BFSPathNode> station = new List<BFSPathNode>() { start};

        while (station.Count > 0)
        {
            List<BFSPathNode> tmp = new List<BFSPathNode>(station);
            foreach(var s in tmp)
            {
                if (s == end)
                {
                    return FinalPath(end);
                }
                List<BFSPathNode> update = GetNeighbourlist(s);
                foreach(var t in update)
                {
                    if (!t.isWalkable || t.isStart) continue;
                    if (t.distance == 0)
                    {
                        t.ShortDistance(s.distance);
                        t.previousNode = s;
                        station.Add(t);
                    }/*else if (t.distance + 1 > s.distance)
                    {
                        t.ShortDistance(s.distance);
                        t.previousNode = s;
                    }*/
                }
            }
            station.RemoveRange(0, tmp.Count);
        }

        return null;
    }

    List<BFSPathNode> FinalPath(BFSPathNode _end)
    {
        List<BFSPathNode> result = new List<BFSPathNode>() { _end };
        BFSPathNode pre = _end.previousNode;

        while(pre != null)
        {
            result.Add(pre);
            pre = pre.previousNode;
        }

        result.Reverse();

        return result;
    }

    private List<BFSPathNode> GetNeighbourlist(BFSPathNode _currentNode)
    {
        List<BFSPathNode> neighbourList = new List<BFSPathNode>();

        if (_currentNode.y < grid.GetHeight() - 1)//y軸可以加
        {
            neighbourList.Add(GetPathNode(_currentNode.x, _currentNode.y + 1));
        }
        if (_currentNode.y > 0)//y軸可以減
        {
            neighbourList.Add(GetPathNode(_currentNode.x, _currentNode.y - 1));
        }
        if (_currentNode.x > 0)
        {
            neighbourList.Add(GetPathNode(_currentNode.x - 1, _currentNode.y));
        }
        if (_currentNode.x < grid.GetWidth() - 1)
        {
            neighbourList.Add(GetPathNode(_currentNode.x + 1, _currentNode.y));
        }

        return neighbourList;
    }

    private BFSPathNode GetPathNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
}
