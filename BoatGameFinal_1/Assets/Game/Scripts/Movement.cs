using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Movement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float leftRightSpeed;
  //  [SerializeField] Animator anim;
    [SerializeField] Joystick joyStick;
    [SerializeField] Transform textObj;
    // [SerializeField] Text drtext;
    [SerializeField] CinemachineVirtualCamera cmMain;
    [SerializeField] CinemachineVirtualCamera cmOverview;
    [SerializeField] GameObject camIntro;
    [SerializeField] Transform bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float fireRate;
   


    float currentFireRate = 0;
    Rigidbody playerRigid;
    NavMeshAgent agent;

    PlayerCollision playerColl;

    void Start()
    {
        playerColl = GetComponent<PlayerCollision>();
        agent = GetComponent<NavMeshAgent>();
        playerRigid = GetComponent<Rigidbody>();
     

    }


    public void SetMove(bool val)
    {
        isMove = val;

        if (!val) {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;

        }
    }
    void Update()
    {

        if (playerColl.IsHitRange)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (currentFireRate < Time.time)
                {
                    currentFireRate = Time.time + fireRate;
                    var b = Instantiate(bullet, bulletSpawnPoint.position, bullet.rotation);
                    b.gameObject.SetActive(true);

                    //  b.DOJump(playerColl.hitObj.position, 3, 1, .37f);

                   b.GetComponent<Rigidbody>().AddForce(transform.forward.normalized *
                       Mathf.Abs( Vector3.Distance(playerColl.hitObj.position,transform.position)) * 81f + new Vector3(0,155,0));

                    this.Wait(() => { Destroy(b.gameObject); },1f);
                }

            }

        }








        if (!isMove) return;


        if (playerColl.IsFishSpot)
        {
            if (playerColl.fishSpot != null) {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerColl.IsFishSpot = false;
                    playerColl.fishSpot.Catch();

                }
            }

        }





        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 700))
            {
                agent.destination = new Vector3(hit.point.x, 1.604073f, hit.point.z);
            }
        }

        // Movements();
    }

    bool isMove = true;
    void Movements()
    {
        if (!isMove)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {


            }

            //   cmMain.m_Priority = 9;
            // cmOverview.m_Priority = 10;
           // var v = playerRigid.velocity.magnitude;
          //  anim.SetFloat("RunSpeed", (v <= .4f) ? 1f : v);

           // if (v <= .3f) anim.SetBool("IsRun", false);
          //  else anim.SetBool("IsRun", true);


            // Rotate();
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                // cmMain.m_Priority = 10;
                //  cmOverview.m_Priority = 9;
                playerRigid.ZeroVelocity();
              //  anim.SetBool("IsRun", false);
            }


        }
    }
    void Rotate()
    {
        Vector3 xzDirection = new Vector3(-joyStick.Horizontal, 0, -joyStick.Vertical);
        if (xzDirection.magnitude > 0 && xzDirection != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
          Quaternion.LookRotation(xzDirection), leftRightSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        return;

        if (!isMove)
        {
            playerRigid.ZeroVelocity();
            return;
        }

        Move();
        Rotate();
    }

    public void Move()
    {
        playerRigid.velocity = (new Vector3(joyStick.Horizontal, 0, joyStick.Vertical) *
            speed * Time.fixedDeltaTime);
    }

}
