using UnityEngine;

public class SimpleEnemy : ObjectWithCollision
{
    void Start()
    {
        visitor = new SimpleEnemyVisitor();
    }

    void Update() { }

    public override void Accept(Visitor v)
    {
        v.SimpleEnemyVisit(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy found collision");
    }
}
