using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 12f;
    public float gravityModifier = 2.5f;
    public float jumpPower = 8f;
    public int maxJumpCount = 2;

    private int jumpCount = 0;
    private Vector3 moveInput;
    private CharacterController charCon;

    [Header("Camera")]
    public Transform camTrans;
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;

    [Header("Gun")]
    public Gun activeGun;
    public Transform FirePoint;

    private Animator anim;
    public Animator GunAnim;

    [Header("Gun Aiming")]
    public Transform gunHolder; // 挂在摄像机下面或者角色上都行

    void Awake() => instance = this;

    void Start()
    {
        charCon = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleShooting();
        HandleAnimation();

        
    }

    private void HandleMovement()
    {
        float yStore = moveInput.y;

        Vector3 inputDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        inputDir.Normalize();

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        moveInput = inputDir * currentSpeed;
        moveInput.y = yStore;

        if (charCon.isGrounded)
        {
            jumpCount = 0;
            moveInput.y = -1f; // small downward force to stay grounded
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            moveInput.y = jumpPower;
            jumpCount++;
        }

        // Apply gravity
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        charCon.Move(moveInput * Time.deltaTime);

        float flatSpeed = new Vector3(charCon.velocity.x, 0f, charCon.velocity.z).magnitude;

        PlayerMoveState moveState = PlayerMoveState.Idle;

        if (flatSpeed > 1.5f && flatSpeed < 11f)
        {
            moveState = PlayerMoveState.Walk;
        }
        else if (flatSpeed > 11f)
        {
            moveState = PlayerMoveState.Run;
        }

        CrosshairController.instance?.SetSpreadState(moveState);
    }

    private void HandleMouseLook()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseInput.x); // 左右转身体

        verticalRotation -= mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); // 限制上下角度

        camTrans.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // 摄像机抬头低头

        if (gunHolder != null)
        {
            gunHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // 枪也跟着抬头低头
        }
    }
    private void HandleShooting()
    {
        if (activeGun == null || AmmoController.instance == null) return;

        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0f && AmmoController.instance.currentAmmo > 0)
            {
                FireShot();
            }
        }

        if (Input.GetMouseButtonDown(0) && !activeGun.canAutoFire)
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 500f))
            {
                FirePoint.LookAt(hit.point);
            }
            else
            {
                FirePoint.LookAt(camTrans.position + camTrans.forward * 30f);
            }

            if (AmmoController.instance.currentAmmo > 0)
            {
                FireShot();
            }
        }
    }

    private void FireShot()
    {
        if (!GunAnimatorController.instance.isReloading)
        {
            AmmoController.instance.currentAmmo--;
            Instantiate(activeGun.bulletPrefab, FirePoint.position, FirePoint.rotation);
            activeGun.fireCounter = activeGun.firerate;
            CrosshairController.instance?.TriggerFireKick();
        }
    }

    private void HandleAnimation()
    {
        Vector3 flatMove = new Vector3(charCon.velocity.x, 0, charCon.velocity.z);
        anim.SetFloat("MoveSpeed", flatMove.magnitude);
        GunAnim.SetFloat("MoveSpeed", flatMove.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IncreaseHealth"))
        {
            var health = PlayerHealthController.instance;

            health.currentHealth = Mathf.Min(health.currentHealth + 1, health.maxHealth);

            UIController.instance.healthSlider.value = health.currentHealth;
            UIController.instance.healthText.text = $"Health: {health.currentHealth}/{health.maxHealth}";

            Destroy(other.gameObject);
        }
    }
}
