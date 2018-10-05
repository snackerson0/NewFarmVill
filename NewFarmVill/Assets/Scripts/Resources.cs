using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    
    public float wood, stone, food;
    [Header("UI Reference")]
    public Text resourcesText;
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    int currentWood = Mathf.RoundToInt(wood);
	    int currentStone = Mathf.RoundToInt(stone);
	    int currentFood = Mathf.RoundToInt(food);
        resourcesText.text = "Wood " + currentWood + "\nStone " + currentStone + "\nFood " + currentFood; 
	}
}
