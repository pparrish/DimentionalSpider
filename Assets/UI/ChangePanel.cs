using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChangePanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject actual;
        [SerializeField]
        private GameObject next;
        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Change);
        }

        private void Change()
        {
            actual.SetActive(false);
            if(next == null)  return;
            next.SetActive(true);
        }

        public void SetPanels(GameObject newActual, GameObject newNext)
        {
            actual = newActual;
            next = newNext;
        }
    }
}
