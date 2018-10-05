using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Build : MonoBehaviour
{
   
    
    private int display;
    
    private GameObject currentCreatedBuildable; 
    public GridElement CurSelectedGridElement, curHoveredGridElement, lastHoveredGridElement;

    public GridElement[] grid; 
    [Header("Color")]
    public Color colorOnHover = Color.white;
    public Color colorOnOccupied = Color.red;

    private RaycastHit mouseHit;
    private Color colorOnNormal;

    public bool buildInProgress;

    public Buildings buildings;
    
	// Use this for initialization
	void Awake ()
	{
	    colorOnNormal = grid[0].GetComponentInChildren<MeshRenderer>().material.color;
	    buildings = FindObjectOfType<Buildings>();
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ProcessMouseSelectionAndHover();
        MoveBuilding();
        PlaceBuilding();
	}

    private void ProcessMouseSelectionAndHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out mouseHit))
        {
            GridElement g = mouseHit.transform.GetComponent<GridElement>();
            if (!g)
            {
                if (curHoveredGridElement)
                {
                    
                    curHoveredGridElement.GetComponent<MeshRenderer>().material.color = colorOnNormal;
                    return;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                CurSelectedGridElement = g;
            }

            if (g)
            {
                curHoveredGridElement = g;
            }

            if (g = curHoveredGridElement)
            {
                if (!g.isOccupied) mouseHit.transform.GetComponent<MeshRenderer>().material.color = colorOnHover;
                else mouseHit.transform.GetComponent<MeshRenderer>().material.color = colorOnOccupied;
            }

            if (g != curHoveredGridElement)
            {
                curHoveredGridElement = g;
            }

            
            if (!lastHoveredGridElement)
            {
                lastHoveredGridElement = curHoveredGridElement;
            }

            if (lastHoveredGridElement != curHoveredGridElement)
            {
                lastHoveredGridElement.GetComponent<MeshRenderer>().material.color = colorOnNormal;
                lastHoveredGridElement = curHoveredGridElement;
            }
        }
    }

    public void OnButtonCreateBuilding(int id)
    {
        if (buildInProgress) return;
        GameObject g = null;
        foreach (var gO in buildings.buildables)
        {
            Building b = gO.GetComponent<Building>();
            if (b.info.id == id)
            {
                g = b.gameObject;
            }
        }

        currentCreatedBuildable = Instantiate(g);
        currentCreatedBuildable.transform.rotation = Quaternion.identity;
        buildInProgress = true;
    }

    public void MoveBuilding()
    {
       if (!currentCreatedBuildable) return;
        currentCreatedBuildable.layer = 2;

        if (curHoveredGridElement)
        {
            currentCreatedBuildable.transform.position = curHoveredGridElement.transform.position;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentCreatedBuildable);
            currentCreatedBuildable = null;
            buildInProgress = false;
        }
    }

    public void PlaceBuilding()
    {
        if (!currentCreatedBuildable || curHoveredGridElement.isOccupied) return;

        if (Input.GetMouseButtonDown(0))
        {
            buildings.builtObjects.Add(currentCreatedBuildable);
            curHoveredGridElement.isOccupied = true;

            Building b = currentCreatedBuildable.GetComponent<Building>();
            curHoveredGridElement.connectedBuilding = b;
            b.placed = true;

            b.UpgradeBuilding();
            b.info.connectedGridID = curHoveredGridElement.gridID;
            currentCreatedBuildable = null;
            buildInProgress = false;
        }
    }
}
