using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BoxCollisionHandler : MonoBehaviour
{
    public Sprite collisionSprite; 
    public PinBallGame pinBallGameInstance; 
    public AudioClip collisionSound; 

    private GeometryCollision.Box boxCollider;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool hasCollided = false; 
    public GameObject collisionEffects; 

    private void Start()
    {
  
        float halfWidth = transform.localScale.x / 2f;
        float halfHeight = transform.localScale.y / 2f;
        boxCollider = new GeometryCollision.Box(transform.position.x, transform.position.y, halfWidth * 2, halfHeight * 2);

        
        spriteRenderer = collisionEffects.GetComponent<SpriteRenderer>();
        audioSource = collisionEffects.GetComponent<AudioSource>();
        audioSource.clip = collisionSound; 
        spriteRenderer.sprite = null; 
    }

    private void Update()
    {
        if (hasCollided) return; 

        if (!pinBallGameInstance) return; 

        foreach (var ball in pinBallGameInstance.balls) 
        {
            if (GeometryCollision.CircleBoxCollision(ball, boxCollider))
            {
                HandleCollision();
                Debug.Log("Collision detected!"); 
                break;
            }
        }
    }

    void HandleCollision()
    {
        
        SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
        if (mainSpriteRenderer) mainSpriteRenderer.enabled = false;

        if (collisionSprite)
        {
            spriteRenderer.sprite = collisionSprite;
        }

        if (audioSource && !audioSource.isPlaying) 
        {
            audioSource.Play(); 
        }

        hasCollided = true; 

       
        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider2D) boxCollider2D.enabled = false;
    }

}
