using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarLocomotion))]
public class Fuel : MonoBehaviour
{
    [SerializeField] private float fuelConsumedPerSecond = 20;
    [SerializeField] private UIManager uiManager;
    GameManager gameManager;
    private float timer;
    private float delayBetweenFuelReduction = 0.05f; //in seconds
    private float fuelReductionAmount;
    private float currFuel = 100;
    private CarLocomotion driver;
    private int fuelCans = 0;
    private float startFuel;
 

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        driver = GetComponent<CarLocomotion>();
        fuelReductionAmount = fuelConsumedPerSecond * delayBetweenFuelReduction;
        uiManager.UpdateFuelBar(currFuel);
        startFuel = currFuel;
    }

    private void Update()
    {
        if (currFuel > 0f || fuelCans > 0)
        {
            UseFuel();
        }
    }

    private void ChangeFuelLevel(float amount)
    {
        currFuel += amount;
        if (currFuel <= 0)
        {
            if (fuelCans == 0)
            {
                currFuel = 0f;
            }
            else
            {
                while (currFuel < 0 && fuelCans > 0)
                {
                    currFuel += 100;
                    fuelCans--;
                }
                if (fuelCans < 0) 
                {
                    fuelCans = 0;
                    currFuel = 0;
                }
                uiManager.UpdateFuelBar(currFuel);
                uiManager.UpdateFuelCans(fuelCans);
            }
        }
        else if (currFuel > 100)
        {
            fuelCans += (int)currFuel / 100;
            currFuel -= 100;
            uiManager.UpdateFuelCans(fuelCans);
        }
        if (currFuel <= 0 && fuelCans <= 0) OutOfFuel();
        uiManager.UpdateFuelBar(currFuel);
    }

    private void UseFuel()
    {
        if (driver.GetMoveState() != MoveState.Moving && driver.GetMoveState() != MoveState.Turning) return;

        timer += Time.deltaTime;

        if (timer >= delayBetweenFuelReduction)
        {
            timer -= delayBetweenFuelReduction;
            ChangeFuelLevel(-fuelReductionAmount);
        }
    }

    public void MessageChangeFuel(float amount)
    {
        ChangeFuelLevel(amount);
    }

    private void OutOfFuel()
    {
        gameManager.EndPlay();
        driver.MessageStopCar();
    }

    public void MessageResetFuel()
    {
        currFuel = startFuel;
        fuelCans = 0;
        uiManager.UpdateFuelCans(fuelCans);
        uiManager.UpdateFuelBar(currFuel);
    }
}
