/*
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defense.Utils;

public class GridXZ<TGridObject> {

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int z;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    [SerializeField] private TGridObject[,] gridArray;
    private List<(int, int)> blockedCells;

    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, bool, TGridObject> createGridObject) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        blockedCells = new List<(int, int)>();

        LoadBlockedCells();

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int z = 0; z < gridArray.GetLength(1); z++) {
                if (blockedCells.Contains((x, z))) {
                    gridArray[x, z] = createGridObject(this, x, z, false);
                } else {
                    gridArray[x, z] = createGridObject(this, x, z, true);
                }
            }
        }

        bool showDebug = true;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int z = 0; z < gridArray.GetLength(1); z++) {
                    debugTextArray[x, z] = UtilsClass.CreateWorldText(gridArray[x, z]?.ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * .5f, 15, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();
            };
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int z) {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetGridObject(int x, int z, TGridObject value) {
        if (x >= 0 && z >= 0 && x < width && z < height) {
            gridArray[x, z] = value;
            TriggerGridObjectChanged(x, z);
        }
    }

    public void TriggerGridObjectChanged(int x, int z) {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        GetXZ(worldPosition, out int x, out int z);
        SetGridObject(x, z, value);
    }

    public TGridObject GetGridObject(int x, int z) {
        if (x >= 0 && z >= 0 && x < width && z < height) {
            return gridArray[x, z];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        return new Vector2Int(
            Mathf.Clamp(gridPosition.x, 0, width - 1),
            Mathf.Clamp(gridPosition.y, 0, height - 1)
        );
    }

    private void LoadBlockedCells() {
        blockedCells.Add((9,0));

        blockedCells.Add((0,1));
        blockedCells.Add((1,1));
        blockedCells.Add((2,1));
        blockedCells.Add((3,1));
        blockedCells.Add((4,1));
        blockedCells.Add((5,1));
        blockedCells.Add((7,1));
        blockedCells.Add((8,1));
        blockedCells.Add((9,1));

        blockedCells.Add((1,2));
        blockedCells.Add((2,2));
        blockedCells.Add((3,2));
        blockedCells.Add((5,2));
        blockedCells.Add((7,2));
        blockedCells.Add((9,2));

        blockedCells.Add((1,3));
        blockedCells.Add((3,3));
        blockedCells.Add((4,3));
        blockedCells.Add((5,3));
        blockedCells.Add((7,3));
        blockedCells.Add((9,3));

        blockedCells.Add((1,4));
        blockedCells.Add((7,4));
        blockedCells.Add((9,4));

        blockedCells.Add((1,5));
        blockedCells.Add((3,5));
        blockedCells.Add((4,5));
        blockedCells.Add((5,5));
        blockedCells.Add((7,5));
        blockedCells.Add((9,5));

        blockedCells.Add((0,6));
        blockedCells.Add((1,6));
        blockedCells.Add((3,6));
        blockedCells.Add((4,6));
        blockedCells.Add((5,6));
        blockedCells.Add((7,6));
        blockedCells.Add((9,6));

        blockedCells.Add((0,7));
        blockedCells.Add((7,7));
        blockedCells.Add((9,7));

        blockedCells.Add((0,8));
        blockedCells.Add((1,8));
        blockedCells.Add((2,8));
        blockedCells.Add((3,8));
        blockedCells.Add((4,8));
        blockedCells.Add((5,8));
        blockedCells.Add((6,8));
        blockedCells.Add((7,8));
        blockedCells.Add((8,8));
        blockedCells.Add((9,8));

        blockedCells.Add((0,9));
        blockedCells.Add((1,9));
        blockedCells.Add((2,9));
        blockedCells.Add((3,9));
        blockedCells.Add((4,9));
        blockedCells.Add((5,9));
        blockedCells.Add((6,9));
        blockedCells.Add((7,9));
        blockedCells.Add((8,9));
        blockedCells.Add((9,9));
    }
}