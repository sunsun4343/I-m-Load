using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Citizen
{
    public GamePlayData gamePlayData { get; }

    public string name;
    public bool sex;
    public byte age;
    public JobDB job;

    public Vector2 position;

    public ItemDB toolItemDB;
    public uint toolDurability;

    public Building_House resident_house;

    public float hunger;
    public float energy;
    public float health;


    //건강, 행복, 교육, 의류
    //배고픔, 갈증, 추위, 질병,
    //결혼

    public Citizen(GamePlayData gamePlayData)
    {
        this.gamePlayData = gamePlayData;
    }

    public void InitState()
    {
        hunger = 500;
        energy = 1000;

    }

    public void Update(double deltaTime)
    {
        //상태변화
        Update_Hunger(deltaTime);

        if(currentAction == ActionType.Rest)
        {
            //행동 목록 검출
            List<ActionType> actionTypes = new List<ActionType>();

            //배고픔 -> 음식찾기
            if (hunger < 100) { actionTypes.Add(ActionType.PickUpFood); }

            //도구없음, 창고에 도구있음 -> 도구찾기
            if (toolItemDB == null && gamePlayData.isStockItem(ItemDB.Category.Tool)) { actionTypes.Add(ActionType.PickUpTool); }

            //행동결정
            if(actionTypes.Count > 0)
            {
                actionTypes.Sort();
                ChangeActionType(actionTypes[0]);
            }
        }

        //행동 실행
        if (citizenAction != null)
        {
            bool complete = citizenAction.ExcuteAction(deltaTime);
            if(complete)
            {
                currentAction = ActionType.Rest;
                citizenAction = null;
            }
        }
    }

    #region Update State

    private void Update_Hunger(double deltaTime)
    {
        float deltaEnergy = (float)deltaTime;

        if (hunger >= deltaEnergy)
        {
            hunger -= deltaEnergy;
        }
        else
        {
            deltaEnergy -= hunger;
            hunger = 0;
            energy -= Math.Abs(deltaEnergy);
        }

        //TODO health -

        if (energy <= 0)
        {
            //Die
            Debug.Log("Die");
        }

    }

    #endregion

    #region Action

    public enum ActionType
    {
        Sleep,
        PickUpFood,
        PickUpTool,
        Rest,
    }

    [SerializeField] ActionType currentAction = ActionType.Rest;
    [SerializeField] CitizenAction citizenAction;

    private void ChangeActionType(ActionType currentAction)
    {
        this.currentAction = currentAction;

        switch (currentAction)
        {
            case ActionType.Sleep:

                break;
            case ActionType.PickUpFood:

                break;
            case ActionType.PickUpTool: citizenAction = new CitizenAction_PickUpTool(this); break;
            case ActionType.Rest:

                break;
        }
    }



    #endregion

}
