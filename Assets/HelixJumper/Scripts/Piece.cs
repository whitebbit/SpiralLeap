using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HelixJumper.Scripts
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Collider collider;


        [SerializeField] private MeshRenderer meshRenderer;
        public void ChangeMaterial(Material material)
        {
            meshRenderer.material = material;
        }
        
        public void Destroy(float delay)
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            transform.parent = null;
            var xForce = transform.position.x >= 0 ? Random.Range(0.1f, 1f) : Random.Range(-1f, -0.1f);
            rigidbody.AddForce(new Vector3(xForce, .25f, 0) * 25,
                ForceMode.Impulse);

            collider.isTrigger = true;

            transform.DOScale(Vector3.zero, delay * 0.25f)
                .OnComplete(() => gameObject.SetActive(false))
                .SetDelay(delay);
        }
    }
}