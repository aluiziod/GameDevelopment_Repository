using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticWorldEuler : MonoBehaviour
{
    [SerializeField] Vector3 globalZeroEular;
    void Start()
    {
      //  print(transform.eulerAngles);
    }

   
    void LateUpdate()
    {
        transform.eulerAngles = globalZeroEular; //Vector3.Lerp(transform.eulerAngles,
           // globalZeroEular, Time.deltaTime * 7f);

        //print(transform.eulerAngles);
    }
}
