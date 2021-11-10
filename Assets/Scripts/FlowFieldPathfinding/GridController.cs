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
        }
    }
}
