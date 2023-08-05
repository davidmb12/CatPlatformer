using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_UI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private RectTransform healthBarFill;

    private void Start()
    {
        playerHealth.OnHit += PlayerHealth_OnHitAction;
    }

    private void PlayerHealth_OnHitAction(object sender, PlayerHealth.OnHitEventArgs e) 
    {
        
        healthBarFill.localScale = new Vector3(healthBarFill.localScale.x - e.hitAmount, healthBarFill.localScale.y);
    }
}
