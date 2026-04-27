using UnityEngine;

public abstract class Visitor
{
    public abstract void PlayerVisit(Player player);
    public abstract void SimpleEnemyVisit(SimpleEnemy simpleEnemy);
}
