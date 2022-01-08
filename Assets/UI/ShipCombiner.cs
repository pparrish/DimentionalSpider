using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShipCombiner : MonoBehaviour
    {
        private Statistics _shipToCombine;

        public void SelectShipToCombine(Statistics ship)
        {
            // Select first ship
            if (_shipToCombine == null)
            {
                _shipToCombine = ship;
                ChangeButtonColors(ship.gameObject, Color.cyan);
                return;
            }

            // Select the same ship, cancel.
            if (_shipToCombine == ship)
            {
                _shipToCombine = null;
                ChangeButtonColors(ship.gameObject, Color.white);
                return;
            }
        
            _shipToCombine.CombineStatistics(ship);
            _shipToCombine.GetComponent<ShipUI>().SetStatsFills();
            ChangeButtonColors(_shipToCombine.gameObject, Color.white);
        
            Destroy(ship.gameObject);
            _shipToCombine = null;
        }

        private static void ChangeButtonColors(GameObject shipGameObject, Color color)
        {
            var buttonComponent = shipGameObject.transform.Find("CombineButton").GetComponent<Button>();
            var buttonColors = buttonComponent.colors;
            buttonColors.normalColor = color;
            buttonColors.selectedColor = color;
            buttonComponent.colors = buttonColors;
        }

    }
}