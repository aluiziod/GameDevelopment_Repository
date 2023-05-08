using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mainball : MonoBehaviour
{
    [SerializeField] private int scoreCounter = 0;
    [SerializeField] private TMP_Text scoreText;
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rigidbody2D;
    
    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        scoreText.text = scoreCounter.ToString();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "target")
        {
            scoreCounter++;
            scoreText.text = scoreCounter.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            scoreCounter++;
            scoreText.text = scoreCounter.ToString();
        }
    }
    
}
