using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject skin;
    [SerializeField] protected LayerMask BridgeBrick;
    [SerializeField] protected LayerMask Bridge;



    public ColorType color;

    protected List<GameObject> collectedBricks = new List<GameObject>(); //List luu tru so luong brick (ca active va deactive hien tai)
    protected int numColectedBricks = 0;   //Bien luu tru so luong brick dang active
    protected Floor floor;
    protected Bridge bridge;
    protected Vector3 floorPos;

    protected enum AnimationType
    {
        idle = 0,
        run = 1,
        win = 2
    }
    protected AnimationType currentAnimType;

    protected void ChangeAnim(AnimationType animType)
    {
        if(animType != currentAnimType)
        {
            Anim.ResetTrigger(currentAnimType.ToString());
            currentAnimType = animType;
            Anim.SetTrigger(currentAnimType.ToString());
        }
    }


    protected void CheckBrick()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out hit, 5f, BridgeBrick))
        {
            MeshRenderer meshRenderer = hit.transform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            Renderer renderer = hit.transform.GetComponent<Renderer>();
            if (!renderer.material.name.StartsWith(skin.GetComponent<Renderer>().material.name))
            {
                renderer.material = skin.GetComponent<Renderer>().material;
                if (numColectedBricks > 0)
                {
                    RemoveOneBrick();
                }
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            floor = other.GetComponent<Floor>();
            floorPos = other.transform.position;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Bridge"))
        {
            GameObject vbridge = other.transform.parent.gameObject;
            bridge = vbridge.GetComponent<Bridge>();
        }
    }

    public void AddBrick()
    {
        if(collectedBricks.Count <= numColectedBricks)
        {
            GameObject brick = Instantiate(brickPrefab, this.transform);
            brick.transform.localPosition = new Vector3(0f, 1.5f, -0.7f) + Vector3.up * 0.5f * collectedBricks.Count;
            brick.GetComponent<Renderer>().material = skin.GetComponent<Renderer>().material;
            collectedBricks.Add(brick);
        }
        else
        {
            collectedBricks[numColectedBricks].GetComponent<BrickController>().ActiveBrick(collectedBricks[numColectedBricks]);
        }
        numColectedBricks++;
    }

    protected void RemoveOneBrick()
    {
        collectedBricks[numColectedBricks - 1].GetComponent<BrickController>().DeactiveBrick(collectedBricks[numColectedBricks - 1]);
        numColectedBricks--;
    }

    protected void RemoveAllBricks()
    {
        for(int i=0; i<collectedBricks.Count; i++)
        {
            collectedBricks.Clear();
        }
    }
}
