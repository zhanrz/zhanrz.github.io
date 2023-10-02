using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallGame : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public List<GeometryCollision.Circle> balls = new List<GeometryCollision.Circle>();
    public GeometryCollision.Box[] boxes;

    private class BallData
    {
        public GeometryCollision.Circle circle;
        public Rigidbody2D rb;

        public BallData(GeometryCollision.Circle circle, Rigidbody2D rb)
        {
            this.circle = circle;
            this.rb = rb;
        }
    }
    private List<BallData> ballDataList = new List<BallData>();

    void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnBall();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnBall()
    {
        float randomForce = Random.Range(30.0f, 50.0f);
        GameObject ballInstance = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = ballInstance.GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.LogError("not have a Rigidbody2D component!");
            return;
        }

        GeometryCollision.Circle newBall = new GeometryCollision.Circle(spawnPoint.position.x, spawnPoint.position.y, 0.5f, ballInstance);
        balls.Add(newBall);
        ballDataList.Add(new BallData(newBall, rb));

        rb.AddForce(Vector2.up * randomForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < ballDataList.Count; i++)
        {
            for (int j = i + 1; j < ballDataList.Count; j++)
            {
                
                if (GeometryCollision.CircleCircleCollision(ballDataList[i].circle, ballDataList[j].circle))
                {
                    
                    if (i == 0)
                    {
                        ballDataList[i].rb.velocity *= 1.2f;
                        Debug.Log("speed up!");
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        // Update each ball's position to match its corresponding GameObject's position
        for (int i = 0; i < ballDataList.Count; i++)
        {
            ballDataList[i].circle.center.x = ballDataList[i].circle.obj.transform.position.x;
            ballDataList[i].circle.center.y = ballDataList[i].circle.obj.transform.position.y;
        }
    }
}
