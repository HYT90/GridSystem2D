using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;

    private void InitializeFlowField()
    {
        curFlowField = new FlowField(cellRadius, gridSize);
        curFlowField.CreateGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitializeFlowField();

            curFlowField.CreateCostField();

            Vector3 worldMousePos = UtilsClass.GetMouseWorldPosition();
            Cell destinationCell = curFlowField.GetCellFromWorldPos(worldMousePos);
            curFlowField.CreateIntegrationField(destinationCell);

            curFlowField.CreateFlowField();

            DrawGrid();
        }
    }

    void DrawGrid()
    {
        for (int i = 0; i < curFlowField.gridSize.x; ++i)
        {
            for (int j = 0; j < curFlowField.gridSize.y; j++)
            {
                var cell = curFlowField.grid[i, j];
                Vector3 pos = cell.worldPos - new Vector3(curFlowField.cellRadius, curFlowField.cellRadius);
                if (i < curFlowField.gridSize.x - 1)
                    Debug.DrawLine(pos, pos + Vector3.right, cell.cost == 255? Color.red : Color.green, 100f);
                if (j < curFlowField.gridSize.y - 1)
                    Debug.DrawLine(pos, pos + Vector3.up, cell.cost == 255 ? Color.red : Color.green, 100f);
            }
        }
    }
}
