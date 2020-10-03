using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : IState
{
    public IState NextState { get; private set; }

    public void OnEnter()
    {
        GameConductor.CameraStateTrigger("Initialize");
        MusicBox.ChangeMusic(Song.Game.ToInt());
    }

    public IEnumerator OnUpdate()
    {
        yield return new WaitForSeconds(1);
        bool isFinished = false;
        ScreenFader.FadeInThen(() =>
        {
            Juicer.ShakeCamera(0.5f);
            MessageController.AddMessage("Hi! Haven't seen you around before.");
            MessageController.AddMessage("Welcome to The Deja Brew.");
            MessageController.AddMessage("... oh? You need a place to stay?");
            MessageController.AddMessage("We got a room in the back when you're ready to hit the hay.");
            MessageController.AddMessage("Literally -- it's all we got.", postAction: () =>
            {
                GameConductor.CameraStateTrigger("FocusPlayer");
                isFinished = true;
            });
            MessageController.AddMessage("Enjoy the band and meet some of the regulars. We usually get a few characters in here.");
        });
        do
        {
            yield return new WaitForSeconds(1);
        } while (!isFinished);
    }

    public void OnExit()
    {
        // MainGameState is implemented as a scriptableobject, so get it from GameConductor
        NextState = GameConductor.GetScriptableGameStateOfType<MainGameState>();
    }
}
