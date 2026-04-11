using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public abstract class ObjectWithMovementCollision : MonoBehaviour
{
    public abstract bool CanMoveThere(Vector3 direction);
}
