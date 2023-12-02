using System;
using HelixJumper.Scripts;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Cylinder: MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        private void Start()
        {
            meshRenderer.material = GameManager.CurrentTheme.Cylinder;
        }
        private void OnEnable()
        {
            MoveController.Move += Move;
        }

        private void OnDisable()
        {
            MoveController.Move -= Move;
        }
        private void Move()
        {
            if (!Ball.IsGameOver && GameManager.isStarted)
            {
                transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 10f, Space.World);
            }
        }
    }
}