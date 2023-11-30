using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Game.Purchase
{
    public abstract class Purchase: MonoBehaviour
    {
        [SerializeField] protected string id;
        
        public abstract void SuccessPurchased(string id);
        public abstract void FailedPurchased(string id);
    }
}