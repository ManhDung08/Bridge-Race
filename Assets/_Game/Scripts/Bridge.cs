using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Bounds bounds;


    public bool isFullBrick = false;

    private void Start()
    {
        bounds.center = transform.position;
        bounds.extents = new Vector3(0.4f, 0f, 7f);
    }

    public void LimitArea()
    {
        if (player.transform.position.x >= bounds.max.x)
        {
            player.transform.position = new Vector3(bounds.max.x, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.x <= bounds.min.x)
        {
            player.transform.position = new Vector3(bounds.min.x, player.transform.position.y, player.transform.position.z);
        }
    }
}
