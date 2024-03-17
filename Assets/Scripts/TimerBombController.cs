using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBombController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) )
        {
            Destroy(gameObject);
        }
    }

}
