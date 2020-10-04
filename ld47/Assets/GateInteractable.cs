using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInteractable : Interactable
{
    public GameObject Gate;
    public GameObject Curtain;

    public override void OnSecondaryActionFinished()
    {
        if (GameConductor.IsPlayerAWarrior)
        {
            GameConductor.IsOblexTriggered = true;
        }
    }
}
