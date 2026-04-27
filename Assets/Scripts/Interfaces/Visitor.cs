using UnityEngine;

public abstract class Visitor : MonoBehaviour
{
    public abstract void PlayerVisit(Player player);
    public abstract void SimpleEnemyVisit(SimpleEnemy simpleEnemy);
}
