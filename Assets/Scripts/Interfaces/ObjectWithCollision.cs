using UnityEngine;

//ObjectWithCollision are the ones that accept a visitor 
//from an ObjectWithCollision trying to enter their space
public abstract class ObjectWithCollision : MonoBehaviour
{
    protected Vector3 targetPosition;
    //not sure how to implement forcing a visitor to exits
    public Visitor visitor;

    public abstract void Accept(Visitor v);

    public void LookForObjectWithCollision()
    {
        Vector2 targetPoint = targetPosition;
        Collider2D target = Physics2D.OverlapPoint(targetPoint, LayerMask.GetMask("ObjectWithCollision"));
        //FIXME  check if target is different from the caller
        if(target is not null){
            //FIXME GetComponent is an expensive function. Try to find an alternative
            target.transform.gameObject.GetComponent<ObjectWithCollision>().Accept(visitor);
            // ObjectWithCollision other = target.transform.gameObject.GetComponent<ObjectWithCollision>();
            // Accept(other.visitor);
        }
    }
}
