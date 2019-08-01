using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUpdateManager : MonoBehaviour
{
    [SerializeField] GameTimeManager gameTimeManager;
    [SerializeField] GamePlayData gamePlayData;

    public void Awake()
    {
        gameTimeManager.OnUpdate += OnUpdate;
    }

    private void OnUpdate(double deltaTime)
    {
        for (int i = 0; i < gamePlayData.citizens.Count; i++)
        {
            gamePlayData.citizens[i].Update(deltaTime);
        }
    }
}
