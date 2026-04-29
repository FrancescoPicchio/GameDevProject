using UnityEngine;

public class SimpleEnemyVisitor : Visitor
{
    public override void PlayerVisit(Player player)
    {
        player.Die();
    }

    public override void SimpleEnemyVisit(SimpleEnemy simpleEnemy)
    {
        //Debug statement just to check if a simpleEnemy can see itslef
        Debug.Log("lmao");
    }
}
