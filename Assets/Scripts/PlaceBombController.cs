using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceBombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject timerBombPrefab;

    public int bombsCount = 1;
    public int bombFuseTime = 2;
    public int bombRadius = 1;

    public bool isTimerBombEnabled = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombsCount > 0){
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        bombsCount--;
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        if(isTimerBombEnabled)
        {
            GameObject bomb = Instantiate(timerBombPrefab, position, Quaternion.identity);

            bomb.GetComponent<BombController>().bombRadius = bombRadius;
        }
        else
        {
            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

            bomb.GetComponent<BombController>().bombRadius = bombRadius;

            Destroy(bomb, bombFuseTime);
        }
    }
}
