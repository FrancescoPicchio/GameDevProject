using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class MovingObject : ObjectWithMovementCollision
{
    public virtual bool IsMovementLegal(Vector3 direction)
    {
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);

        if (col != null && col.gameObject != gameObject && col.TryGetComponent<ObjectWithMovementCollision>(out ObjectWithMovementCollision other))
        {
            return (other.CanMoveThere(direction));
        }
        return true;
    }
    public virtual bool TryToMove(Vector3 direction)
    {
        if (IsMovementLegal(direction))
        {
            Move(direction);
            return true;
        }
        return false;
    }
    protected abstract void Move(Vector3 direction);
}
