using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BFSAction : MonoBehaviour
{
    [Range(2, 100)]
    public int width, height;

    [SerializeField] private BFSVisual bfsVisual;
    private BFS pathfinding;


    void Start()
    {
        pathfinding = new BFS(width, height);
        bfsVisual.SetGrid(pathfinding.GetGrid());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
            BFSPathNode.ClearPath(pathfinding.GetGrid());
            pathfinding.GetGrid().GetGridXY(mousePos, out int x, out int y);
            List<BFSPathNode> path = pathfinding.FindPath(0, 0, x, y);
            if(path != null)
            {
                for(int i = 0; i < path.Count; ++i)
                {
                    path[i].SetIsFinalPath();
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetGridObject(mouseWorldPosition).SetIsWalkable();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clear();
        }
    }

    public void Clear()
    {
        pathfinding.GetGrid().GetGridObject(0, 0).ClearObstacle();
    }
}
