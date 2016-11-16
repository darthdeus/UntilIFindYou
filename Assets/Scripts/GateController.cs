using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;
using Fungus;

public class GateController : MonoBehaviour {
    public GameObject connectedGate;
    public string address;

    private GateSystem _gateSystem;
    private GameObject _player;
    private DialingManager _dialingManager;

    void Start() {
        _gateSystem = GateSystem.FindGateSystem();
        _gateSystem.ConnectGate(address, this);

        _player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(_player != null, "Player is missing!");

        _dialingManager = GetComponentsInChildren<DialingManager>().FirstOrDefault();
        Debug.Assert(_dialingManager != null, "Dialing Manager is missing!");
    }

    void OnMouseUp() {
        Debug.Log("Klikam");
        _dialingManager.Activate(_player); 
    }
}
