using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    private Grid<HeatMapGridObject> grid;
    private Grid<StringGridObject> stringGrid;

    [Range(0, 100)]
    public int w, h;
    [Range(2.5f, 10f)]
    public float s;


    void Start()
    {
        //grid = new Grid<HeatMapGridObject>(w, h, s, Vector3.zero, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        stringGrid = new Grid<StringGridObject>(w, h, s, Vector3.zero, (Grid<StringGridObject> s, int x, int y) => new StringGridObject(s, x, y));
        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
        //heatMapGenericVisual.SetGrid(grid);
    }

    private void Update()
    {
        Vector3 position = UtilsClass.GetMouseWorldPosition();
        /*
        if (Input.GetMouseButton(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            //grid.AddValue(position, 2, 6);
            //grid.SetValue(position, true);
            HeatMapGridObject hmgo = grid.GetGridObject(position);
            if(hmgo != null)
            {
                hmgo.AddValue(2);
            }
        }
        */

        /*
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
        */

        if (Input.GetKeyDown(KeyCode.A))
        {
            stringGrid.GetGridObject(position).AddLetters("A");
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            stringGrid.GetGridObject(position).AddNumbers("7");
        }
    }
}

public class HeatMapGridObject
{
    private int Min = 0;
    private int Max = 100;

    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;

    private int value;

    public HeatMapGridObject(Grid<HeatMapGridObject> _grid, int _x, int _y)
    {
        grid = _grid;
        x = _x;
        y = _y;
    }
    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, Min, Max);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetVlaueNormalized()
    {
        return (float)value / Max;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class StringGridObject
{
    private string letters;
    private string numbers;
    private Grid<StringGridObject> grid;
    private int x;
    private int y;

    public StringGridObject(Grid<StringGridObject> _grid, int _x, int _y)
    {
        grid = _grid;
        x = _x;
        y = _y;
        letters = "";
        numbers = "";
    }

    public void AddLetters(string _letters)
    {
        letters += _letters;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddNumbers(string _numbers)
    {
        numbers += _numbers;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
