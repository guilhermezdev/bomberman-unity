using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyAfterAnimationController : MonoBehaviour
{
  
    void Start()
    {
        float length = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, length);
    }
   
}
