
//Visitors describe how an ObjectWithCollision tries to move to a space
//Elements like a wall don't need visitors
public abstract class Visitor
{
    public abstract void PlayerVisit(Player player);
    public abstract void SimpleEnemyVisit(SimpleEnemy simpleEnemy);
}
