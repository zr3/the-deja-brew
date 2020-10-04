using Cinemachine;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "MainGameStateState.asset", menuName = "peanutbutters/MainGameState", order = 20)]
public class MainGameState : ScriptableObject, IState
{
    public IState NextState { get; private set; }
    // 90 secs then stop
    // 3 hours, 30 secs per hour
    // 2 mins / second
    // 10 mins / 5 real seconds
    private float time = 0;
    private int niceHour = 11;
    private int niceMinute = 0;

    public void OnEnter() {
        GameConductor.SetShowHud(true);
        time = 0;
        niceHour = 11;
        niceMinute = 0;
        SetTime();
    }

    private void SetTime()
    {
        string minutePrefix = niceMinute < 10 ? "0" : "";
        DataDump.Set("Time", $"{niceHour}:{minutePrefix}{niceMinute}");
        GameConductor.PerformScheduleCallbacks(niceHour, niceMinute);
    }

    public IEnumerator OnUpdate()
    {
        while (!GameConductor.IsOblexTriggered && !GameConductor.IsSleeping) {
            time += 1;
            if (time >= 5 && niceHour != 2)
            {
                time = 0;
                niceMinute += 10;
                if (niceMinute == 60)
                {
                    niceMinute = 0;
                    niceHour += 1;
                    if (niceHour == 13)
                    {
                        niceHour = 1;
                    }
                }
                SetTime();
            }
            yield return new WaitForSeconds(1);
        }
        GameConductor.SetShowHud(false);
        yield return new WaitForSeconds(1);
    }

    public void OnExit()
    {
        if (GameConductor.IsSleeping)
        {
            ScreenFader.FadeOut();
            NextState = new IntroState(GameConductor.GetDayCard());
            GameConductor.IsSleeping = false;
        } else
        {
            NextState = new OblexState();
        }
    }
}
