using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int distance;
    [SerializeField] Animator anim;
    [SerializeField] bool isChase = false;
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] int health = 100;
    [SerializeField] GameObject explosion;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

       
    }


   
    // Update is called once per frame
    void Update()
    {
        if (!agent.enabled) return;

        if(Mathf.Abs(Vector3.Distance(transform.position,target.position)) < distance && !isChase)
        {
            anim.SetBool("IsRun",true);
            isChase = true;
        }

        if (Mathf.Abs(Vector3.Distance(transform.position, target.position)) > (distance+7) && isChase)
        {
            anim.SetBool("IsRun", false);
            isChase = false;
            agent.SetDestination(transform.position);
        }


        if (isChase)
        {
            agent.SetDestination(target.position);
            if (agent.velocity.magnitude > .5f)
                agent.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(agent.velocity).
                    eulerAngles.y, 0);
        }
    }


    bool isHitFx = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!agent.enabled) return;

        if (other.IsNameStartWith("Explode"))
        {
            if (!isHitFx)
            {
                isHitFx = true;
                mesh.material.DOColor(Color.red, .05f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => { isHitFx = false; });
            }


            health -= 25;
            Destroy(other.transform.parent.gameObject);
            var e = Instantiate(explosion,new Vector3(transform.position.x,explosion.transform.position.y,transform.position.z),Quaternion.identity);
            e.SetActive(true);


            UiManager.Instance.BossBarFill(health);

            if (health <= 0)
            {
                GameManager.instance.OnEnemyKilled();

                Destroy(gameObject);
            }
        }
    }


    public void JumpToPlayer(Transform p)
    {

        agent.enabled = false;

        transform.DOJump(p.position, 7, 1, .7f);


    }

}
