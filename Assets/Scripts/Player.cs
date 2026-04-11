using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class Player : MovingObject
{

    public float moveSpeed = 15f;
    private Vector3 targetPosition;

    public static event Action PlayerActionDone;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 direction = GetDirection();
            if (direction!=Vector3.zero)
            {
                TryToMove(direction);
                PlayerActionDone?.Invoke();
            }
        }
    }

    override public bool CanMoveThere(Vector3 direction) 
    {
        return false;
    }
    override protected void Move(Vector3 direction)
    {
        targetPosition=transform.position+direction;
    }

    Vector3 GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector3.right;
        }

        return Vector3.zero;
    }

}
