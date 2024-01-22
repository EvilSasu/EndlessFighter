using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPanelData : MonoBehaviour
{
    public PlayerData player;

    private void Update()
    {
        
    }

    public void ShowHide()
    {
        if(this.gameObject.activeSelf)
        { 
            this.gameObject.SetActive(false);
        }
        else 
        { 
            this.gameObject.SetActive(true); 
        }
    }
}
