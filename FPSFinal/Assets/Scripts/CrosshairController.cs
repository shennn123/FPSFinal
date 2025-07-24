using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public static CrosshairController instance;

    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    
    public float defaultSpread = 10f;
    public float walkSpread = 20f;
    public float runSpread = 60f;
    public float fireKickSpread = 25f;
    public float spreadSmoothSpeed = 10f;

    private float currentSpread;
    private float targetSpread;
    private float fireKickTimer = 0f;

    void Awake()
    {
        instance = this;
    }

    public float baseGap = 50f;

    void Update()
    {
        if (fireKickTimer > 0)
        {
            fireKickTimer -= Time.deltaTime;
            targetSpread = Mathf.Max(targetSpread, fireKickSpread);
        }

        currentSpread = Mathf.Lerp(currentSpread, targetSpread, Time.deltaTime * spreadSmoothSpeed);

        float finalOffset = baseGap + currentSpread;

        top.anchoredPosition = new Vector2(0, finalOffset);
        bottom.anchoredPosition = new Vector2(0, -finalOffset);
        left.anchoredPosition = new Vector2(-finalOffset, 0);
        right.anchoredPosition = new Vector2(finalOffset, 0);
    }
    public void SetSpreadState(PlayerMoveState moveState)
    {
        switch (moveState)
        {
            case PlayerMoveState.Idle:
                targetSpread = defaultSpread;
                break;
            case PlayerMoveState.Walk:
                targetSpread = walkSpread;
                break;
            case PlayerMoveState.Run:
                targetSpread = runSpread;
                break;
        }
    }

    public void TriggerFireKick()
    {
        fireKickTimer = 0.1f;
    }
}

public enum PlayerMoveState
{
    Idle,
    Walk,
    Run
}