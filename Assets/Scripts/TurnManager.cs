using System;
using UnityEngine;

public class TurnManager 
{
    private int tick;
    public event Action OnTick;
    public TurnManager()
    {
        tick = 1;
    }

    public void Tick()
    {
        tick++;
        OnTick?.Invoke();
        
    }

}
