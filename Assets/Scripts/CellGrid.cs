using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public GameObject test;
    public int ColumnLength = 3;
    public int RowHeight = 10;

    public float timer = 2;
    private float initialTimer;

    public Cell[,] grid;
    // Use this for initialization
    void Start()
    {
        initialTimer = timer;

        grid = new Cell[ColumnLength, RowHeight];

        for (int i = 0; i < ColumnLength; i++)
        {
            for (int j = 0; j < RowHeight; j++)
            {
                grid[i, j] = Instantiate(test, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity).AddComponent<Cell>();
            }
        }

        Camera.main.transform.position = new Vector3(ColumnLength / 2, RowHeight / 2, -10);
        Camera.main.orthographicSize = RowHeight / 2;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;
            if (Physics.Raycast(ray, out info, Mathf.Infinity))
            {
                var cell = info.collider.GetComponent<Cell>();
                cell.SetAlive(!cell.alive);
            }
        }


        if (Input.GetKey(KeyCode.E))
        {
            for (int i = 0; i < ColumnLength; i++)
            {
                for (int j = 0; j < RowHeight; j++)
                {
                    grid[i, j].GetComponent<Cell>().aliveNeighbors = 0;

                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            if (i + x > 0 && i + x < ColumnLength && j + y >= 0 && j + y < RowHeight && grid[i + x, j + y].GetComponent<Cell>().alive && grid[i + x, j + y] != grid[i, j].GetComponent<Cell>())
                            {
                                grid[i, j].GetComponent<Cell>().aliveNeighbors++;
                            }
                        }
                    }
                    Debug.Log(grid[i, j].GetComponent<Cell>().aliveNeighbors);
                }
            }

            for (int i = 0; i < ColumnLength; i++)
            {
                for (int j = 0; j < RowHeight; j++)
                {
                    if (grid[i, j].alive)
                    {
                        grid[i, j].tempAlive = grid[i, j].aliveNeighbors == 2 || grid[i, j].aliveNeighbors == 3;
                    }
                    else
                    {
                        grid[i, j].tempAlive = grid[i, j].aliveNeighbors == 3;
                    }

                    Debug.Log(grid[i, j].tempAlive);
                }
            }

            for (int i = 0; i < ColumnLength; i++)
            {
                for (int j = 0; j < RowHeight; j++)
                {
                    grid[i, j].SetAlive(grid[i, j].tempAlive);
                }
            }
        }
    }
}

