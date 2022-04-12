using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int intPointsForCoins = 100;

    bool bolWasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.tag == "Player" && !bolWasCollected)
       {
           bolWasCollected = true;
           FindObjectOfType<GameSession>().AddToScore(intPointsForCoins);
           AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
           gameObject.SetActive(false);
           Destroy(gameObject);
       }
   }
}
