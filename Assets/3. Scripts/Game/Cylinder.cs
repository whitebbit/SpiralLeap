using System;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Cylinder: MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _meshRenderer.material = GameManager.CurrentTheme.Cylinder;
        }
    }
}