
using UnityEngine;

namespace UI
{
   public class PauseGame : MonoBehaviour
   {
      private bool _isPaused;

      public void TogglePause()
      {
         if (!_isPaused)
         {
            Time.timeScale = 0;
            _isPaused = true;
            return;
         }

         Time.timeScale = 1;
         _isPaused = false;
      }

      private void OnApplicationFocus(bool hasFocus)
      {
         if (!hasFocus && !_isPaused)
         {
            TogglePause();
         }
      }
   }
}
