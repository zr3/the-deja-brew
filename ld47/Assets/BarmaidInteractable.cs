using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarmaidInteractable : Interactable
{
    public Transform TargetQuitLocation;
    public override void OnPrimaryActionFinished()
    {
        GameConductor.IsMaidAQuitter = true;
        GameConductor.EnqueueReset(() => GameConductor.IsMaidAQuitter = false);
        transform.parent.position = TargetQuitLocation.position;
    }
}
