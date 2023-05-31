using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bots : Character
{

    private IState currentState;

    public NavMeshAgent agent;


    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public Vector3 DesOfClosestBrick()
    {
        float distance = 40f;
        Vector3 des = Vector3.zero;
        if(floor.floorBricks.Count >= 0)
        {
            for(int i=0; i<floor.floorBricks.Count; i++)
            {
                if (floor.floorBricks[i].GetComponent<BrickController>().isActive)
                {
                    if (floor.floorBricks[i].GetComponent<BrickController>().brickColor == this.color )
                    {
                        if(Vector3.Distance(this.transform.position, floor.floorBricks[i].transform.position) < distance )
                        {
                            distance = Vector3.Distance(this.transform.position, floor.floorBricks[i].transform.position);
                            des = floor.floorBricks[i].transform.position;
                        }
                    }
                }
            }
            return des;
        }
        return transform.position;
    }


}
