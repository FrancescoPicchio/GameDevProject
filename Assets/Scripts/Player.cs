using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

//The player sprite is centered to the cells by having the grid class be offset by 0.5,0.5
public class Player : MonoBehaviour
{
    private EventHandler eventHandler;
    public UnityEvent playerMoved;

    [SerializeField]
    private float moveSpeed;
    private Vector3 targetPosition;
    private Vector3 direction;

    void Start()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
            playerMoved.AddListener(eventHandler.callEnemies);
        else
            Debug.Log("Couldn't find EventHandler");
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
        if (!IsWallInTheWay(movementDirection))
        {
            targetPosition += movementDirection;
        }
        //TODO the event should be called for any type of player input, not just movement
        playerMoved.Invoke();
    }

    private bool IsWallInTheWay(Vector3 movementDirection)
    {
        RaycastHit2D wallIsInTheWay = Physics2D.Raycast(
            transform.position,
            movementDirection,
            1f,
            LayerMask.GetMask("Wall")
        );
        if (wallIsInTheWay)
            Debug.Log("Hit a wall!");
        return wallIsInTheWay;
    }
}
