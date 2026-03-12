using UnityEngine;

//The player sprite is centered to the cells by having the grid class be offset by 0.5,0.5
public class Player : MonoBehaviour
{
    public float moveSpeed = 15f;
    private Vector3 targetPosition;

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
            if (Input.GetKeyDown(KeyCode.UpArrow))
                targetPosition += Vector3.up;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                targetPosition += Vector3.down;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                targetPosition += Vector3.left;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                targetPosition += Vector3.right;
        }
    }
    
}
