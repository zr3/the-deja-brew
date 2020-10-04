using System.Collections;
using UnityEngine;

public class CreditsState : IState
{
    public IState NextState { get; private set; }

    public void OnEnter() {
        ScreenFader.FadeOut();
        GameConductor.FreezePlayer();
        MusicBox.ChangeMusic((int)Song.Final);
        MusicBox.Instance.FadeInTrack(1);
        CreditRoller.RollCredits();
    }

    public IEnumerator OnUpdate()
    {
        while (true)
        {
            yield return null;
        }
    }

    public void OnExit()
    {
    }
}
