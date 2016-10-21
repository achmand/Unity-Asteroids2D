using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class UiManager : MonoBehaviour
    {
        public Text healthText;
        public Text scoreText;
        public Image energyBar;
        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;

        void OnGUI()
        {
            Cursor.visible = false;
            var cursorSizeX = 40;
            var cursorSizeY = 40;
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSizeX / 2, Event.current.mousePosition.y - cursorSizeY / 2, cursorSizeX, cursorSizeY), cursorTexture);
        }

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
        public void SetHealth(int lives)
        {
            healthText.text = lives.ToString();
        }
        public void SetEnergyBar(float energy)
        {
            energyBar.fillAmount += energy;
        }
    }
}
