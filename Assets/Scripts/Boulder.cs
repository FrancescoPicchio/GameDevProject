using UnityEngine;

public class Boulder : MovingObject
{
    override public bool CanMoveThere(Vector3 direction)
    {
        return TryToMove(direction);
    }

    override protected void Move(Vector3 direction)
    {
        transform.position+=direction;
    }
}