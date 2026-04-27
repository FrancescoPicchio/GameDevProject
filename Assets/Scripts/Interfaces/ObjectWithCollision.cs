using UnityEngine;

public abstract class ObjectWithCollision : MonoBehaviour
{
    public abstract void Accept(Visitor v);
}
