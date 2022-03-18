using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNeutralState : State
{
    public EnemyBehavior behav;
    public EnemyChaseState chaseState;
    public bool canSeePlayer;
    public override State RunCurrentState()
    {
        
        if (behav.distance <= 3)
        {
            canSeePlayer = true;
            
        }
        else
        {
            canSeePlayer = false;
        }
        
        if (canSeePlayer)
        {
            Debug.Log("In see range");
            Debug.Log("Switching to chase state");
            return chaseState;
        }
        
        else
        {
            return this;
        }
    }
}
