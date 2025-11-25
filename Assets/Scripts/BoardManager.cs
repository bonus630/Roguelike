using UnityEngine.Tilemaps;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public class CellData
    {
        public bool Passable { get; set; }
        public CellObject ContainedObject;
    }
    public int width;
    public int height;
    public Tile[] groundTiles;
    public Tile[] wallTiles;
    public FoodObject[] foodObjects;
    public WallObject[] wallObjects;
    private CellData[,] boardData;
    private Grid grid;
    private List<Vector2Int> emptyCells = new();
    Tilemap tileMap;

    public void Init()
    {
        boardData = new CellData[width, height];
        tileMap = GetComponentInChildren<Tilemap>();
        grid = GetComponent<Grid>();
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Tile tile;
                boardData[x, y] = new CellData();
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    boardData[x, y].Passable = false;
                    tile = wallTiles[Random.Range(0, wallTiles.Length)];
                }
                else
                {
                    boardData[x, y].Passable = true;
                    tile = groundTiles[Random.Range(0, groundTiles.Length)];
                    emptyCells.Add(new Vector2Int(x, y));
                }
                tileMap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        emptyCells.Remove(new Vector2Int(1, 1));
        GenerateFoods();
        GenerateWalls();

    }
    void GenerateFoods()
    {
        int foodsCount = 5;
        Generate<FoodObject>(foodsCount, foodObjects);
    }
    void GenerateWalls()
    {
        int wallsCount = Random.Range(6, 10);
        Generate<WallObject>(wallsCount, wallObjects);
    }
    void Generate<T>(int amount, T[] values) where T : CellObject
    {
        for (int i = 0; i < amount; i++)
        {
            var cellData = TakeEmptyCellData(out Vector2Int coord);
            if (cellData.Passable && cellData.ContainedObject == null)
            {
                T go = Instantiate(values[Random.Range(0, values.Length)]);
                go.Init(coord);
                go.transform.position = CellToWord(coord);
                cellData.ContainedObject = go;
            }
        }
    }
    private CellData TakeEmptyCellData(out Vector2Int coord)
    {
        int p = Random.Range(0, emptyCells.Count);
        coord = emptyCells[p];
        var cellData = boardData[coord.x, coord.y];
        emptyCells.RemoveAt(p);
        return cellData;
    }
    public Vector3 CellToWord(Vector2Int cellIndex)
    {
        return grid.CellToWorld((Vector3Int)cellIndex);
    }
    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.y < 0 || cellIndex.x >= width || cellIndex.y >= height)
            return null;
        return boardData[cellIndex.x, cellIndex.y];
    }
    public void SetCellTile(Vector2Int index, Tile tile)
    {
        tileMap.SetTile((Vector3Int)index, tile);
    }
    public Tile GetCellTile(Vector2Int cell)
    {
        return tileMap.GetTile<Tile>((Vector3Int)cell);
    }
    void Update()
    {

    }

}
