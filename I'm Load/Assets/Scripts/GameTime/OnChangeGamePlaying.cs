using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnChangeGamePlaying : RequireAwakeBehaviour
{
    [SerializeField] GameTimeManager gameTimeManager;

    [Serializable] public class Event_ChangeGamePlaying : UnityEvent { }
    public Event_ChangeGamePlaying ChangeGamePlayingTrue;
    public Event_ChangeGamePlaying ChangeGamePlayingFalse;

   
    private void Awake()
    {
        gameTimeManager.OnChangeIsPlaying += OnChangeIsPlaying;
    }

    private void OnChangeIsPlaying(bool isPlaying)
    {
        if (isPlaying)
        {
            if(ChangeGamePlayingTrue != null)
                ChangeGamePlayingTrue.Invoke();
        }
        else
        {
            if (ChangeGamePlayingFalse != null)
                ChangeGamePlayingFalse.Invoke();
        }
    }
}
