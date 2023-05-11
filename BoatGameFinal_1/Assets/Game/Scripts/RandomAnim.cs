using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnim : MonoBehaviour
{
    [SerializeField] float minSpeed =1f;
    [SerializeField] float maxSpeed = 1.25f;
    void Start()
    {
        GetComponent<Animator>().speed = Random.Range(minSpeed,maxSpeed);
    }

  
}
