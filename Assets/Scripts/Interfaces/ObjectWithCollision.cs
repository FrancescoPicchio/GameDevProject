using UnityEngine;

public abstract class ObjectWithCollision : MonoBehaviour
{
    protected Vector3 targetPosition;

    //not sure how to implement forcing a visitor to exits
    public Visitor visitor;

    public abstract void Accept(Visitor v);

    public void LookForObjectWithCollision()
    {
        Vector2 targetPoint = targetPosition;
        Collider2D target = Physics2D.OverlapPoint(targetPoint);
        //FIXME  check if target is different from the caller
        if (target is null)
            return;
        else
        {
            //FIXME GetComponent is an expensive function. Try to find an alternative
            ObjectWithCollision other =
                target.transform.gameObject.GetComponent<ObjectWithCollision>();
            if (other is not null)
                Accept(other.visitor);
        }
    }
}
