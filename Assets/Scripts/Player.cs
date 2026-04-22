using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

//The player sprite is centered to the cells by having the grid class be offset by 0.5,0.5
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 targetPosition;
    private Vector3 direction;

    public UnityEvent playerMoved;

    void Start()
    {
        targetPosition = transform.position;
        playerMoved.AddListener(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().Move);
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
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            //TODO Use newer input method to handle multiple control schemes
            //TODO Keep the player moving if they hold a direction key down
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeDirection(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeDirection(Vector3.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeDirection(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeDirection(Vector3.right);
            }
            else
                direction = Vector3.zero;
        }
    }

    private void ChangeDirection(Vector3 movementDirection)
    {
        if(!IsWallInTheWay(movementDirection)){
            targetPosition += movementDirection;
        }
        //TODO the event should be called for any type of player input, not just movement
        playerMoved.Invoke();
    }

    private bool IsWallInTheWay(Vector3 movementDirection)
    {
        RaycastHit2D enemyDetector = Physics2D.Raycast(
            transform.position,
            movementDirection,
            1f,
            LayerMask.GetMask("Enemy")
        );
        if (enemyDetector && enemyDetector.transform.CompareTag("Enemy"))
            Debug.Log("ENEMY!!!!!");
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            movementDirection,
            1f,
            LayerMask.GetMask("Wall")
        );
        if (hit)
            Debug.Log("Hit a wall!");
        return hit;
    }
}
