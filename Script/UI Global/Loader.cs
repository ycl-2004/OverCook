using System;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene{
        MainMenu,
        GameScenes,
        LoadingScene,
    }
    private static Scene targetScene;
    public static void Load(Scene targetScene){
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback(){
        SceneManager.LoadScene(targetScene.ToString());
    }

    
}
