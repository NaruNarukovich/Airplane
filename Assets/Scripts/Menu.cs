using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject shopPanel;

    public void Play() 
    {
        SceneManager.LoadScene(1);
    }

    public void Shop() 
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    public void SelectSkin(int index) 
    {
        PlayerPrefs.SetInt("CurrentSkin", index);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
