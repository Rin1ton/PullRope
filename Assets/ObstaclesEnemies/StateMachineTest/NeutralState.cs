using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralState : State
{
    public ChaseState chaseState;
    public bool canSeePlayer;
    public override State RunCurrentState()
    {
        if (canSeePlayer)
        {
            return chaseState;
        }
        
        else
        {
            return this;
        }
    }
}
