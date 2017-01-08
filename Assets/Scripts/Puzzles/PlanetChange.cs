using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Puzzles
{
    class PlanetChange : MonoBehaviour
    {
        public string PlanetName;
        public Material ChangedMaterial;

        void ChangePlanet()
        {
            Debug.Log("Planet should change now.");
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            renderer.material = ChangedMaterial;
            renderer.materials[0] = ChangedMaterial;

            var col = gameObject.GetComponent<EdgeCollider2D>();

            var originalCollider = GameObject.FindGameObjectWithTag("HomePlanet").GetComponent<EdgeCollider2D>();
            col.points = originalCollider.points;

            //Only for DEATH planet
            if (PlanetName == "death") {
                var gameObjects = GameObject.FindGameObjectsWithTag("Lake");

                for (var i = 0; i < gameObjects.Length; i++)
                    Destroy(gameObjects[i]);
            }
        }
    }
}
