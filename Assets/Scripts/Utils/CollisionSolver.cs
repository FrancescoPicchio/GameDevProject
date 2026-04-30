using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSolver : MonoBehaviour
{
    static private Dictionary<(Type, Type), Func<IObjectWithMovementCollision, IObjectWithMovementCollision, bool>> interactions = new();

    void Awake()
    {
        Register<Player, Boulder>(PlayerOnBoulder);
        Register<Boulder, Boulder>(BoulderOnBoulder);
    }

    public void Register<T1, T2>(Func<T1, T2, bool> func)
        where T1 : IObjectWithMovementCollision
        where T2 : IObjectWithMovementCollision
    {
        interactions[(typeof(T1), typeof(T2))] = (a, b) => func((T1)a, (T2)b);
    }
    public static bool A_MovesOver_B(IObjectWithMovementCollision a, IObjectWithMovementCollision b)
    {
        var key = (a.GetType(), b.GetType());

        if (interactions.TryGetValue(key, out var func))
        {
            return func(a, b);
        }

        Debug.Log("No interaction defined");
        return false;
    }

    bool PlayerOnBoulder(Player p, Boulder b)
    {
        Vector3 direction = p.transform.position - b.transform.position;
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);
        if (col != null && col.TryGetComponent<IObjectWithMovementCollision>(out IObjectWithMovementCollision other))
        {
            return A_MovesOver_B(b, other);
        }
        return true;
    }
    bool BoulderOnBoulder(Boulder b1, Boulder b2)
    {
        Vector3 direction = b1.transform.position - b2.transform.position;
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);
        if (col != null && col.TryGetComponent<IObjectWithMovementCollision>(out IObjectWithMovementCollision other))
        {
            return A_MovesOver_B(b2, other);
        }
        return true;
    }
}