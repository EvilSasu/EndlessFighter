using System;
using System.Collections;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public int maxEnergy;
    public VillageEnergySliderController energySliderController;

    public float energyPerHour = 1000f;
    public float energyRegenIntervalSeconds = 15f;

    private DateTime lastEnergyUpdateTime;
    public int currentEnergy;
    private GameData gameData;
    private bool StartCalculating = false;
    public float tmpEnergy = 0f;
 
    private void Setup()
    {
        gameData = GetComponent<GameData>();
        maxEnergy = gameData.maxEnergy;
        currentEnergy = gameData.currentEnergy;
        lastEnergyUpdateTime = gameData.lastEnergyUpdateTime;
        StartCoroutine(TimeBeforeStart(2f));
    }

    private void Update()
    {
        if (StartCalculating)
        {
            if ((DateTime.Now - lastEnergyUpdateTime).TotalSeconds >= energyRegenIntervalSeconds)
            {
                int energyToAdd = CalculateEnergyToAdd();
                currentEnergy += energyToAdd;
                currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
                lastEnergyUpdateTime = DateTime.Now;
                gameData.currentEnergy = currentEnergy;
                gameData.lastEnergyUpdateTime = lastEnergyUpdateTime;

                if(energySliderController != null)
                    energySliderController.UpdateEnergyBar();

                gameData.SaveGameDataToPlayerPrefs();
            }
        }      
    }

    private int CalculateEnergyToAdd()
    {
        double secondsSinceLastUpdate = (DateTime.Now - lastEnergyUpdateTime).TotalSeconds;
        tmpEnergy += (float)(secondsSinceLastUpdate / energyRegenIntervalSeconds) * energyPerHour / 3600f;

        int energyToAdd = 0;

        if (tmpEnergy >= 1f)
        {
            energyToAdd = Mathf.FloorToInt((float)tmpEnergy);
            tmpEnergy -= energyToAdd;
        }

        return energyToAdd;

    }

    private void UpdateEnergy()
    {
        int energyToAdd = CalculateEnergyToAdd();

        currentEnergy += energyToAdd;

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        lastEnergyUpdateTime = DateTime.Now;
        gameData.currentEnergy = currentEnergy;
        gameData.lastEnergyUpdateTime = lastEnergyUpdateTime;

        gameData.SaveGameDataToPlayerPrefs();
    }

    IEnumerator TimeBeforeStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCalculating = true;
    }

    IEnumerator LaterStart(float time)
    {
        yield return new WaitForSeconds(time);
        Setup();
        UpdateEnergy();

        if(energySliderController != null)
            energySliderController.UpdateEnergyBar();
    }

    public void InitializeData()
    {
        Setup();
        UpdateEnergy();

        if (energySliderController != null)
            energySliderController.UpdateEnergyBar();
    }
}
