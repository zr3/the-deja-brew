using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomDoor : Interactable
{
    public override void OnSecondaryActionFinished()
    {
        GameConductor.IsSleeping = true;
        GameConductor.FreezePlayer();
        // todo: play sound
    }
}
