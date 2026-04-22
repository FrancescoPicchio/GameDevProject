using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Axis {horizontal, vertical};

    [SerializeField]
    private EventHandler eventHandler;
    [SerializeField]
    private Axis movementAxis;
    private Vector3 direction;

    void Start()
    {
        //TODO flip sprite based on direction
        if(movementAxis == Axis.horizontal)
            direction = Vector3.right;
        else
            direction = Vector3.up;
        eventHandler.subscribeEnemy(this);
    }

    public void Move()
    {
        RaycastHit2D wallIsInFront = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Wall"));
        if(wallIsInFront)
            //TODO flip sprite
            direction *= -1;
        transform.position += direction;
    }
}
