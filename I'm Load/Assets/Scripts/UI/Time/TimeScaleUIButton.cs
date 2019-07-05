using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleUIButton : MonoBehaviour
{
    [SerializeField] GameTimeManager gameTimeManager;

    [SerializeField] Text Text_TimeScale;

    public List<float> TimeScaleList;

    public int index;

    public void OnClick_TimeScale()
    {
        index++;
        if (index >= TimeScaleList.Count)
        {
            index = 0;
        }

        float timescale = TimeScaleList[index];

        gameTimeManager.TimeScale = timescale;

        Text_TimeScale.text = "x" + timescale;
    }
}
