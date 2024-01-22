using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsController : MonoBehaviour
{
    private DataInitializer _dataInitializer;
    private LevelLoader _levelLoader;

    private float currentTimer = 2f;
    private void Awake()
    {
        _dataInitializer = GameObject.FindGameObjectWithTag("DataInitializer").GetComponent<DataInitializer>();
        _levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f)
        {
            _dataInitializer = GameObject.FindGameObjectWithTag("DataInitializer").GetComponent<DataInitializer>();
            _levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
            currentTimer = 2f;
        }
    }

    public void AddLevels()
    {
        if (_dataInitializer != null)
        {
            _dataInitializer.playerData.unitLevel++;
            _dataInitializer.playerData.freeAtributesPoints+=10;
            _dataInitializer.playerData.freeSkillPoints+=10;
            _dataInitializer.SaveAllData();
        }
        else
            Debug.Log("No DataInitializer");     
    }

    public void RemoveData()
    {
        if (_dataInitializer != null)
        {
            _dataInitializer.RemoveAllDataKeys();
            _dataInitializer.saveData = false;
            _levelLoader.LoadScene("MainMenu");
        }
        else
            Debug.Log("No DataInitializer");
    }

    public void CreateNewData()
    {
        if (_dataInitializer != null)
        {
            _dataInitializer.InitializeAllData();
        }
        else
            Debug.Log("No DataInitializer");
    }
}
