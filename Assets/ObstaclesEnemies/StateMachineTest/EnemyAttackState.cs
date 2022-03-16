using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("I have attacked lmao gottem");
        return this;
    }
}
