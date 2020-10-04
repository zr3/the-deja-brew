using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractable : Interactable
{
    public GameObject playerSword;

    public override void OnPrimaryActionFinished()
    {
        GameConductor.IsPlayerAWarrior = true;
        Juicer.PlaySound(1);
        playerSword.SetActive(true);
        GameConductor.EnqueueReset(() =>
        {
            playerSword.SetActive(false);
            GameConductor.IsPlayerAWarrior = false;
            transform.parent.Translate(0, 10, 0);
        });
        transform.parent.Translate(0, -10, 0);
    }
}
