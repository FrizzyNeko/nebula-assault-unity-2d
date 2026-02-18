using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    [SerializeField] float moveSpeed = 10f;

    [Header("")]
    [Tooltip("Sprite boyutuna ek olarak býrakmak istediðin ekstra mesafe")]
    [SerializeField] float extraPaddingLeft;
    [SerializeField] float extraPaddingRight;
    [SerializeField] float extraPaddingUp;
    [SerializeField] float extraPaddingDown;

    // Bileþenler ve Deðiþkenler
    InputAction moveAction;
    InputAction fireAction;
    SpriteRenderer spriteRenderer;
    Camera mainCamera;
    Shooter playerShooter;

    // Viewport köþeleri olacak
    Vector2 minBounds;
    Vector2 maxBounds;

    void Awake()
    {
        // SpriteRenderer (child dahil)
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"{gameObject.name}: SpriteRenderer bulunamadý!");
            enabled = false;
            return;
        }

        // Input Action
        moveAction = InputSystem.actions.FindAction("Move");
        if (moveAction == null)
        {
            Debug.LogError("Move InputAction bulunamadý!");
            enabled = false;
            return;
        }

        fireAction = InputSystem.actions.FindAction("Fire");
        if (fireAction == null)
        {
            Debug.LogError("Fire InputAction bulunamadý!");
            enabled = false;
            return;
        }

        // Shooter
        playerShooter = GetComponent<Shooter>();
        if (playerShooter == null)
        {
            Debug.LogError("Shooter bulunamadý!");
            enabled = false;
            return;
        }

        // Camera
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera bulunamadý!");
            enabled = false;
            return;
        }

    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        MovePlayer();
        FireShooter();
    }

    void InitBounds()
    {
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
    }

    void MovePlayer()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 deltaMove = new Vector3(input.x, input.y, 0f) * moveSpeed * Time.deltaTime;

        Vector3 newPos = transform.position + deltaMove;

        // Sprite'ýn gerçek dünya boyutlarý
        float halfWidth = spriteRenderer.bounds.extents.x;
        float halfHeight = spriteRenderer.bounds.extents.y;

        newPos.x = Mathf.Clamp(
            newPos.x,
            minBounds.x + halfWidth + extraPaddingLeft,
            maxBounds.x - halfWidth - extraPaddingRight
        );

        newPos.y = Mathf.Clamp(
            newPos.y,
            minBounds.y + halfHeight + extraPaddingDown,
            maxBounds.y - halfHeight - extraPaddingUp
        );

        transform.position = newPos;
    }

    void FireShooter()
    {
        playerShooter.isFiring = fireAction.IsPressed();
    }


}