using Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class InitGameManager : MonoBehaviour
    {
        [SerializeField] private SpiderVisionEventBus spiderVisionEventBus;
        [SerializeField]
        private GameObject shipPrefab;
        [SerializeField]
        private GameObject spiderPrefab;
        [SerializeField]
        private Transform startPoint;
        [SerializeField]
        private Transform spiderStartPoint;

        private Statistics _shipStats;
        
        public UnityEvent<Transform> onPlayerShipCreated;
    

        public void StartGame()
        {
            CreateSpider();
            var player = CreatePlayerShip();
            spiderVisionEventBus.Player = player;
        }
        
        // Spider Creator maybe
        private void CreateSpider()
        {
            Instantiate(spiderPrefab, spiderStartPoint.position, spiderStartPoint.rotation);
        }

        // Ship creator maybe
        private GameObject CreatePlayerShip()
        {
            var playerShip = Instantiate(shipPrefab, startPoint.position, startPoint.rotation);
            onPlayerShipCreated?.Invoke(playerShip.transform);
            playerShip.GetComponent<Statistics>().SetStats(_shipStats);
            return playerShip;
        }

        public void ChangeShipStats(Statistics stats)
        {
            _shipStats = stats;
        }
    }
}
