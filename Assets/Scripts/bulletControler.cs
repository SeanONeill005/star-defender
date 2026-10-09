using System.Net;
using Unity.VisualScripting;
using UnityEngine;


public class bulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Rigidbody bulletBody;
    private float movementZ;
    public float speed = 1;
    private float zBound = 12;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletBody.linearVelocity = new Vector3(movementZ * speed, 0.0f, 0.0f);
        Vector3 position = bulletBody.position;
        position.x = Mathf.Clamp(position.x, -zBound, zBound);
        bulletBody.position = position + new Vector3(0, 0, speed);
        if (bulletBody.position.z > 12)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnBullet(Vector3 spawnPosition)
    {
        print("Spawn Bullet");
        Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        print(spawnPosition);
    }
}
