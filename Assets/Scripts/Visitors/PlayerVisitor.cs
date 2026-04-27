using UnityEngine;

public class PlayerVisitor : Visitor
{
    public override void PlayerVisit(Player player)
    {
        //there's only one player. Change if otherwise
        return;
    }

    public override void SimpleEnemyVisit(SimpleEnemy simpleEnemy)
    {
        Debug.Log("player visitor called simple enemy visit");
    }
}
