using UnityEngine;

public class Player : ObjectWithMovementCollision
{
    public override void Accept(IActionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override bool SolveCollision(Player player)
    {
        // Un oggetto player vuole spostarsi su questo oggetto
        
        return false;
    }
    public override bool SolveCollision(Boulder boulder)
    {
        // Un oggetto boulder vuole spostarsi su questo oggetto
        return false;
    }
}