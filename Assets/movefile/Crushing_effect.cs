
using UnityEngine;

public class CircleExplosion : MonoBehaviour
{
    public GameObject fragmentPrefab;
    public int numberOfFragments = 10;
    public float explosionForce = 5.0f;

    private GeometryCollision.Circle myCircle; 
    private float myRadius; 


    private void Start()
    {
        
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            myRadius = collider.radius;
        }
        myCircle = new GeometryCollision.Circle(transform.position.x, transform.position.y, myRadius);
    }

    private void Update()
    {
        CheckForCollisions();
    }

    private void CheckForCollisions()
    {
        
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            CircleCollider2D ballCollider = ball.GetComponent<CircleCollider2D>();
            if (ballCollider)
            {
                GeometryCollision.Circle ballCircle = new GeometryCollision.Circle(ball.transform.position.x, ball.transform.position.y, ballCollider.radius);
                if (GeometryCollision.CircleCircleCollision(myCircle, ballCircle))
                {
                    Explode();
                    Destroy(gameObject);
                    Debug.Log("Explode method called!");
                    return; 
                }
            }
        }
    }

    void Explode()
    {
        ScoreManager.instance.AddScoreOnExplosion(10); 
        for (int i = 0; i < numberOfFragments; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
            fragment.AddComponent<Rigidbody2D>();
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            fragment.GetComponent<Rigidbody2D>().AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
        }
    }

}
