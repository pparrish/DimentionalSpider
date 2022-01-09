using Enemies;
using UnityEngine;

public class AddLimitsToSpiderVision : MonoBehaviour
{
    [SerializeField] private SpiderVisionEventBus spiderVisionEventBus;
    public Transform leftLimit;
    public Transform rightLimit;
    public void Start()
    {
        spiderVisionEventBus.rightLimit = rightLimit;
        spiderVisionEventBus.leftLimit = leftLimit;
    }
}
