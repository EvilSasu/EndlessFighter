using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageEnergySliderController : MonoBehaviour
{
    public Image energyBar;
    public EnergySystem energy;
    public TextMeshProUGUI energyText;

    private void Start()
    {
        energyText.text = new string(energy.currentEnergy + "/" + energy.maxEnergy);
        energyBar.fillAmount = (float)energy.currentEnergy / energy.maxEnergy;
    }

    public void UpdateEnergyBar()
    {
        energyText.text = new string(energy.currentEnergy + "/" + energy.maxEnergy);
        energyBar.fillAmount = (float)energy.currentEnergy / energy.maxEnergy;
    }
}
