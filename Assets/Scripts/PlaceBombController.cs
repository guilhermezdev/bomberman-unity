using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceBombController : MonoBehaviour
{
    public GameObject bombPrefab;

    public int bombsCount = 1;
    public int bombFuseTime = 3;
    public int bombRadius = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombsCount > 0){
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        bombsCount--;
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

        bomb.GetComponent<BombController>().bombRadius = bombRadius;

        yield return new WaitForSeconds(bombFuseTime);

        bombsCount++;

        Destroy(bomb);
    }

 
}
