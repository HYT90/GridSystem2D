using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingVisual : MonoBehaviour
{
    private Grid<PathNode> grid;
    Mesh mesh;
    bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid<PathNode> _grid)
    {
        grid = _grid;
        UpdateVisual();

        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, Grid<PathNode>.OnGridValueChangedEventArgs e)
    {
        updateMesh = true;
        //UpdateHeatMapVisual();
    }

    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateVisual();
        }
    }

    private void UpdateVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);
    
        for(int x = 0; x < grid.GetWidth(); ++x)
        {
            for(int y = 0; y < grid.GetHeight(); ++y)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                PathNode pathNode = grid.GetGridObject(x, y);

                if (pathNode.isWalkable && !pathNode.isFinalPath && !pathNode.isChecked)
                {
                    quadSize = Vector3.zero;
                }

                if (pathNode.isFinalPath)
                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, Vector2.one, Vector2.one);
                else if (!pathNode.isWalkable)
                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, Vector2.zero, Vector2.zero);
                else if (pathNode.isChecked)
                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, Vector2.one * .5f, Vector2.one * .5f);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
