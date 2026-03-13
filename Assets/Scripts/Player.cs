using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

//The player sprite is centered to the cells by having the grid class be offset by 0.5,0.5
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 targetPosition;
    private Vector3 direction;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0f)
        {
            //TODO Add additional logic to check for walls, enemies or pitfalls.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);         
        }
        else
        {
            //TODO Use newer input method to handle multiple control schemes
            //TODO Keep the player moving if they hold a direction key down
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                if(!IsWallInTheWay(Vector3.up))
                    targetPosition +=  Vector3.up;
                }
            else
            if (Input.GetKeyDown(KeyCode.DownArrow)){
                if(!IsWallInTheWay(Vector3.down))
                    targetPosition +=  Vector3.down;
                }
            else
            if (Input.GetKeyDown(KeyCode.LeftArrow)){
                if(!IsWallInTheWay(Vector3.left))
                    targetPosition +=  Vector3.left;
                }
            else
            if (Input.GetKeyDown(KeyCode.RightArrow)){
                if(!IsWallInTheWay(Vector3.right))
                    direction = Vector3.right;
                }
            else direction = Vector3.zero;

            targetPosition += direction;
        }
    }

    private bool IsWallInTheWay(Vector3 movementDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, 1f, LayerMask.GetMask("Wall"));
        if(hit)
            Debug.Log("Hit a wall!");
        return hit;
    }
    
}
