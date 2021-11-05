using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class AStarAction : MonoBehaviour
{
    [Range(2, 100)]
    public int width, height;

    [SerializeField] private PathfindingVisual pathfindingVisual;
    private AStar pathfinding;

    void Start()
    {
        pathfinding = new AStar(width, height);
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetGridXY(mouseWorldPosition, out int x, out int y);
            var path = pathfinding.FindPath(0, 0, x, y);
            if(path != null)
            {
                for (int i = 0; i < path.Count-1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.red, 100f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetGridObject(mouseWorldPosition).SetIsWalkable();
        }
    }
}
