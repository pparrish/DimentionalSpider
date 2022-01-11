using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class HealBar : MonoBehaviour
   {
      private Image _healImage;
      private void Start()
      {
         _healImage = GetComponent<Image>();
      }

      public void OnLifeChange(ILifeStatistic life)
      {
         if (life.Life.Actual == 0)
         {
            _healImage.fillAmount = 0;
            return;
         }
         _healImage.fillAmount = life.Life.Actual / life.Life.Total;
      }
   }
}
