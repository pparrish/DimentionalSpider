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
         _healImage.fillAmount = life.GetLife() / life.GetMaxLife();
      }
   }
}
