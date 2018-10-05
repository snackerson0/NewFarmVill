using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int connectedBuildingID;
    [HideInInspector]
    public Building connectedBuilding;
    
    public Text resourceText;

    private Button button;

    private Resources resources;
	// Use this for initialization
	void Awake ()
	{
	    button = GetComponent<Button>();
	    resources = FindObjectOfType<Resources>();
	    Buildings buildings = FindObjectOfType<Buildings>();
	    
        foreach (var gO in buildings.buildables)
        {
            Building b = gO.GetComponent<Building>();
            if (b.info.id == connectedBuildingID)
            {
                connectedBuilding = b;
                break;
            }
        }
        
                resourceText.text = connectedBuilding.priceTag.woodPrice + " wo.| " +
                                    connectedBuilding.priceTag.stonePrice + " st. | " +
                                    connectedBuilding.priceTag.foodPrice + " fd. | ";
        
    }

    // Update is called once per frame
    void Update ()
	{
	    bool result = false;
	    if (!resources) print("Resources null");
        if (resources.wood >= connectedBuilding.priceTag.woodPrice&&
	        resources.stone >= connectedBuilding.priceTag.stonePrice &&
	        resources.food >= connectedBuilding.priceTag.foodPrice)
	    {
	        result = true;
	        
	    }
	    button.interactable = result;
    }
}
