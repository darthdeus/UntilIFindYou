using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.ResourceManagement;
using Fungus;

public class GateSystem : MonoBehaviour {
    private readonly Dictionary<string, GateController> _connectedGates = new Dictionary<string, GateController>();
    private bool _banditsIntro = true;
    private int _banditsChance;
    private GameObject _banditSprite;

    public void Start()
    {
        _banditSprite = GameObject.FindGameObjectWithTag("BanditSprite");
        _banditSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

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

            if (address == "abcde") {
                if (_banditsIntro) {
                    Fungus.Flowchart.BroadcastFungusMessage("BanditAttackIntro");
                    _banditsIntro = false;
                } else {

                    _banditsChance = Random.Range(0, 10);
                    if (_banditsChance < 5) {
                        _banditSprite.GetComponent<SpriteRenderer>().enabled = true;
                        Fungus.Flowchart.BroadcastFungusMessage("BanditAttack");
                    } else {
                        _banditSprite.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }

            return true;
        } else {
            return false;
        }
    }

    public bool CheckCorrectAddress(string address)
    {
        if (_connectedGates.ContainsKey(address)) {
            return true;
        } else {
            return false;
        }
    }

    public static GateSystem Find() {
        var gateSystemObjects = GameObject.FindGameObjectsWithTag("GateSystem");
        Debug.Assert(gateSystemObjects.Length == 1, "There should be only one GateSystem object");

        var gateSystem = gateSystemObjects[0].GetComponent<GateSystem>();
        Debug.Assert(gateSystem != null, "Found a GateSystem object without the appropriate script.");

        return gateSystem;
    }
}
