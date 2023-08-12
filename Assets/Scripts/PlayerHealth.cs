using NarvalDev.Runner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerHealth : AbstractSingleton<PlayerHealth>
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerData playerData;
    GameManager m_GameManager;

    public EventHandler<OnHitEventArgs> OnHit;
    public class OnHitEventArgs: EventArgs
    {
        public float hitAmount;
    }
    
    private float currentColorTime = 0f;
    private float currentNoColorTime = 0f;
    private float currentHealth;


    void Start()
    {
        currentHealth = playerData.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            float durationFrame = Time.deltaTime;

            if (currentColorTime < playerData.colorDuration)
            {
                float ratio = currentColorTime / playerData.colorDuration;
                float grayAmount = ratio;
                SetGrayScale(grayAmount);
                currentColorTime += durationFrame;
            }
            else
            {
                currentNoColorTime += durationFrame;
                if (currentNoColorTime >= playerData.hitNoColorTime)
                {
                    float currentHitAmount = 1 / playerData.maxHealth;
                    currentHealth -= currentHitAmount;
                    OnHit?.Invoke(this, new OnHitEventArgs { hitAmount = currentHitAmount });
                    currentNoColorTime = 0f;
                }
            }
        }
        
    }
    public void ResetGrayscaleTime() 
    {
        currentColorTime = 0f;
    }
    public void SetGrayScale(float amount = 1) 
    {
        spriteRenderer.material.SetFloat("_GrayscaleAmount", amount);
    }


}
