using UnityEngine;
using CodeMonkey.Utils;
using System;

public class Grid<GenericGridObject>
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;//一個委派事件, 參數類別為object, OnGridValueChangedEventArgs
    public class OnGridValueChangedEventArgs
    {
        public int x;
        public int y;
    }

    private readonly int width;
    private readonly int height;
    private readonly float cellSize;
    private Vector3 originPosition;//場景中網格座標的Offset, grid[0, 0]的位置
    private GenericGridObject[,] gridArray;//每格網格座標物件的類別為泛型


    public Grid(int _width, int _height, float _cellSize, Vector3 _originPosition, Func<Grid<GenericGridObject>, int, int, GenericGridObject> creatGridObject)
    {
        width = _width;
        height = _height;
        cellSize = _cellSize;
        originPosition = _originPosition;
        gridArray = new GenericGridObject[width, height];

        for(int x = 0; x < gridArray.GetLength(0); ++x)
        {
            for(int y = 0; y < gridArray.GetLength(1); ++y)
            {
                gridArray[x, y] = creatGridObject(this, x, y);
            }
        }

        bool showGridDetail = false;
        if (!showGridDetail) return;
        TextMesh[,] debugTextArray = new TextMesh[width, height];
        for (int x = 0; x < gridArray.GetLength(0); ++x)
        {
            for (int y = 0; y < gridArray.GetLength(1); ++y)
            {
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), default, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 30, Color.white, TextAnchor.MiddleCenter); 
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);

        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
        {
            debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
        };
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //滑鼠點擊從螢幕到場景的點, 點位減去起始Offset座標 除以 網格大小得到grid的XY
    public void GetGridXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, height - 1);
    }

    public void SetGridObject(int x, int y, GenericGridObject v)
    {
        if(x >= 0 && y >= 0 && x < width && y < height){
            gridArray[x, y] = v;
            if (OnGridValueChanged != null) OnGridValueChanged.Invoke(this, new OnGridValueChangedEventArgs{ x = x, y = y});
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null) OnGridValueChanged.Invoke(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    public void SetGridObject(Vector3 worldPosition, GenericGridObject v)
    {
        GetGridXY(worldPosition, out int x, out int y);
        SetGridObject(x, y, v);
    }

    /*
    public void AddValue(int x, int y, int v)
    {
        SetValue(x, y, GetValue(x, y) + v);
    }
    */

    public GenericGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(GenericGridObject);
        }
    }

    public GenericGridObject GetGridObject(Vector3 worldPosition)
    {
        GetGridXY(worldPosition, out int x, out int y);
        x = Mathf.Clamp(x, 0, width-1);
        y = Mathf.Clamp(y, 0, height-1);
        return GetGridObject(x, y);
    }

    /*
    public void AddValue(Vector3 worldPosition, int v, int range)
    {
        GetGridXY(worldPosition, out int x, out int y);

        for (int i = 0; i < range; i++)
        {
            for (int j = 0; j < range - i; j++)
            {
                AddValue(x + i, y + j, v);
                AddValue(x - i, y + j, v);
                AddValue(x - i, y - j, v);
                AddValue(x + i, y - j, v);
            }
        }
    }
    */
}
