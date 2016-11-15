using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GateSystem : MonoBehaviour {
    private readonly Dictionary<string, GateController> _connectedGates = new Dictionary<string, GateController>();

    public void ConnectGate(string address, GateController gateController) {
        if (_connectedGates.ContainsKey(address)) {
            Debug.LogError(string.Format("Trying to connect a gate with an address that's already in use {0}", address));
        }

        _connectedGates[address] = gateController;
    }

    public bool DialAndMoveAddress(GameObject player, string address) {
        if (_connectedGates.ContainsKey(address)) {
            var gate = _connectedGates[address];
            player.gameObject.transform.position = gate.transform.position;
            return true;
        } else {
            return false;
        }
    }
}
