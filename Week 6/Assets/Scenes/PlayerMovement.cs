using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    public bool grounded;
    [SerializeField] private GameObject particleSystem;
    public int collectableCounter;
    [SerializeField] private AudioClip sound_collectable;
    [SerializeField] private TMP_Text cherriesText;
    [SerializeField] private GameObject gameOverPanel;
   
    
    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collectableCounter = 0;
    }
 
    private void Update()
    {
        cherriesText.text = collectableCounter.ToString();
        if (body.velocity.y < 0)
        {
            anim.SetTrigger("fall");
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
 
        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(4,4,4);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-4, 4, 4);
 
        
        
        
            
 
        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    
    //detect if player is falling

  
        
 
    public void Jump()
    {
        if (grounded)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            anim.SetTrigger("jump");
            grounded = false;
            particleSystem.SetActive(true);
        }
       
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            particleSystem.SetActive(true);
           // anim.SetTrigger("grounded");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Collectable")
        {
            SoundManager.instance.PlaySound(sound_collectable);
            Destroy(col.gameObject);
            collectableCounter++;
        }

        if (col.gameObject.tag == "spike")
        {
            gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
            
        }
        if(col.gameObject.tag == "win")
        {
            Win();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Win()
    {
        SceneManager.LoadScene(1);
    }
}
