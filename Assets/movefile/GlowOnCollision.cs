using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowOnCollision : MonoBehaviour
{
    public Color normalColor = Color.white;  
    public Color glowColor = Color.yellow;   
    private SpriteRenderer spriteRenderer;
    public float glowDuration = 2f;  
    private float timeSinceGlow;

    public AudioClip collisionSound; 
    private AudioSource audioSource; 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.color = normalColor;
        }

        
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; 
        }
        audioSource.clip = collisionSound;
    }

    private void Update()
    {
        if (timeSinceGlow > 0)
        {
            timeSinceGlow -= Time.deltaTime;
            if (timeSinceGlow <= 0)
            {
                spriteRenderer.color = normalColor;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (spriteRenderer)
            {
                spriteRenderer.color = glowColor;
                timeSinceGlow = glowDuration;
            }

      
            if (audioSource && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
