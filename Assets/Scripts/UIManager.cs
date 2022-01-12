using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image fuelGauge;
    [SerializeField] private ObjectPool fuelCansPool;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;
    private int uiFuelCans = 0;

    private void Awake() 
    {
        fuelGauge.fillAmount = 1.0f;
    }

    public void UpdateFuelBar(float currFuel)
    {
        fuelGauge.fillAmount = (currFuel % 101) / 100;
    }

    public void UpdateFuelCans(int numCans)
    {
        if (numCans == uiFuelCans) return;

        if (numCans < uiFuelCans)
        {
            for (int i = numCans; i < uiFuelCans; i++)
            {
                fuelCansPool.RemoveObject();
            }
        }
        else
        {
            for (int i = uiFuelCans; i < numCans; i++)
            {
                fuelCansPool.GetObject();
            }
        }
        uiFuelCans = numCans;
    }

    public void ChangeScore(int newScore)
    {
        if (newScore <= 0)
        {
            scoreText.text = "";
        }
        else
        {
            scoreText.text = "" + newScore;
            finalScoreText.text = "Score: " + newScore;
        }
    }
}
