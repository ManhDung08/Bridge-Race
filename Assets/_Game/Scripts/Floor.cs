using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private MeshRenderer brickRenderer;
    [SerializeField] private GameObject Floorbricks;
    [SerializeField] private Bounds bounds;
    [SerializeField] private GameObject player;

    public List<GameObject> floorBricks = new List<GameObject>();

    public List<ColorType> colors = new List<ColorType>(); //Danh sach mau cac nhan vat o tren floor


    void Start()
    {
        bounds.center = transform.position;
        bounds.extents = new Vector3(20.0f, 0, 20.0f);
        SpawnAllBricks();
    }

    private void SpawnAllBricks()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                GameObject obj = Instantiate(brickPrefab, Floorbricks.transform);
                obj.transform.localPosition = new Vector3(-18f + 3 * i, 0.75f, -18f + 3 * j);
                obj.GetComponent<BrickController>().SetRandomAllColors(obj);
                obj.GetComponent<BrickController>().DeactiveBrick(obj);
                floorBricks.Add(obj);
            }
        }
    }

    private void TurnActiveBrick(ColorType color)
    {
        for (int i = 0; i < floorBricks.Count; i++)
        {
            if (floorBricks[i].GetComponent<BrickController>().brickColor == color)
            {
                floorBricks[i].GetComponent<BrickController>().ActiveBrick(floorBricks[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAndBots"))
        {
            ColorType color = other.GetComponent<Character>().color;
            if (!colors.Contains(color))
            {
                TurnActiveBrick(color);
                colors.Add(color);
            }
            else
            {
                return;
            }
        }
    }

    public void LimitArea()
    {
        if (player.transform.position.x >= bounds.max.x)
        {
            player.transform.position = new Vector3(bounds.max.x, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.z >= bounds.max.z)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, bounds.max.z);
        }
        if (player.transform.position.x <= bounds.min.x)
        {
            player.transform.position = new Vector3(bounds.min.x, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.z <= bounds.min.z)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, bounds.min.z);
        }

    }
}
