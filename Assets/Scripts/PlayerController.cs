using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Ekstra Boþluk (Opsiyonel)")]
    [Tooltip("Sprite boyutuna ek olarak býrakmak istediðin ekstra mesafe")]
    [SerializeField] float extraPaddingLeft;
    [SerializeField] float extraPaddingRight;
    [SerializeField] float extraPaddingUp;
    [SerializeField] float extraPaddingDown;

    // Bileþenler ve Deðiþkenler
    InputAction moveAction;
    SpriteRenderer spriteRenderer; 

    // Viewport köþeleri
    Vector2 minBounds;
    Vector2 maxBounds;

    void Awake()
    {
        // GetComponentInChildren: Bu objede VEYA bunun altýndaki child objelerde
        // bulduðu ÝLK SpriteRenderer'ý getirir.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        // Güvenlik Kontrolü: Eðer child objelerde sprite renderer yoksa hata ver.
        if (spriteRenderer == null)
        {
            Debug.LogError($"{gameObject.name} objesi veya çocuklarýnda SpriteRenderer bulunamadý! " +
                           "Sýnýr hesaplamasý yapýlamýyor.");
            // Hata vermeye devam etmemesi için scripti durdur.
            enabled = false;
            return;
        }

        // Ekran sýnýrlarýný belirle
        InitBounds();
    }

    void Update()
    {
        // Eðer renderer yoksa hareket kodunu çalýþtýrma (Hata önlemi)
        if (spriteRenderer == null) return;

        MovePlayer();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        // Ekranýn sol alt (0,0) ve sað üst (1,1) köþelerini dünya koordinatýna çevir
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 deltaMove = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + deltaMove;

        // SpriteRenderer child objede olsa bile, 'bounds' özelliði 
        // onun DÜNYA üzerindeki kapladýðý alaný verir. 
        // Yani hesaplamamýz hala doðru çalýþýr.
        float halfWidth = spriteRenderer.bounds.extents.x;
        float halfHeight = spriteRenderer.bounds.extents.y;

        // Ekranýn kenar noktasý + sprite'ýn yarý geniþliði
        // Böylece sprite'ýn yarýsý ekrandan taþmaz
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
}