using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Song
{
    Intro,
    Game,
    Boss,
    Final
}

public partial class GameConductor : MonoBehaviour
{
    public static int PlayerFrozen = 0;
    public GameObject DayCard;
    public List<GameObject> NPCs;
    public static List<GameObject> GetNPCs=> _instance.NPCs;
    public List<GameObject> BandNPCs;
    public static List<GameObject> GetBandNPCs => _instance.BandNPCs;
    public GameObject OblexBody;
    public static GameObject GetOblexBody => _instance.OblexBody;
    public GameObject Gate;
    public static GameObject GetGate => _instance.Gate;
    public GameObject Curtain;
    public static GameObject GetCurtain => _instance.Curtain;
    public static int NPCsKilled = 0;
    public static void KillNPC()
    {
        NPCsKilled++;
    }
    public static GameObject GetDayCard()
    {
        return _instance.DayCard;
    }
    void OnMainMenuStart()
    {
        MusicBox.PlayAmbience(0);
    }

    void OnGameStart()
    {
        IEnumerator stateMachine(IState state)
        {
            do
            {
                Debug.Log($"Entering state {state.GetType()}");
                DataDump.Set("StateTitle", state.GetType().ToString());
                state.OnEnter();
                yield return state.OnUpdate();
                Debug.Log($"Exiting state {state.GetType()}");
                state.OnExit();
                state = state.NextState;
            } while (state != null);
        }
        var initialState = new IntroState(DayCard);
        StartCoroutine(stateMachine(initialState));
    }
    
    public static void FreezePlayer()
    {
        PlayerFrozen++;
    }

    public static void UnfreezePlayer()
    {
        PlayerFrozen--;
        if (PlayerFrozen < 0)
        {
            PlayerFrozen = 0;
        }
    }

    public static bool IsPlayerFrozen => PlayerFrozen > 0;

    /// <summary>
    /// These should be added every state loop
    /// </summary>
    public static Queue<Action> StateResets = new Queue<Action>();
    public static void EnqueueReset(Action action)
    {
        StateResets.Enqueue(action);
    }
    public static void ResetStates()
    {
        while (StateResets.Any())
        {
            StateResets.Dequeue().Invoke();
        }
    }
    /// <summary>
    /// These should only be added once per app load, not every state loop
    /// </summary>
    public static HashSet<Action<int, int>> ScheduleCallbacks = new HashSet<Action<int, int>>();
    public static void AddScheduleCallback(Action<int, int> action)
    {
        ScheduleCallbacks.Add(action);
    }
    public static void PerformScheduleCallbacks(int hour, int min)
    {
        foreach (var action in ScheduleCallbacks)
        {
            action.Invoke(hour, min);
        }
    }

    public static bool IsSleeping = false;

    private static Queue<string> _days = new Queue<string>(new[] {
        "Friday",
        "Saturday",
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday"
    });
    public static void AdvanceDay()
    {
        string nextDay = _days.Dequeue();
        DataDump.Set("Day", nextDay);
        _days.Enqueue(nextDay);
    }
    public static bool IsMaidAQuitter = false;
    public static bool IsCrowd1Drinking = false;
    public static bool IsCrowd2Drinking = false;
    public static bool IsCrowd3Drinking = false;
    public static bool IsBarflyHelping = false;
    public static bool IsPlayerAWarrior = false;
    public static bool IsOblexRevealed = false;
    public static bool IsOblexTriggered = false;
}

public static class SongExtensions
{
    public static int ToInt(this Song song) => (int)song;
}
