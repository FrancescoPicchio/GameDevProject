using UnityEngine;

//WARNING If wall has composite collider 2d this doesn't work.
//Overlap point won't work also if TilemapCollider is set to merge
public class Wall : ObjectWithCollision
{
    void Start()
    {
        visitor = new WallVisitor();
    }

    //Wall doesn't need to implement accept because it's not supposed to move
    //It only implements WallVisitor to let other objects handle walls
    public override void Accept(Visitor v) { }
}
