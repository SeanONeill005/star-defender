using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class playerMovement : MonoBehaviour
{
    public float speed = 4;
    private Rigidbody playerBody;
    private float movementX;
    private float xBound = 10.0f;
    public GameObject bullet;
    private bulletControler bulletUser;
    private int lives = 3;
    private bool isAlive = true;
    private float deathTimer = 0;
    public TextMeshProUGUI playerLives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        bulletUser = bullet.GetComponent<bulletControler>();

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            playerBody.linearVelocity = new Vector3(movementX * speed, 0.0f, 0.0f);
            Vector3 position = playerBody.position;
            position.x = Mathf.Clamp(position.x, -xBound, xBound);
            playerBody.position = position;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                bulletUser.SpawnBullet(transform.position, 1);
            }
        }
        else
        {
            deathTimer += Time.deltaTime;
            if (deathTimer > 2)
            {
                deathTimer = 0;
                if (lives > 0)
                {
                    lives--;
                    respawnSequence();
                    isAlive = true;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            isAlive = false;
            //gameObject.SetActive(false);
        }
    }
    void respawnSequence()
    {
        gameObject.transform.position = Vector3.zero;
        updateLives();
        //gameObject.SetActive(true);
    }

    void updateLives()
    {
        playerLives.text = "Lives: " + lives;
    }
}