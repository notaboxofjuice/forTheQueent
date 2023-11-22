using UnityEngine;
public class Pollen : MonoBehaviour
{
    private void OnDestroy()
    {
        PollenFactory.PollenList.Remove(gameObject);
    }
}