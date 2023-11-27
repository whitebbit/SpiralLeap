using System;
using UnityEngine;

namespace _3._Scripts.Game
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FogPlane: MonoBehaviour
    {
        private static readonly int Property = Shader.PropertyToID("_FogColor");

        private void Start()
        {
            var component = GetComponent<MeshRenderer>();
            component.material.SetColor(Property, GameManager.CurrentTheme.Fog);
        }
    }
}