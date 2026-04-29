
public class PlayerVisitor : Visitor
{
    public override void PlayerVisit(Player player)
    {
        //there's only one player. Change if otherwise
        return;
    }

    public override void SimpleEnemyVisit(SimpleEnemy simpleEnemy)
    {
    }
}
