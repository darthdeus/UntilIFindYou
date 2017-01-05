using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Carrot()
    {
        GetComponent<SpriteRenderer>().sprite = gameObject.transform.parent.GetComponent<TableSpriteHolder>().CarrotTable;
    }

    public void Scarf()
    {
        GetComponent<SpriteRenderer>().sprite = gameObject.transform.parent.GetComponent<TableSpriteHolder>().ScarfTable;
    }

    public void Snowflakes()
    {
        GetComponent<SpriteRenderer>().sprite = gameObject.transform.parent.GetComponent<TableSpriteHolder>().SnowflakesTable;
    }

    public void Icicle()
    {
        GetComponent<SpriteRenderer>().sprite = gameObject.transform.parent.GetComponent<TableSpriteHolder>().IcicleTable;
    }
    public void Default()
    {
        GetComponent<SpriteRenderer>().sprite = gameObject.transform.parent.GetComponent<TableSpriteHolder>().DefaultTable;
    }
}
