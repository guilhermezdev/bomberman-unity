using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float itemSpawnChange = 0.5f;
    public List<GameObject> items = new List<GameObject>();

    private void OnDestroy()
    {
        if(items.Count > 0 && Random.value <= itemSpawnChange)
        {
            int randomIndex = Random.Range(0, items.Count);

            Instantiate(items[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
