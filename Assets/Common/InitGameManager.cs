using Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class InitGameManager : MonoBehaviour
    {
        [SerializeField] private SpiderVisionEventBus spiderVisionEventBus;
        [SerializeField] private LifeEntityEventBus playerLifeEventBus;
        [SerializeField] private LifeEntityEventBus spiderLifeEventBus;
        
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

        public GameObject aPlayer;


        private GameObject player;
        private GameObject spider;
        
        private void Start()
        {
            if (aPlayer)
            {
                spiderVisionEventBus.player = aPlayer;
                player = aPlayer;
            }
            playerLifeEventBus.onDeath.AddListener(() => Destroy(spider));
            spiderLifeEventBus.onDeath.AddListener(() => Destroy(player));
        }

        public void StartGame()
        {
            CreateSpider();
            spiderVisionEventBus.player = CreatePlayerShip();;
        }
        
        // Spider Creator maybe
        private void CreateSpider()
        {
            spider = Instantiate(spiderPrefab, spiderStartPoint.position, spiderStartPoint.rotation);
        }

        // Ship creator maybe
        private GameObject CreatePlayerShip()
        {
            var playerShip = Instantiate(shipPrefab, startPoint.position, startPoint.rotation);
            onPlayerShipCreated?.Invoke(playerShip.transform);
            playerShip.GetComponent<Statistics>().SetStats(_shipStats);
            player = playerShip;
            return playerShip;
        }

        public void ChangeShipStats(Statistics stats)
        {
            _shipStats = stats;
        }
    }
}
