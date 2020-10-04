using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OblexState : IState
{
    public IState NextState { get; private set; }

    public void OnEnter() {
    }

    public IEnumerator OnUpdate()
    {
        GameConductor.FreezePlayer();
        GameConductor.CameraStateTrigger("OblexCam");
        yield return new WaitForSeconds(2);
        // todo: shwink sound and animations
        GameConductor.GetGate.transform.Translate(0, -10, 0);
        Juicer.ShakeCamera(0.1f);
        yield return new WaitForSeconds(1);
        GameConductor.CameraStateTrigger("NextState");
        Juicer.ShakeCamera(1);
        yield return new WaitForSeconds(1);
        GameConductor.GetCurtain.transform.Translate(0, -10, 0);
        foreach(var npc in GameConductor.GetBandNPCs)
        {
            npc.transform.Translate(0, -10, 0);
        }
        GameConductor.GetOblexBody.SetActive(true);
        foreach (var npc in GameConductor.GetNPCs)
        {
            npc.GetComponentInChildren<OblexNPC>()?.Activate();
        }
        // todo: disable doors
        GameConductor.IsOblexRevealed = true;
        yield return new WaitForSeconds(3);
        GameConductor.UnfreezePlayer();
        GameConductor.CameraStateTrigger("FocusPlayer");
        while (GameConductor.NPCsKilled < 11) { yield return null; }
    }

    public void OnExit() {
        NextState = new CreditsState();
    }
}
