using UnityEngine;
public class Pollen : MonoBehaviour
{
    private void OnDestroy()
    {
        PollenFactory.Instance.PollenList.Remove(gameObject);
    }
}