using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        GameObject obj1 = new GameObject("Player");
        ObjectWithMovementCollision player = obj1.AddComponent<Player>();

        GameObject obj2 = new GameObject("Boulder");
        ObjectWithMovementCollision boulder = obj2.AddComponent<Boulder>();
        Debug.Log("Boulder si muove su player: ");
        ResolveMovement(boulder, player);
    }

    void ResolveMovement(ObjectWithMovementCollision movingObj, ObjectWithMovementCollision other)
    {
        MovementVisitor visitor = new MovementVisitor(other);
        movingObj.Accept(visitor);
    }
}