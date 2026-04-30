using UnityEngine;

public class Boulder : ObjectWithMovementCollision
{
    public override void Accept(IActionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override bool SolveCollision(Player player)
    {
        // Un oggetto player vuole spostarsi su questo oggetto
        Vector3 direction = this.transform.position-player.transform.position;
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);
                
        if (col != null && col.TryGetComponent<ObjectWithMovementCollision>(out ObjectWithMovementCollision other))
        {
            return other.SolveCollision(this);
        }
        return true;
    }
    public override bool SolveCollision(Boulder boulder)
    {
        // Un oggetto boulder vuole spostarsi su questo oggetto
        Vector3 direction = this.transform.position-boulder.transform.position;
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);
                
        if (col != null && col.TryGetComponent<ObjectWithMovementCollision>(out ObjectWithMovementCollision other))
        {
            return other.SolveCollision(this);
        }
        return true;
    }
}