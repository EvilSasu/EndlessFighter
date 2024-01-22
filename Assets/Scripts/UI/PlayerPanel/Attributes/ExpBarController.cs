using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ExpBarController : MonoBehaviour
{
    public Image expBar;
    public PlayerData playerData;
    public TextMeshProUGUI expText;

    private void Start()
    {
        expText.text = new string(playerData.currentExp + "/" + playerData.maxExp);
        expBar.fillAmount = (float)playerData.currentExp / playerData.maxExp;
    }
}
