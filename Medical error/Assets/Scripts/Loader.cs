using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader
{
    private static Action onLoaderCallback;

    public static void Load()
    {
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(1);
        };

        SceneManager.LoadScene(6);
    }

    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
