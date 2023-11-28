
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        // Ваши сохранения
        public int money;
        public string currentSkin;
        public int currentLevel;
        public bool premium;
        public bool soundActive;
        public bool reviewSent;
        public List<string> unlockedSkins;
        

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            currentSkin = "Ball";
            currentLevel = 1;
            soundActive = true;
        }
    }
}
