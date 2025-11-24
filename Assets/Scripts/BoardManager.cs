using UnityEngine.Tilemaps;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public class CellData :  MonoBehaviour
    {
        public bool Passable { get; set; }
    }
    public int width;
    public int height;
    public Tile[] groundTiles;
    public Tile[] wallTiles;
    private CellData[,] boardData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boardData = new CellData[width, height];
        Tilemap tileMap = GetComponentInChildren<Tilemap>();
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Tile tile;
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    boardData[x, y].Passable = false;
                    tile = wallTiles[Random.Range(0, wallTiles.Length)];
                }
                else
                {
                    boardData[x, y].Passable = true;
                    tile = groundTiles[Random.Range(0, groundTiles.Length)];
                }
                    tileMap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
