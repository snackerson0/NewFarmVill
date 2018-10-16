using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Experimental.PlayerLoop;
[System.Serializable]
public class SavedProfile
{
    public float s_Wood, s_Stone, s_Food;
    public List<BuildingInfo> buildingsSavedData = new List<BuildingInfo>();
}
public class Save : MonoBehaviour
{
    public SavedProfile savedProfile;

    private Resources resources;

    private Buildings buildings;

    private Build build;

	// Use this for initialization
	void Awake ()
	{
	    resources = FindObjectOfType<Resources>();
	    buildings = FindObjectOfType<Buildings>();
	    build = FindObjectOfType<Build>();
        LoadGame();
	}

    private void SaveGame()
    {
        if (savedProfile ==null)
        {
            savedProfile = new SavedProfile();
        }

        savedProfile.s_Wood = resources.wood;
        savedProfile.s_Stone = resources.stone;
        savedProfile.s_Food = resources.food;

        foreach (var g in buildings.builtObjects)
        {
            BuildingInfo b = g.GetComponent<Building>().info;
                        savedProfile.buildingsSavedData.Add(b);
        }
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
            File.Delete(path);
        FileStream fs = File.Open(path, FileMode.OpenOrCreate);
        bf.Serialize(fs,savedProfile);
        fs.Close();
    }

    private void LoadGame()
    {
        string pathToLoad = Application.persistentDataPath + "/save.dat";
        if (!File.Exists(pathToLoad)){ print("No saved profile found ;("); return;}
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(pathToLoad, FileMode.Open); 
        SavedProfile loadProfile = bf.Deserialize(fs) as SavedProfile;
        fs.Close();
        resources.wood = loadProfile.s_Wood;
        resources.stone = loadProfile.s_Stone;
        resources.food = loadProfile.s_Food;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {SaveGame(); print("Game Saved!"); }
    }
}
