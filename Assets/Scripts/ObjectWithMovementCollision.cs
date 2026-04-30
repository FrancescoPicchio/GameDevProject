using System;
using UnityEngine;

public abstract class ObjectWithMovementCollision : MonoBehaviour
{
    public abstract void Accept(IActionVisitor visitor);
    public abstract bool SolveCollision(Player player);
    public abstract bool SolveCollision(Boulder boulder);

    
}
