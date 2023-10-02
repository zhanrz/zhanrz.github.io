using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DisappearOnCollision : MonoBehaviour
{
    private GeometryCollision.Box boxCollider;
    public PinBallGame pinBallGameInstance;

    private void Start()
    {
       
        float halfWidth = transform.localScale.x / 2f;
        float halfHeight = transform.localScale.y / 2f;
        boxCollider = new GeometryCollision.Box(transform.position.x, transform.position.y, halfWidth * 2, halfHeight * 2);
    }

    private void Update()
    {
        if (!pinBallGameInstance) return;

        foreach (var ball in pinBallGameInstance.balls)
        {
            if (GeometryCollision.CircleBoxCollision(ball, boxCollider))
            {
                HandleCollision();
                break;
            }
        }
    }

    void HandleCollision()
    {
        
        Destroy(gameObject);
    }
}
