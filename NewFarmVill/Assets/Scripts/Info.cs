using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public Text nameText;
    public Button upgradeButton, destoryButton;
    private Build build;
    private Building selectedBuilding;

    private Resources resources;
    // Use this for initialization
	void Awake ()
	{
	    build = FindObjectOfType<Build>();
	    resources = FindObjectOfType<Resources>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (build.CurSelectedGridElement && build.CurSelectedGridElement.connectedBuilding)
	    {
	        selectedBuilding = build.CurSelectedGridElement.connectedBuilding;
	        nameText.text = selectedBuilding.objectName + "\n Level " + selectedBuilding.info.level;
	    }
	    else
	    {
	        nameText.text = "No building selected";
	        selectedBuilding = null;
	    }

	    destoryButton.interactable = selectedBuilding;
        bool result = false;
        if (!resources) print("Resources null");
        if (resources.wood >= selectedBuilding.priceTag.woodPrice &&
            resources.stone >= selectedBuilding.priceTag.stonePrice &&
            resources.food >= selectedBuilding.priceTag.foodPrice)
        {
            result = true;

        }

    }

    public void OnButtonUpgrade()
    {
        if (!selectedBuilding) return;
        selectedBuilding.UpgradeBuilding();


    }

    public void OnButtonDestory()
    {
        if (!selectedBuilding) return;
        build.CurSelectedGridElement.isOccupied = false;
        build.buildings.builtObjects.Remove(selectedBuilding.gameObject);
        Destroy(selectedBuilding.gameObject);
    }
}
