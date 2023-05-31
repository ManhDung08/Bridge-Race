using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float speed;


    private float maxSlopeAngle = 90f;
    private float rotationSpeed = 720f;
    private float horizontal;
    private float vertical;
    private Vector3 slopeNormal;
    private Vector3 direction;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Move();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Stop();
        }
    }

    private void Move()
    {
        ChangeAnim(AnimationType.run);
        horizontal = variableJoystick.Horizontal;
        vertical = variableJoystick.Vertical;
        direction = new Vector3(horizontal, 0f, vertical);
        if (direction != Vector3.zero)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out hit, 10f, Bridge))
            {
                if (vertical > 0)
                {
                    if (numColectedBricks > 0)
                    {
                        this.CheckBrick();
                    }
                    else 
                    {
                        direction = new Vector3(horizontal, 0f, 0f);
                    }
                    
                }
                this.bridge.LimitArea();
                this.MoveOnBridge(direction, hit);
            }
            else
            {
                floor.LimitArea();
                transform.position = new Vector3(transform.position.x, floorPos.y, transform.position.z);
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                transform.position += direction * speed * Time.deltaTime;
            }
        }
        
    }

    private void MoveOnBridge(Vector3 direction, RaycastHit hit)
    {
        slopeNormal = hit.normal;
        Vector3 movementDirection = Vector3.zero;
        float slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
        if (slopeAngle <= maxSlopeAngle)
        {
            movementDirection = Vector3.ProjectOnPlane(direction, slopeNormal).normalized;
        }

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    private void Stop()
    {
        ChangeAnim(AnimationType.idle);
    }

    

}
