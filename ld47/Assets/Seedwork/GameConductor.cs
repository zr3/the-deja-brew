﻿using System.Collections;
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
    void OnMainMenuStart()
    {
        MusicBox.ChangeMusic((int)Song.Intro);
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
        var initialState = new IntroState();
        StartCoroutine(stateMachine(initialState));
    }
}

public static class SongExtensions
{
    public static int ToInt(this Song song) => (int)song;
}
