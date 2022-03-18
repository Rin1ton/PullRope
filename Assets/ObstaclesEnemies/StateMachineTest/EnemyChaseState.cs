using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : State
{
    public EnemyNeutralState neutralState;
    public EnemyBehavior behav;
    public EnemyAttackState attackState;
    public bool isInAttackRange;
    public override State RunCurrentState()
    {
        
        if (behav.distance >= 1 && behav.distance <=2)
        {
            
            isInAttackRange = true;
            Debug.Log("is in attack range");
        }
        else
        {
            isInAttackRange = false;
            Debug.Log("chase");
            behav._agent.SetDestination(behav.Player.transform.position);
        }
        
        if (behav.distance > 4)
        {
            
            behav._agent.isStopped = true;
            Debug.Log("stopping and switch to neutral");
            return neutralState;
        }
        else
        {
            Debug.Log("keep chasing");
        }
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
