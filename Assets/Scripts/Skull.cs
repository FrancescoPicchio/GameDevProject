using UnityEngine;

public class Skull : ReactingObject
{
    private Vector3 currentDirection = Vector3.left;

    public override bool CanMoveThere(Vector3 direction)
    {
        return false;
    }

    protected override void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    protected override void Reaction()
    {
        if (!TryToMove(currentDirection))
        {
            currentDirection = Vector3.right;
            if (!TryToMove(currentDirection))
            {
                currentDirection = Vector3.left;
            }
        }
    }
}
