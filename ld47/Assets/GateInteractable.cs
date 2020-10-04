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
            // todo: shwink sound and animations
            GameConductor.EnqueueReset(() =>
            {
                Gate.transform.Translate(0, 10, 0);
                Curtain.transform.Translate(0, 10, 0);
            });
            Gate.transform.Translate(0, -10, 0);
            Curtain.transform.Translate(0, -10, 0);
            ScreenFader.FadeOutThen(() =>
            {
                GameConductor.IsOblexDead = true;
            }, 1);
        }
    }
}
