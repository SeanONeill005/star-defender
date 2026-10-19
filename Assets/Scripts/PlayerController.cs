using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public GameObject bullet;
    public TextMeshProUGUI playerLives;

    private Rigidbody playerBody;
    private bulletControler bulletUser;
    private InputSystem_Actions controls;
    private float movementX;
    private float xBound = 10.0f;
    private int lives = 3;
    private bool isAlive = true;
    private float deathTimer = 0;

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
        controls.Player.Attack.performed += OnFire;
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Player.Attack.performed -= OnFire;
        controls.Player.Disable();
    }

    void OnDestroy()
    {
        controls.Dispose();
    }

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        bulletUser = bullet.GetComponent<bulletControler>();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementX = context.ReadValue<Vector2>().x;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (isAlive)
        {
            bulletUser.SpawnBullet(transform.position, 1);
        }
    }

    void Update()
    {
        if (isAlive)
        {
            playerBody.linearVelocity = new Vector3(movementX * speed, 0.0f, 0.0f);
            Vector3 position = playerBody.position;
            position.x = Mathf.Clamp(position.x, -xBound, xBound);
            playerBody.position = position;
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
        if (isAlive && collider.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collider.gameObject);
            isAlive = false;
        }
    }

    void respawnSequence()
    {
        gameObject.transform.position = Vector3.zero;
        updateLives();
    }

    void updateLives()
    {
        playerLives.text = "Lives: " + lives;
    }
}
