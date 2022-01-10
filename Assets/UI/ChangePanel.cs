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
            if(next != null) next.SetActive(true);
            actual.SetActive(false);
        }

        public void SetPanels(GameObject newActual, GameObject newNext)
        {
            actual = newActual;
            next = newNext;
        }
    }
}
