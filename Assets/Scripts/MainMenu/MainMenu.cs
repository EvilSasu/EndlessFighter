using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public void StartGame()
    {
        CheckIfSavesExist();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void CheckIfSavesExist()
    {
        bool _noSaveTest = false;
        if (!_noSaveTest)
        {
            levelLoader.LoadScene("Village");
        }
        else
        {
            levelLoader.LoadScene("Game");
        }
    }
}
