using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;
    
    public GameObject lose;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) <= 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
        
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private void OnTriggerEnter(Collider collision)
    
    {
        
        if (collision.gameObject == player)
        {
            Debug.Log("Player hit");
            Destroy(collision.gameObject);
            //activate lose canvas
            lose.gameObject.SetActive(true);
            
        }
        Debug.Log("Player hit");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit");
            Destroy(collision.gameObject);
            //activate lose canvas
            lose.gameObject.SetActive(true);
            
        }
        
        
    }
}
