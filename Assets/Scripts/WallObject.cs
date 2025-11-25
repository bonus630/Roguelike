using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
    [SerializeField] Tile[] wallTiles;
    [SerializeField] int MaxHearth = 3;
    int hearth;
    Tile originalTile;
    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        hearth = MaxHearth;


        originalTile = GameManager.Instance.BoardManager.GetCellTile(cell);
        GameManager.Instance.BoardManager.SetCellTile(cell, wallTiles[Random.Range(0, wallTiles.Length)]);
    }
    public override bool PlayerWantsToEnter()
    {
        Debug.Log("heart" + hearth);
        hearth--;
        if (hearth <= 0)
        {
            GameManager.Instance.BoardManager.SetCellTile(cell, originalTile);
            Destroy(gameObject);
            return true;
        }
        return false;
    }

}
