using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject pausePanel;

    bool inventoryOpen = false;
    bool pausePanelOpen = false;

    public Slider healthbar;
    public int health = 10;

    private void Awake()
    {
        healthbar.maxValue = health;
        healthbar.value = health;
    }
    public void Inventory()
    {
        if (inventoryOpen)
        {
            inventoryPanel.SetActive(false);
            inventoryOpen = false;
        }
        else
        {
            inventoryPanel.SetActive(true);
            inventoryOpen = true;
        }
    }    
    public void PauseMenu()
    {
        if (pausePanelOpen)
        {
            pausePanel.SetActive(false);
            pausePanelOpen = false;
        }
        else
        {
            pausePanel.SetActive(true);
            pausePanelOpen = true;
        }
    }
    public void TakeAwayHealth()
    {
        health--;
        healthbar.value = health;
    }
    public void AddHealth()
    {
        if (health < 10)
        {
            health++;
            healthbar.value = health;
        }
    }
}
