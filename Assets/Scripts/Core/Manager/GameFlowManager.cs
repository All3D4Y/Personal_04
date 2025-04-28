using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    PlayState currentState;
    Dictionary<Type, PlayState> states;

    void Start()
    {
        states = new Dictionary<Type, PlayState>
        {
            { typeof(StageLoad), new StageLoad(this) },
            { typeof(Setup), new Setup(this) },
            { typeof(Playing), new Playing(this) },
            { typeof(Result), new Result(this) }
        };
    }

    void Update()
    {
        currentState?.Update();
    }

    public void FlowStart()
    {
        ChangeState<StageLoad>();
    }

    public void ChangeState<T>() where T : PlayState
    {
        if (currentState != null)
            currentState.Exit();

        currentState = states[typeof(T)];
        currentState.Enter();
    }

    public void ChangeState(Type stateType)
    {
        if (!typeof(PlayState).IsAssignableFrom(stateType))
        {
            Debug.LogError($"Invalid state type: {stateType}");
            return;
        }

        if (currentState != null)
            currentState.Exit();

        currentState = states[stateType];
        currentState.Enter();
    }
}
