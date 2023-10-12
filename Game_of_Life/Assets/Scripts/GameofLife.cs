using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Game_of_LIfe : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameManager gameManager;
    Cell[,] cells;
    float cellSize = 0.5f;
    int numberofColums, numberofRows;
    int spawnChancePercentage = 25;

    public const bool Alive = true;
    public const bool Dead = false;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        numberofColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize);
        numberofRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);

        cells = new Cell[numberofColums, numberofRows];


        for (int y = 0; y < numberofRows; y++)
        {
            for (int x = 0; x < numberofColums; x++)
            {
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize *
                    Camera.main.aspect,
                    y * cellSize - Camera.main.orthographicSize);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<Cell>();

                if (Random.Range(0, 100) < spawnChancePercentage)
                {
                    cells[x, y].alive = Alive;
                }

                cells[x, y].UpdateStatus();
            }
        }
    }
    void Update()
    {
        if (!gameManager.isPaused)
        {
            UpdateCells();
            OnMouseClick();
        }
        else
        {
            OnMouseClick();
        }
    }

    void UpdateCells()
    {
        bool[,] newCellStates = new bool[numberofColums, numberofRows];

        for (int y = 0; y < numberofRows; y++)
        {
            for (int x = 0; x < numberofColums; x++)
            {
                int aliveNeighbors = CountAliveNeighbors(x, y);

                if (cells[x, y].alive)
                {
                    // Apply the Game of Life rules

                    // Rule 1: Any live cell with fewer than two live neighbors dies as if caused by underpopulation.
                    if (aliveNeighbors < 2)
                    {
                        newCellStates[x, y] = Dead;
                    }
                    // Rule 2: Any live cell with two or three live neighbors lives on to the next generation.
                    else if (aliveNeighbors == 2 || aliveNeighbors == 3)
                    {
                        newCellStates[x, y] = Alive;
                    }
                    // Rule 3: Any live cell with more than three live neighbors dies, as if by overpopulation.
                    else
                    {
                        newCellStates[x, y] = Dead;
                    }
                }
                else
                {
                    // Rule 4: Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                    if (aliveNeighbors == 3)
                    {
                        newCellStates[x, y] = Alive;
                    }
                    else
                    {
                        newCellStates[x, y] = Dead;
                    }
                }
            }
        }

        for (int y = 0; y < numberofRows; y++)
        {
            for (int x = 0; x < numberofColums; x++)
            {
                cells[x, y].alive = newCellStates[x, y];
                cells[x, y].UpdateStatus();
            }
        }
    }

    public void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int cellX = Mathf.FloorToInt((mousePosition.x + Camera.main.orthographicSize * Camera.main.aspect) / cellSize);
            int cellY = Mathf.FloorToInt((mousePosition.y + Camera.main.orthographicSize) / cellSize);

            if (cellX >= 0 && cellX < numberofColums && cellY >= 0 && cellY < numberofRows)
            {
                cells[cellX, cellY].alive = !cells[cellX, cellY].alive;
                cells[cellX, cellY].UpdateStatus();
            }
        }
    }

    int CountAliveNeighbors(int x, int y)
    {
        int count = 0;
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int i = 0; i < 8; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (newX >= 0 && newX < numberofColums && newY >= 0 && newY < numberofRows)
            {
                if (cells[newX, newY].alive)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void ClearAllCells()
    {
        for (int y = 0; y < numberofRows; y++)
        {
            for (int x = 0; x < numberofColums; x++)
            {
                cells[x, y].alive = Dead;
                cells[x, y].UpdateStatus();
            }
        }
    }
}