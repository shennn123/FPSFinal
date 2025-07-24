using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    public AudioSource _audioSource;
    public CharacterController _controller;

    public AudioClip[] walkClips;
    public AudioClip[] runClips;
    public AudioClip jumpClip;
    public AudioClip landClip;

    public float walkInterval = 0.5f;
    public float runInterval = 0.35f;

    private float _stepTimer = 0f;
    private bool _wasGroundedLastFrame = true;

    private void Update()
    {
        if (_controller == null || _audioSource == null) return;

        bool isGrounded = _controller.isGrounded;

        // 跳跃和落地音效
        if (!isGrounded && _wasGroundedLastFrame)
        {
            PlayJumpSound();
        }
        else if (isGrounded && !_wasGroundedLastFrame)
        {
            PlayLandSound();
        }

        _wasGroundedLastFrame = isGrounded;

        // 获取当前水平速度（忽略 y）
        Vector3 horizontalVelocity = new Vector3(_controller.velocity.x, 0, _controller.velocity.z);
        float speed = horizontalVelocity.magnitude;

        if (isGrounded && speed > 0.1f)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float interval = isRunning ? runInterval : walkInterval;

            _stepTimer += Time.deltaTime;

            if (_stepTimer >= interval)
            {
                _stepTimer = 0f;
                PlayFootstepSound(isRunning);
            }
        }
        else
        {
            _stepTimer = 0f;
        }
    }

    private void PlayFootstepSound(bool isRunning)
    {
        AudioClip[] clips = isRunning ? runClips : walkClips;
        if (clips.Length == 0) return;

        int index = Random.Range(0, clips.Length);
        _audioSource.PlayOneShot(clips[index]);
    }

    private void PlayJumpSound()
    {
        if (jumpClip != null)
            _audioSource.PlayOneShot(jumpClip);
    }

    private void PlayLandSound()
    {
        if (landClip != null)
            _audioSource.PlayOneShot(landClip);
    }
}
