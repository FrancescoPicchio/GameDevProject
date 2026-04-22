using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Axis {horizontal, vertical};

    [SerializeField]
    private Axis movementAxis;
    private Vector3 direction;

    void Start()
    {
        if(movementAxis == Axis.horizontal)
            direction = Vector3.right;
        else
            direction = Vector3.up;
    }

    void Update()
    {
        
    }

    public void Move(){
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Wall"));
        if(wallHit)
            direction *= -1;
        transform.position += direction;
    }
}
