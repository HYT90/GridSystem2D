using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public GridController gridController;
    public GameObject unitPrefab;
    public int numUnitsPerSpawn;
    public float moveSpeed;

    public List<GameObject> unitsInGame;

    void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnUnits();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DestroyUnits();
        }
    }

    private void FixedUpdate()
    {
        if (gridController.curFlowField == null) return;
        foreach(GameObject unit in unitsInGame)
        {
            Cell nodeBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
            Vector3 moveDirection = new Vector3(nodeBelow.bestDirection.Vector.x, nodeBelow.bestDirection.Vector.y, 0);
            unit.transform.Translate(moveDirection * moveSpeed);
        }
    }

    private void DestroyUnits()
    {
        foreach(GameObject g in unitsInGame)
        {
            Destroy(g);
        }
        unitsInGame.Clear();
    }

    private void SpawnUnits()
    {
        Vector2Int gridSize = gridController.gridSize;
        float nodeRadius = gridController.cellRadius;
        float nodeDiameter = nodeRadius * 2;
        Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeDiameter + nodeRadius, gridSize.y * nodeDiameter + nodeRadius);
        int colMask = LayerMask.GetMask("Impassible", "Units");
        Vector3 newPos;
        for (int i = 0; i < numUnitsPerSpawn; i++)
        {
            GameObject newUnit = Instantiate(unitPrefab);
            newUnit.transform.parent = transform;
            unitsInGame.Add(newUnit);
            do
            {
                newPos = new Vector3(Random.Range(0, maxSpawnPos.x), Random.Range(0, maxSpawnPos.y), 0);
                newUnit.transform.position = newPos;
            } while (Physics.OverlapSphere(newPos, 0.5f, colMask).Length > 0);
        }
    }
}
