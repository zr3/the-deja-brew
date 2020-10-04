using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroState : IState
{
    private GameObject _daycard;
    public IntroState(GameObject daycard)
    {
        _daycard = daycard;
    }
    public IState NextState { get; private set; }

    public void OnEnter()
    {
        GameConductor.CameraStateTrigger("Initialize");
        MusicBox.ChangeMusic(Song.Game.ToInt());
        GameConductor.ResetStates();
        GameConductor.AdvanceDay();
    }

    public IEnumerator OnUpdate()
    {
        GameConductor.ResetPlayer();
        GameConductor.UnfreezePlayer();
        GameConductor.PerformScheduleCallbacks(11, 0);
        _daycard.SetActive(true);
        _daycard.GetComponentInChildren<Text>().text = DataDump.Get<string>("Day") + " evening";
        yield return new WaitForSeconds(4);
        _daycard.SetActive(false);
        bool isFinished = false;
        ScreenFader.FadeInThen(() =>
        {
            GameConductor.FreezePlayer();
            MessageController.AddMessage("Hmm. Haven't seen you around before.", postAction: () => GameConductor.CameraStateTrigger("NextState"));
            MessageController.AddMessage("Welcome to The Deja Brew.");
            MessageController.AddMessage("... oh? You need a place to stay?");
            MessageController.AddMessage("We got a room in the back when you're ready to hit the hay.");
            MessageController.AddMessage("Literally -- it's all we got.", postAction: () =>
            {
                GameConductor.CameraStateTrigger("FocusPlayer");
                GameConductor.UnfreezePlayer();
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
