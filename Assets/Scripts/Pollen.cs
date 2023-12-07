using UnityEngine;
public class Pollen : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 60); // Destroy pollen after 60 seconds
    }
    private void OnDestroy()
    {
        PollenFactory.PollenList.Remove(gameObject);
    }
}