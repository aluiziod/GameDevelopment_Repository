using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject win;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)

    {
        if (collision.gameObject.tag == "endpoint")
        {
            Debug.Log("Player hit");
            Destroy(collision.gameObject);
            //activate lose canvas
            win.gameObject.SetActive(true);
            
        }
    }
    
}
