using Common;
using UnityEngine;

public class ActivePanelOnDeath : MonoBehaviour
{
    public LifeEntityEventBus playerLifeEventBus;
    public GameObject PlayerPanel;
    public LifeEntityEventBus SpiderLifeEventBus;
    public GameObject SpiderPanel;
    // Start is called before the first frame update
    void Start()
    {
        playerLifeEventBus.onDeath?.AddListener(()=> {PlayerPanel.SetActive(true);});   
        SpiderLifeEventBus.onDeath?.AddListener(()=> {SpiderPanel.SetActive(true);});   
    }
}
