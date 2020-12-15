using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameController.isNewParty = true;

        SceneManager.LoadScene("LevelDesign");

    }

    public void LoadGame()
    {
        GameController.isNewParty = false;

        SceneManager.LoadScene("LevelDesign");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
