using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public KeyCode flipKey;
    public float flipAngle = 45.0f;
    public bool isRightFlipper = false;
    public float flipSpeed = 5.0f;
    public float bounceForce = 10.0f; 

    private Quaternion originalRotation;
    private Quaternion flippedRotation;
    private bool isFlipped = false;

    private bool IsMoving
    {
        get
        {
            return Quaternion.Angle(transform.rotation, isFlipped ? flippedRotation : originalRotation) > 0.1f;
        }
    }

    private void Start()
    {
        originalRotation = transform.rotation;
        flippedRotation = Quaternion.Euler(0, 0, isRightFlipper ? originalRotation.eulerAngles.z - flipAngle : originalRotation.eulerAngles.z + flipAngle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(flipKey) && !isFlipped)
        {
            isFlipped = true;
        }
        else if (Input.GetKeyUp(flipKey) && isFlipped)
        {
            isFlipped = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFlipped)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, flippedRotation, flipSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, flipSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball") && IsMoving)
        {
            Rigidbody2D ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb)
            {
                float forceDirection = isRightFlipper ? -1 : 1;
                ballRb.AddForce(new Vector2(0, forceDirection * bounceForce), ForceMode2D.Impulse);
            }
        }
    }
}
