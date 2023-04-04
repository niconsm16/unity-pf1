using UnityEngine;

namespace MainMenu.Scripts
{
    public class ExitScript : MonoBehaviour
    {
        public void CloseGame() =>
            Application.Quit();
    }
}