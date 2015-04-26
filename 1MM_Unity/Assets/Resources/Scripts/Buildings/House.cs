﻿using UnityEngine;
using System.Collections;

public class House : ABuilding {

    public float housePower;

    public House()
    {
        name = "House";
        shortDescription = "Houses increase the maximum number of citizens you can have by 300. ";
        longDescription = "Houses increase the maximum number of citizens you can have by 300. Houses can also be linked to other houses for an additional 150 Citizens.";
        flavorText = "You must construct additional pylons!";
        foodCost = 40;
        moneyCost = 2;
        buildingMaterialsCost = 20;
        housePower = GameResources.instance.houseCitizensT;
    }

    public override void Initialize(int i)
    {
        base.Initialize(i);
        GameResources.instance.maximumCitizens += housePower;
    }

    public override void Effect()
    {

    }
}
