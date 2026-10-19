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
    private float invulnerableTimer = 0;
    private readonly float invulnerableTime = 2.0f;
    private readonly float blinkInterval = 0.25f;
    private float blinkTimer = 0;
    private Renderer playerRenderer;

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
        playerRenderer = GetComponent<Renderer>();
        UpdateLives();
        bulletUser = bullet.GetComponent<bulletControler>();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementX = context.ReadValue<Vector2>().x;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        bulletUser.SpawnBullet(transform.position, 1);
    }

    void Update()
    {
        playerBody.linearVelocity = new Vector3(movementX * speed, 0.0f, 0.0f);
        Vector3 position = playerBody.position;
        position.x = Mathf.Clamp(position.x, -xBound, xBound);
        playerBody.position = position;

        if (invulnerableTimer > 0)
        {
            invulnerableTimer -= Time.deltaTime;
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0;
                playerRenderer.enabled = !playerRenderer.enabled;
            }
            if (invulnerableTimer <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (invulnerableTimer <= 0 && collider.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collider.gameObject);
            DeathSequence();
        }
    }

    void DeathSequence()
    {
        lives--;
        UpdateLives();
        if (lives > 0)
        {
            RespawnSequence();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void RespawnSequence()
    {
        invulnerableTimer = invulnerableTime;
        blinkTimer = 0;
        playerRenderer.enabled = false;
    }

    void UpdateLives()
    {
        playerLives.text = "Lives: " + lives;
    }
}
