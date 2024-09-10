using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void GoToScene(int sceneID){
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
    }

    public void Quit(){
        Application.Quit();
    }
    
    
}
