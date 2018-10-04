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
	    resourcesText.text = "Wood " + wood + "\nStone " + stone + "\nFood " + food; 
	}
}
