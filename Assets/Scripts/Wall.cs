using UnityEngine;

public class Walls : ObjectWithMovementCollision
{
    override public bool CanMoveThere(Vector3 direction)
    {
        return false;
    }
}