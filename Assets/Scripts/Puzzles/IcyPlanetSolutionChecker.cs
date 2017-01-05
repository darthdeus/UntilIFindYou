using UnityEngine;
using System.Collections;

public class IcyPlanetSolutionChecker : MonoBehaviour
{

    public GameObject Table1;  // Solution: Carrot
    public GameObject Table2;  // Solution: Icicle
    public GameObject Table3;  // Solution: Scarf
    public GameObject Table4;  // Solution: Snowflakes

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckSolution()
    {
        Debug.Log(Table1.GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log(Table2.GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log(Table3.GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log(Table4.GetComponent<SpriteRenderer>().sprite.name);
        if (Table1.GetComponent<SpriteRenderer>().sprite.name == "TableCarrot" &&
            Table2.GetComponent<SpriteRenderer>().sprite.name == "TableIcicle" &&
            Table3.GetComponent<SpriteRenderer>().sprite.name == "TableScarf" &&
            Table4.GetComponent<SpriteRenderer>().sprite.name == "TableSnowflakes")
        {
            CorrectSolution();
        }
        else
        {
            WrongSolution();
        }
    }

    void WrongSolution()
    {
        Table1.GetComponent<SpriteManager>().Default();
        Table2.GetComponent<SpriteManager>().Default();
        Table3.GetComponent<SpriteManager>().Default();
        Table4.GetComponent<SpriteManager>().Default();
        Fungus.Flowchart.BroadcastFungusMessage("IcyWrong");
    }

    void CorrectSolution()
    {
        Fungus.Flowchart.BroadcastFungusMessage("IcyCorrect");
    }
}
