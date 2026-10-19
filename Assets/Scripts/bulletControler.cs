using UnityEngine;

public class bulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Rigidbody bulletBody;
    public float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletBody.linearVelocity = new Vector3(0.0f, speed, 0.0f);
        if (bulletBody.position.y > 12 || bulletBody.position.y < -3)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnBullet(Vector3 spawnPosition, int direction)
    {
        if (direction == -1)
        {
            spawnPosition.y -= 1;
        }
        else if (direction == 1)
        {
            spawnPosition.y += 1;
        }
        print("Spawn Bullet");
        GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        newBullet.GetComponent<bulletControler>().speed = Mathf.Abs(speed) * direction;
        print(spawnPosition);
    }
    private void OnTriggerEnter(Collider collider)
    {
        bool hitBullet = collider.gameObject.CompareTag("PlayerBullet") || collider.gameObject.CompareTag("EnemyBullet");
        if (hitBullet && !collider.gameObject.CompareTag(gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
