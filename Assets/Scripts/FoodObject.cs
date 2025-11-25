using UnityEngine;

public class FoodObject : CellObject
{
    public int foodGranted = 10;
    public override void PlayerEntered()
    {
        GameManager.Instance.ChangeFood(foodGranted);
        Destroy(gameObject);

    }
}
