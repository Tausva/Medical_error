using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewOne : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        GameObject.Find("Spawner").GetComponent<CharacterSpawner>().Characters();
        GameObject.Find("Spawner").GetComponent<CharacterSpawner>().CloseExit();
    }
}
