using System;
using YG;

namespace _3._Scripts.Game
{
    public static class MoneyWidget
    {
        public static event Action<int,int> OnChange;
        public static int money
        {
            get => YandexGame.savesData.money;
            set
            {
                OnChange?.Invoke(YandexGame.savesData.money, value);
                YandexGame.savesData.money = value;
                YandexGame.SaveProgress();
            }
        }
    }
}