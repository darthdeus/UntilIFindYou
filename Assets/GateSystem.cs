﻿using UnityEngine;
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

    public static GateSystem FindGateSystem() {
        var gateSystemObjects = GameObject.FindGameObjectsWithTag("GateSystem");
        Debug.Assert(gateSystemObjects.Length == 1, "There should be only one GateSystem object");

        var _gateSystem = gateSystemObjects[0].GetComponent<GateSystem>();
        Debug.Assert(_gateSystem != null, "Found a GateSystem object without the appropriate script.");

        return _gateSystem;
    }
}
