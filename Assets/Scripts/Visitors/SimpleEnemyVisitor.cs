using UnityEngine;

public class SimpleEnemyVisitor : Visitor
{
    public override void PlayerVisit(Player player)
    {
        player.Die();
    }

    public override void SimpleEnemyVisit(SimpleEnemy simpleEnemy)
    {
        throw new System.NotImplementedException();
    }
}
