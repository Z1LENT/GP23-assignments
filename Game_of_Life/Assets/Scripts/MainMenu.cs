using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject cellPrefab;
    Cell[,] cells;
    float cellSize = 0.5f;
    int numberofColumns, numberofRows;
    int spawnChancePercentage = 25;

    public const bool Alive = true;
    public const bool Dead = false;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 16;

        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        numberofColumns = Mathf.FloorToInt(cameraWidth / cellSize);
        numberofRows = Mathf.FloorToInt(cameraHeight / cellSize);

        float startX = -cameraWidth / 2 + cellSize / 2;
        float startY = cameraHeight / 2 - cellSize / 2;

        cells = new Cell[numberofColumns, numberofRows];

        for (int y = 0; y < numberofRows; y++)
        {
            for (int x = 0; x < numberofColumns; x++)
            {
                float xPos = startX + x * cellSize;
                float yPos = startY - y * cellSize;
                Vector2 newPos = new Vector2(xPos, yPos);

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
        UpdateCells();
    }

    void UpdateCells()
    {
        bool[,] newCellStates = new bool[numberofColumns, numberofRows];

        for (int y = 0; y < numberofRows; y++)
        {
            for (int x = 0; x < numberofColumns; x++)
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
            for (int x = 0; x < numberofColumns; x++)
            {
                cells[x, y].alive = newCellStates[x, y];
                cells[x, y].UpdateStatus();
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

            if (newX >= 0 && newX < numberofColumns && newY >= 0 && newY < numberofRows)
            {
                if (cells[newX, newY].alive)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
