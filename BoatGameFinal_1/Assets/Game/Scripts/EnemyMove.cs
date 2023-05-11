using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
   

    [SerializeField] Transform enemy;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float speed = 33f;

    void Start()
    {
        var pos = wayPoints.GoTransformsArrayToVector3ArrayPositions();
        enemy.DOPath(pos,speed, PathType.CatmullRom, PathMode.Full3D, 10).SetOptions(true, AxisConstraint.None, AxisConstraint.None)
            .SetLookAt(0.05f,Vector3.forward, Vector3.up)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
           
    }

   
}
