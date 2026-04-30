using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        GameObject obj1 = new GameObject("Player");
        IObjectWithMovementCollision player = obj1.AddComponent<Player>();

        GameObject obj2 = new GameObject("Boulder");
        IObjectWithMovementCollision boulder = obj2.AddComponent<Boulder>();

        Debug.Log(CollisionSolver.A_MovesOver_B(boulder,player));
    }

}