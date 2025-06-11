using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public class Cell
    {
        public int x;
        public int y;
        public int z;
        public List<Cell> neighbours = new();

        public Cell(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;
    [SerializeField] private int gridSizeZ = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private LayerMask obstacleMask; // Select obstacle layers in the Inspector
    [SerializeField] private float drawGizmoColor = 0.5f;

    private Cell[,,] cells;

    private void Awake()
    {
        cells = new Cell[gridSizeX, gridSizeY, gridSizeZ];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    cells[x, y, z] = new Cell(x, y, z);
                }
            }
        }

        AddNeighbours();
    }

    private void AddNeighbours()
    {
        int[] dx = { -1, 0, 1 };
        int[] dy = { -1, 0, 1 };
        int[] dz = { -1, 0, 1 };

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Cell current = cells[x, y, z];
                    current.neighbours.Clear();

                    for (int ix = 0; ix < 3; ix++)
                    {
                        for (int iy = 0; iy < 3; iy++)
                        {
                            for (int iz = 0; iz < 3; iz++)
                            {
                                int nx = x + dx[ix];
                                int ny = y + dy[iy];
                                int nz = z + dz[iz];

                                // Skip self
                                if (nx == x && ny == y && nz == z)
                                    continue;

                                // Only consider direct neighbors (8 in 3D: all adjacent, not diagonals)
                                int diff = Mathf.Abs(nx - x) + Mathf.Abs(ny - y) + Mathf.Abs(nz - z);
                                if (diff != 1)
                                    continue;

                                if (nx >= 0 && nx < gridSizeX &&
                                    ny >= 0 && ny < gridSizeY &&
                                    nz >= 0 && nz < gridSizeZ)
                                {
                                    current.neighbours.Add(cells[nx, ny, nz]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, drawGizmoColor);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 cellPosition = transform.position + new Vector3(x, y, z) * cellSize;
                    // Only draw if not inside an obstacle
                    bool isBlocked = Physics.CheckBox(cellPosition, Vector3.one * 0.5f * cellSize, Quaternion.identity);
                    if (!isBlocked)
                    {
                        Gizmos.DrawWireCube(cellPosition, Vector3.one * cellSize * 0.9f);
                    }
                }
            }
        }
    }
}
