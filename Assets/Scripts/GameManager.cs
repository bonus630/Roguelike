using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PlayerController player;
    [field: SerializeField]public BoardManager BoardManager { get;  set; }
    [SerializeField] UIDocument uiDoc;
    private int foodAmount = 100;
    private Label foodLabel;
    public TurnManager turnManager { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        BoardManager.Init();
        player.Spawn(BoardManager, new Vector2Int(1, 1));
        
        turnManager = new TurnManager();
        turnManager.OnTick += OnTickHappen;
        foodLabel = uiDoc.rootVisualElement.Q<Label>("FoodLabel");
        foodLabel.text = $"Food:{foodAmount}";
    }

    private void OnTickHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int foodAmount)
    {
        this.foodAmount += foodAmount;
        foodLabel.text = $"Food:{this.foodAmount}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
