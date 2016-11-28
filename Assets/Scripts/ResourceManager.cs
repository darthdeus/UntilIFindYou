using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public enum ResourceType {
        Wood,
        Stone
    }

    public enum ToolType {
        Axe,
        Pickaxe
    }

    public int ResourceCount(ResourceType type) {
        switch (type) {
            case ResourceType.Wood:
                return 3;
            case ResourceType.Stone:
                return 0;
            default:
                throw new InvalidOperationException("Invalid resource type");

        }
    }

    public bool HasTool(ToolType type) {
        switch (type) {
            case ToolType.Axe:
                return true;
            case ToolType.Pickaxe:
                return false;
            default:
                throw new InvalidOperationException("Invalid tool type");
        }
    }
}