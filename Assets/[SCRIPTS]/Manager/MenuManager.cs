using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    
    public void GoMmENU()
    {
        SceneManager.LoadScene(0);
    }
    
    public void  StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenURL()
    {
        Application.OpenURL("https://alzheimer-recherche.org/faire-un-don-pour-la-recherche-sur-la-maladie-dalzheimer/?gad_source=1&gclid=Cj0KCQjwkZm_BhDrARIsAAEbX1FpVisR_rD_jTVmvJ1MTxqOQLwK-BtxjtIJnqQ1o0TScmHPeqMvni8aAmETEALw_wcB");
    }
}
