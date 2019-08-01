using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임세계의 시간을 시뮬레이션 합니다.
/// </summary>
public class GameTimeManager : MonoBehaviour
{
    private bool _isPlaying;
    public bool IsPlaying
    {
        get
        {
            return _isPlaying;
        }
        set
        {
            if(_isPlaying != value)
            {
                _isPlaying = value;

                if (_isPlaying)
                {
                    double realtime = Time.realtimeSinceStartup;
                    prevRealtimeSinceStartup = realtime;

                    Time.timeScale = TimeScale;
                }
                else
                {

                }

                if (OnChangeIsPlaying != null) OnChangeIsPlaying(value);
            }
        }
    }
    public Action<bool> OnChangeIsPlaying;

    private float _TimeScale;
    public float TimeScale
    {
        get
        {
            return _TimeScale;
        }
        set
        {
            _TimeScale = value;
            if(IsPlaying) Time.timeScale = value;
        }
    }

    private double _GameTime;
    public double GameTime
    {
        get
        {
            return _GameTime;
        }
        set
        {
            _GameTime = value;
        }
    }

    [Header("Time Setting")]
    [SerializeField] uint YearNeedMonth = 12;
    [SerializeField] double MonthNeedTime = 300;

    private double DefaultGameTime;

    public uint CurrYear { get; set; }
    public uint CurrMonth { get; set; }
    public double CurrDayTime { get; set; }

    public float CurrDayRate
    {
        get
        {
            return (float)(CurrDayTime / MonthNeedTime);
        }
    }

    //월 12개 4계절 초봄 봄 초여름 여름 초가을 가을 초겨울 겨울 
    //3  4  5  봄
    //6  7  8  여름
    //9  10 11 가을
    //12 1  2  겨울


    private void Awake()
    {
        this.IsPlaying = false;
        this.TimeScale = 1;

        CurrYear = 1;
        CurrMonth = 3;
        CurrDayTime = 0;

        DefaultGameTime = CurrDayTime;
        DefaultGameTime += CurrMonth * MonthNeedTime;
        DefaultGameTime += CurrYear * MonthNeedTime * YearNeedMonth;

    }

    double prevRealtimeSinceStartup;

    public Action<double> OnUpdate;

    void Update()
    {
        if (IsPlaying)
        {
            double realtime = Time.realtimeSinceStartup;
            double deltaTime = realtime - prevRealtimeSinceStartup;
            deltaTime *= TimeScale;
            prevRealtimeSinceStartup = realtime;

            GameTime += deltaTime;

            if (OnUpdate != null) OnUpdate(deltaTime);

            //GameTime Calc
            double gameTime = DefaultGameTime + GameTime;
            uint month = (uint)(gameTime / MonthNeedTime);
            CurrDayTime = gameTime - (month * MonthNeedTime);
            CurrYear = (uint)(month / YearNeedMonth);
            CurrMonth = month % YearNeedMonth;
        }

    }
}



