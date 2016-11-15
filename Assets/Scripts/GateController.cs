using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour {
    public GameObject connectedGate;
    private GateSystem _gateSystem;
    public string address;

    void Start() {
        var gateSystemObjects = GameObject.FindGameObjectsWithTag("GateSystem");
        Debug.Assert(gateSystemObjects.Length == 1, "There should be only one GateSystem object");

        _gateSystem = gateSystemObjects[0].GetComponent<GateSystem>();
        Debug.Assert(_gateSystem != null, "Found a GateSystem object without the appropriate script.");

        _gateSystem.ConnectGate(address, this);
    }
}
