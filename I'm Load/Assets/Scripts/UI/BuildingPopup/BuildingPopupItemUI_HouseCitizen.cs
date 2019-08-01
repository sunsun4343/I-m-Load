using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPopupItemUI_HouseCitizen : BuildingPopupItemUI
{
    [SerializeField] Text Text_Name;
    [SerializeField] Image Image_sex;
    [SerializeField] Text Text_Age;
    [SerializeField] Text Text_Job;

    [SerializeField] Sprite Sprite_Male;
    [SerializeField] Sprite Sprite_Female;

    public void InitUI(Citizen citizen)
    {
        Text_Name.text = citizen.name;
        Image_sex.sprite = citizen.sex ? Sprite_Male : Sprite_Female;
        Text_Age.text = citizen.age.ToString();
        Text_Job.text = Str.Get(citizen.job.labelKey);
    }
}
