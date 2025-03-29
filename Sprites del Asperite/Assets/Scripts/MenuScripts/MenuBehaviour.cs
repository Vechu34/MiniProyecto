using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuBehaviour : MonoBehaviour
{
    [Header("Selector escena")] //Works to have a header Unity's inspector
    public string sceneLoad;
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneLoad)) 
        {
            SceneManager.LoadScene(sceneLoad);
        }

        else
        {
            Debug.LogError("El nombre de la escena o la escena no está configurado correctamente");
        }
    }
    public void LoadSceneIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        else
        {
            Debug.LogError("El nombre de la escena o la escena no está configurado correctamente");
        }
    }
    public void ExitGame()
    {
        Debug.Log("El juego se cierra");
        Application.Quit();
    }
}
