using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[Serializable]
public class PriceTag
{
    public float woodPrice, stonePrice, foodPrice;
    
}
[Serializable]
public class BuildingInfo
{
    public int id, level=0;
    public float yRot = 0;
    public int connectedGridID; 
}
public class Building : MonoBehaviour
{
    public BuildingInfo info;

    public PriceTag priceTag;

    public string objectName;

    public bool placed;

    public int baseResourcesGained = 1;

    private Resources resources;
	// Use this for initialization
	void Awake ()
	{
	    resources = FindObjectOfType<Resources>();
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!placed) 
	        return;
	    switch (info.id)
	    {// lumberjack
            case 1: resources.wood += (baseResourcesGained * info.level)* Time.deltaTime ;
                break;
            case 2:
                resources.stone += (baseResourcesGained * info.level) * Time.deltaTime;
                break;
            case 3:
                resources.food += (baseResourcesGained * info.level) * Time.deltaTime;
                break; 
        }
	}

    public void UpgradeBuilding()
    {
        info.level++;
        resources.wood -= priceTag.woodPrice;
        resources.stone -= priceTag.stonePrice;
        resources.food -= priceTag.foodPrice;
    }
}
