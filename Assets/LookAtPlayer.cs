using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,180);
    }
}
