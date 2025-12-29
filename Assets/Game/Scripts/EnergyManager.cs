using TMPro;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private int maxEnergyValue;
    [SerializeField] private TMP_Text energyTxt;
    private int currentEnergy = 0;

    private void Start()
    {
        currentEnergy = maxEnergyValue;
        UpdateUI();
    }
    public void UpdateUI()
    {
        energyTxt.text = currentEnergy.ToString() + "/" + maxEnergyValue.ToString();
    }
    public bool EnoughEnergyToPlayCard(int value)
    {
        if(currentEnergy >= value)
        {
            currentEnergy -= value;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough energy to use this card!");
            return false;
        }
        
    }
    public void RefillEnergy()
    {
        currentEnergy = maxEnergyValue;
        UpdateUI();
    }
}
