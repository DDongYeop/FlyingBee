using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Thorn"))
        {
            Debug.Log("Game Over!");
        }
    }
    void Update()
    {
        
    }
}
