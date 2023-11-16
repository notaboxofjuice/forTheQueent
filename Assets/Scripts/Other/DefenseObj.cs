using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna
public class DefenseObj : MonoBehaviour
{
    public DefenseSocket myDefenseSocket;
    public int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] float damageCooldown;
    private bool canTakeDamage;

    private void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    public void TakeDamage(int _damageAmount)
    {
        //take damage
        currentHealth =- _damageAmount;

        //start the cooldown, so  we don't take repeated damage
        canTakeDamage = false;
        StartCoroutine(DamageCooldown());
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
