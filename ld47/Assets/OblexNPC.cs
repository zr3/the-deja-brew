using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OblexNPC : Interactable
{
    public GameObject OblexEffect;
    public Interactable OldInteractable;
    public void Activate()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        OblexEffect.SetActive(true);
        OldInteractable.GetComponent<CapsuleCollider>().enabled = false;
        OldInteractable.enabled = false;
    }
    public override void OnPrimaryActionFinished()
    {
        Juicer.ShakeCamera(0.5f);
        GameConductor.KillNPC();
        transform.parent.parent.Translate(0, -10, 0);
    }
}
