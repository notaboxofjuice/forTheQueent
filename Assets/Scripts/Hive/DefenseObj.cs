using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

//Working on this script: Ky'onna
public class DefenseObj : MonoBehaviour
{
    [HideInInspector] public DefenseSocket myDefenseSocket;
    [HideInInspector] public int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] float damageCooldown;

    [Tooltip("Health loss rate per cooldown")]
    [SerializeField] int deteriorateRate;
    private bool canTakeDamage;

    [SerializeField] bool deteriorateOverTime;

    private void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    private void Update()
    {
        if (deteriorateOverTime)
        {
            TakeDamage(deteriorateRate);
        }
        
    }

    public void TakeDamage(int _damageAmount)
    {
        if (canTakeDamage)
        {
            //take damage
            currentHealth -= _damageAmount;

            //start the cooldown, so we don't take repeated damage
            canTakeDamage = false;
            StartCoroutine(DamageCooldown());

            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private void OnDestroy()
    {
        //Set the defense socket to unnoccupied
        myDefenseSocket.isOccupied = false;
    }

    
}
