using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeInfoUI : MonoBehaviour
{
    [SerializeField] GameTimeManager gameTimeManager;

    [SerializeField] Text Text_Year;
    [SerializeField] Text Text_Month;
    [SerializeField] Image Image_Clock;

    void Update()
    {
        Text_Year.text = gameTimeManager.CurrYear + "년";
        Text_Month.text = gameTimeManager.CurrMonth + "월";
        float fill = gameTimeManager.CurrDayRate;
        Image_Clock.fillAmount = fill;
    }
}
