using System;
using UnityEngine;

namespace _3._Scripts.Game
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FogPlane: MonoBehaviour
    {
        private void Start()
        {
            var component = GetComponent<MeshRenderer>();
            component.material.color = GameManager.CurrentTheme.Fog;
        }
    }
}