using UnityEngine;

public class Grid : MonoBehaviour
{
    public class Cell
    {
        public int x;
        public int y;
        public int z;

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 cellPosition = transform.position + new Vector3(x, y, z) * cellSize;
                    // Only draw if not inside an obstacle
                    bool isBlocked = Physics.CheckSphere(cellPosition, cellSize * 0.4f);
                    if (!isBlocked)
                    {
                        Gizmos.DrawWireCube(cellPosition, Vector3.one * cellSize * 0.9f);
                    }
                }
            }
        }
    }
}
