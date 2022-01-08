using System.Collections.Generic;
using Common;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CreateShipsUI : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject shipUiPrefab;
        [SerializeField] private GameObject shipSelectionPanel;
        [SerializeField] private GameObject spiderSelectionPanel;
        [SerializeField] private InitGameManager initGameManager;
        [SerializeField] private List<StatsData> ships;
        private ShipCombiner _combiner;


        // Start is called before the first frame update
        private void Awake()
        {
            if (ships == null || ships.Count == 0)
            {
                ships = new List<StatsData>
                {
                    new StatsData()
                };
            }
        }

        void Start()
        {
            if (ships == null) return;
            _combiner = GetComponent<ShipCombiner>();
        
            foreach (var ship in ships)
            {
                //Instantiate ship
                var newShip = Instantiate(shipUiPrefab, content.transform);
                var newShipStats = newShip.GetComponent<Statistics>();
                newShipStats.SetStats(ship);
                //Move Ship before the create button 
                var newShipTransform = newShip.transform;
                newShipTransform.SetSiblingIndex(newShipTransform.GetSiblingIndex() - 1);
             
                var newShipButtonTransform = newShipTransform
                    .Find("SelectButton");
                newShipButtonTransform.GetComponent<Button>()
                    .onClick.AddListener(() => initGameManager.ChangeShipStats(newShipStats));
                var newShipButtonPanelChange = newShipButtonTransform.GetComponent<ChangePanel>();
                newShipButtonPanelChange.SetPanels(shipSelectionPanel, spiderSelectionPanel);
            
                newShipTransform
                    .Find("CombineButton")
                    .GetComponent<Button>()
                    .onClick.AddListener(() => _combiner.SelectShipToCombine(newShipStats));
            
            }
        }
    }
}
