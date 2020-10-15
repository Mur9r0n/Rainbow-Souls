using UnityEngine;

public class MushroomIdleState : ABaseState
{
    public override bool Enter()
    {
        //TODO Adjustments todo
        Debug.Log("Hier wäre die Update! MushroomIdleState");
        return base.Enter();
    }

    public override void Update()
    {

    }
}
