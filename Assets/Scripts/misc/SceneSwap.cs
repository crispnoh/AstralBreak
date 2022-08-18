using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    //to make things easier this script will be connected to an empty object
    //bc in order to reference a script from the game it needs to be connected
    //to something and having it connected to something called "SceneChangerLol"
    //just makes it easier to understand and all that :)

    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(sceneName: scene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
