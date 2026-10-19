using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int points = 10;
    private int health = 2;
    private float speed = 2.0f;
    private float xBound = 9.5f;
    private float stepDown = 0.5f;

    private static int direction = 1;
    private static bool edgeReached = false;
    private static int lastTurnFrame = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x += direction * speed * Time.deltaTime;
        gameObject.transform.position = pos;
        if (pos.x * direction > xBound)
        {
            edgeReached = true;
        }
    }

    void LateUpdate()
    {
        if (edgeReached)
        {
            edgeReached = false;
            direction = -direction;
            lastTurnFrame = Time.frameCount;
        }
        if (lastTurnFrame == Time.frameCount)
        {
            Vector3 pos = gameObject.transform.position;
            pos.y -= stepDown;
            gameObject.transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collider.gameObject);
            health--;
            if (health == 0)
            {
                FindFirstObjectByType<SpawnManager>().AddScore(points);
                Destroy(gameObject);
            }
        }
    }
}
