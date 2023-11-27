using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : GroundEnemy
{
    public override void Start()
    {
        base.Start();
        AsignAnimation("idle", "Attack", "Run");
    }
}
