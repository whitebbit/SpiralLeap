using _3._Scripts.UI;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Finish: MonoBehaviour
    {

        private bool _finished;
        
        public void Finishing()
        {
            if(_finished) return;
            _finished = true;
            GameManager.instance.ChangePanel(GameManager.instance.WinPanel);
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.OnWin);
        }
    }
}