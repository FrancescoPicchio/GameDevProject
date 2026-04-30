using UnityEngine.SceneManagement;

public class PlayerVisitor : Visitor
{
    public override void PlayerVisit(Player player)
    {
        //there's only one player. Change if otherwise
        return;
    }

    public override void SimpleEnemyVisit(SimpleEnemy simpleEnemy)
    {
        //Resets the level if player moves on an enemy
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
