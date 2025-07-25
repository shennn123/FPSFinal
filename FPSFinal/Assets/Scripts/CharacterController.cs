using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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

    public float currentSpeed = 0;

    [Header("Gun Aiming")]
    public Transform gunHolder;

    private PlayerMoveState lastMoveState = PlayerMoveState.Idle; 

    public Gun[] guns; 
    public int currentGunIndex = 0;

    public bool gun1Unlocked = true;  
    public bool gun2Unlocked = false;
    public bool gun3Unlocked = false;
    public bool gun4Unlocked = false;
    public bool gun5Unlocked = false;

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gun1Unlocked) SwitchGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && gun2Unlocked) SwitchGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && gun3Unlocked) SwitchGun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4) && gun4Unlocked) SwitchGun(3);
        if (Input.GetKeyDown(KeyCode.Alpha5) && gun5Unlocked) SwitchGun(4);

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

        if (flatSpeed > 1.5f && flatSpeed < 6f)
        {
            moveState = PlayerMoveState.Walk;
        }
        else if (flatSpeed >= 6f)
        {
            moveState = PlayerMoveState.Run;
        }

        // ֻ��״̬��ı仯�ŵ���
        if (moveState != lastMoveState)
        {
            CrosshairController.instance?.SetSpreadState(moveState);
            lastMoveState = moveState;
        }
    }

    private void HandleMouseLook()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseInput.x); 

        verticalRotation -= mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); 

        camTrans.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        if (gunHolder != null)
        {
            gunHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); 
        }
    }
    private void HandleShooting()
    {
        // 自动射击武器
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0f)
            {
                if (activeGun.currentAmmo > 0)
                {
                    FireShot();
                }
                else if (!activeGun.isReloading)
                {
                    StartCoroutine(activeGun.ReloadGun());
                }
            }
        }

        // 单发武器
        if (Input.GetMouseButtonDown(0) && !activeGun.canAutoFire)
        {
            if (activeGun.currentAmmo > 0)
            {
                FireShot();
            }
            else if (!activeGun.isReloading)
            {
                StartCoroutine(activeGun.ReloadGun());
            }
        }
    }

    private void FireShot()
    {
        if (activeGun != null && !activeGun.isReloading)
        {
            if (activeGun.currentAmmo > 0)
            {
                activeGun.currentAmmo--;
                UIController.instance.UpdateAmmoUI();
                Instantiate(activeGun.bulletPrefab, FirePoint.position, FirePoint.rotation);
                activeGun.fireCounter = activeGun.firerate;

                CrosshairController.instance?.TriggerFireKick();
            }
            else
            {
                // 开枪时发现没子弹也触发自动换弹（保险）
                if (!activeGun.isReloading)
                {
                    StartCoroutine(activeGun.ReloadGun());
                }
            }
        }
    }


    public void HandleAnimation()
    {
        Vector3 flatMove = new Vector3(charCon.velocity.x, 0, charCon.velocity.z);
        currentSpeed = flatMove.magnitude;
        anim.SetFloat("MoveSpeed", flatMove.magnitude);
        GunAnim.SetFloat("MoveSpeed", flatMove.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {

    }


    public void SwitchGun(int gunIndex)
    {
        if (gunIndex < 0 || gunIndex >= guns.Length) return;

        if (gunIndex == currentGunIndex) return; 

        //SyncGunAnimatorState(GunAnim);


        if (activeGun != null)
        {
            activeGun.gameObject.SetActive(false);
        }


        activeGun = guns[gunIndex];
        currentGunIndex = gunIndex;
        activeGun.gameObject.SetActive(true);
        UIController.instance.UpdateAmmoUI();
        UIController.instance.UpdateWeaponUI();
 
    }
    
}