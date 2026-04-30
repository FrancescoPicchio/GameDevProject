using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

//The player sprite is centered to the cells by having the grid class be offset by 0.5,0.5
public class Player : ObjectWithCollision
{
    private EventHandler eventHandler;
    private bool canMove = false;
    public UnityEvent playerMoved;

    [SerializeField]
    private float moveSpeed;
    private Vector3 direction;

    void Start()
    {
        visitor = new PlayerVisitor();
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
        {
            playerMoved.AddListener(eventHandler.callEnemies);
            eventHandler.playerTurn.AddListener(this.setCanMove);
        }
        else
            Debug.Log("Couldn't find EventHandler");
        targetPosition = transform.position;
    }

    void Update()
    {
        MoveCharacter();
    }

    public override void Accept(Visitor v)
    {
        v.PlayerVisit(this);
    }

    public void Stop()
    {
        setCanMove();
        targetPosition = transform.position;
        //player movement has finished
        playerMoved.Invoke();
    }

    public void Die()
    {
        //Resets level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void setCanMove()
    {
        if (canMove)
            canMove = false;
        else
            canMove = true;
    }

    void MoveCharacter()
    {
        if (canMove && Vector3.Distance(transform.position, targetPosition) > 0f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            if (transform.position == targetPosition)
            {
                canMove = false;
                playerMoved.Invoke();
            }
        }
        // else if (canMove)
        // {
        //     Debug.Log("plaeyr finished");
        //     playerMoved.Invoke();
        //     canMove = false;
        // }
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
        targetPosition += movementDirection;
        //Setting canMove before so Accept can modify it.
        canMove = true;
        LookForObjectWithCollision();
        //TODO the event should be called for any type of player input, not just movement
        // canMove = false;
    }
}
