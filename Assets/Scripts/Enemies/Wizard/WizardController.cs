using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : GroundEnemy
{
    public override void Start()
    {
        base.Start();
        AsignAnimation("Idle", "Attack", "Run");
    }
}
