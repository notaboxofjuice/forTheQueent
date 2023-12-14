using UnityEngine;
public class DeathSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 2f);
    }
}