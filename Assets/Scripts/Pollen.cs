using UnityEngine;
public class Pollen : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyPollen", 10);
    }
    private void DestroyPollen()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        PollenFactory.PollenList.Remove(gameObject);
    }
}