using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    BoardManager boardManager;
    Vector2Int cellPosition;

    public void Spawn(BoardManager boardManager, Vector2Int cellPosition)
    {
        this.boardManager = boardManager;
        MoveTo(cellPosition);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newPlayerCell = cellPosition;
        bool hasMove = false;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newPlayerCell.y += 1;
            hasMove = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newPlayerCell.y -= 1;
            hasMove = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newPlayerCell.x -= 1;
            hasMove = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newPlayerCell.x += 1;
            hasMove = true;
        }
        if (hasMove)
        {
            BoardManager.CellData cell = boardManager.GetCellData(newPlayerCell);
            if (cell != null && cell.Passable)
            {
                GameManager.Instance.turnManager.Tick();
                if(cell.ContainedObject ==null)
                    MoveTo(newPlayerCell);
                else if (cell.ContainedObject.PlayerWantsToEnter())
                {
                    MoveTo(newPlayerCell);
                    cell.ContainedObject.PlayerEntered();
                }
            }
        }

    }

    private void MoveTo(Vector2Int newPlayerCell)
    {
        cellPosition = newPlayerCell;
        transform.position = boardManager.CellToWord(cellPosition);
    }
}
