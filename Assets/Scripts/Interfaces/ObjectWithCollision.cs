using UnityEngine;

public abstract class ObjectWithCollision : MonoBehaviour
{
    //not sure how to implement forcing a visitor to exits
    public Visitor visitor;

    public abstract void Accept(Visitor v);
}
