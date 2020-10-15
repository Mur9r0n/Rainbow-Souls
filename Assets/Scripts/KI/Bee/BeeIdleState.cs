using UnityEngine;

public class BeeIdleState : ABaseState
{
    public override bool Enter()
    {
        //TODO Adjustments todo
        Debug.Log("Hier wäre die Update! BeeIdleState");
        return base.Enter();
    }

    public override void Update()
    {

    }
}
