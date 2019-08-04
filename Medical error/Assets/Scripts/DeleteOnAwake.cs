using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteOnAwake : MonoBehaviour
{
    public List<int> Scenes;
    Scene scene;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
            Destroy(gameObject);
    }

    private void Update()
    {
        int count = 0;
        scene = SceneManager.GetActiveScene();
        foreach (int a in Scenes)
        {
            if (scene.buildIndex == a)
                count++;
        }

        if (count == 0)
          Destroy(gameObject);
        
    }
}
