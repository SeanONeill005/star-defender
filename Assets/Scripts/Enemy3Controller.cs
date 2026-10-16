using System.Net;
using System.Threading;
using System.Timers;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
{
    
    Vector3 currentDestination;
    float speed = 0.05f;
    float moveTimer = 0.0f;
    float shootTimer = 0.0f;
    public GameObject bullet;
    private bulletControler bulletUser;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentDestination = GenerateDestination();
        bulletUser = bullet.GetComponent<bulletControler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = gameObject.transform.position;
        moveTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        if (moveTimer > 1)
        {
            currentDestination = GenerateDestination();
            moveTimer = 0;
        }
        
        if (shootTimer > 5)
        {
            bulletUser.SpawnBullet(transform.position, -1);
            shootTimer = 0;
        }

        if (currentDestination.x > position.x)
        {
                position.x += speed;
        }
        else
        {
            position.x -= speed;
        }
        if (currentDestination.y > position.y)
        {
            position.y += speed;
        }
        else
        {
            position.y -= speed;
        }
        gameObject.transform.position = position;


    }

    Vector3 GenerateDestination()
    {
        Vector3 position = new Vector3(Random.Range(-12, 12), Random.Range(1, 12), 0.0f);

        
        return position;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
