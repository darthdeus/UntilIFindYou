using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;
using Fungus;

public class GateController : MonoBehaviour
{
    public string address;
    private GateSystem _gateSystem;
    private GameObject _player;
    private DialingBook _dialingBook;

    void Start()
    {
        _gateSystem = GateSystem.Find();
        _gateSystem.ConnectGate(address, this);

        _player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(_player != null, "Player is missing!");

        _dialingBook = DialingBook.Find();
    }

    void OnMouseUp()
    {
        Debug.Log("Gate clicked");
        if (GetComponent<SpriteRenderer>().color.a != 0f)
            _dialingBook.Activate(_player, gameObject);
    }
}
