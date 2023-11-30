using System.Collections;
using System.Threading.Tasks;
using _3._Scripts.Architecture.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace _3._Scripts.Game.Purchase
{
    public class PremiumPurchase : Purchase
    {
        [Header("Premium Purchase")]
        [SerializeField] private bool loadMainScene;
        [SerializeField] private Sprite secretSkinUnlockedIcon;

        public override void SuccessPurchased(string id)
        {
            if (id != this.id) return;

            StartCoroutine(SuccessPurchased());
        }

        public override void FailedPurchased(string id)
        {
            
        }
        
        private IEnumerator SuccessPurchased()
        {
            YandexGame.savesData.premium = true;
            MoneyWidget.money += 5000;
            
            var skin = Configuration.instance.skinsHolder.GetSkinByType(BuyType.Premium);
            skin.ChangeIcon(secretSkinUnlockedIcon);
            
            YandexGame.savesData.unlockedSkins.Add(skin.Name);
            
            yield return new WaitForSeconds(1);

            if (!loadMainScene) yield break;
            
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.OnReward);
            SceneManager.LoadScene("Main");
        }
    }
}