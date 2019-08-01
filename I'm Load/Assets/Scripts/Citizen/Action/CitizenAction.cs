using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CitizenAction
{
    protected Citizen citizen;
    protected double excuteTime;

    public CitizenAction(Citizen citizen)
    {
        this.citizen = citizen;
    }

    public abstract bool ExcuteAction(double deltaTime);

}
