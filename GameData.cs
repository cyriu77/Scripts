﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highScores;
    public int[] stars;
}

public class GameData : MonoBehaviour
{
    public static GameData gamaData;
    public SaveData saveData;

    // Start is called before the first frame update
    void Awake()
    {
        if(gamaData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gamaData = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Load();
    }
    private void Start()
    {
            
    }
    public void Save()
    {
        //Create a binary formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Create a route from the program to the file

        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);

        //Create a copy of save data
        SaveData data = new SaveData();
        data = saveData;

        //Actually save the data in the file
        formatter.Serialize(file, data);

        //Close the data stream
        file.Close();

        Debug.Log("Saved");

    }
    public void Load()
    {
        //check if the save game file exits
        if(File.Exists(Application.persistentDataPath + "/player.dat"))
        {
                //Create a Binary Formatter
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
                saveData = formatter.Deserialize(file) as SaveData;
                file.Close();
                Debug.Log("Loaded");
        }
    }
    private void OnDisable()
    {
        Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
