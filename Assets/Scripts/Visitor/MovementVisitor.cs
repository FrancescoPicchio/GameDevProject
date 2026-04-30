using UnityEngine;

public class MovementVisitor : IActionVisitor
{
    private ObjectWithMovementCollision other;

    public MovementVisitor(ObjectWithMovementCollision other)
    {
        this.other=other;
    }

    public void Visit(Player player)
    {
        other.SolveCollision(player);
    }

    public void Visit(Boulder boulder)
    {
        other.SolveCollision(boulder);
    }
}