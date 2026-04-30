using UnityEngine;

public class WallVisitor : Visitor
{
    public override void PlayerVisit(Player player)
    {
        Debug.Log("found wall");
        player.Stop();
    }

    public override void SimpleEnemyVisit(SimpleEnemy simpleEnemy)
    {
        simpleEnemy.FlipMovementDirection();
    }
}
