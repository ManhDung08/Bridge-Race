using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private MeshRenderer brickRenderer;
    [SerializeField] private ColorData colorData;

    private Floor floor;
    public ColorType brickColor = ColorType.None;
    public bool isActive;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            floor = other.GetComponent<Floor>();
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerAndBots"))
        {
            Character character = other.GetComponent<Character>();
            if(character.color == brickColor)
            {
                character.AddBrick();
                this.EatBrick();
            }
        }
    }

    private IEnumerator ResetBrick(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if(floor != null)
        {
            SetRandomColor(this.gameObject, floor.colors);
        }
        ActiveBrick(this.gameObject);
    }

    private void EatBrick()
    {
        DeactiveBrick(this.gameObject);
        StartCoroutine(ResetBrick(10f));
    }

    private void SetRandomColor(GameObject brick, List<ColorType> colorList)
    {
        int colorIndex = Random.Range(0, colorList.Count);
        brickRenderer.material = colorData.GetMat(colorList[colorIndex]);
        brickColor = colorList[colorIndex];
    }

    public void SetRandomAllColors(GameObject brick)
    {
        int colorIndex = Random.Range(1, 5);
        brickRenderer.material = colorData.GetMat((ColorType)colorIndex);
        brickColor = (ColorType)colorIndex;
    }

    

    public void ActiveBrick(GameObject brick)
    {
        brick.GetComponent<Renderer>().enabled = true;
        brick.GetComponent<BoxCollider>().enabled = true;
        isActive = true;
    }
    public void DeactiveBrick(GameObject brick)
    {
        brick.GetComponent<Renderer>().enabled = false;
        brick.GetComponent<BoxCollider>().enabled = false;
        isActive = false;
    }

    
}
