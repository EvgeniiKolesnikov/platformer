using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashTimer = 0f;
    [SerializeField] private float _dashDuration = 0.25f;
    [SerializeField] private float _dashReloadTime = 3f;

    public float DashSpeed { get; private set; } = 12f;
    public bool IsDashing { get; private set; }
    public Vector2 DashDirection { get; private set; }

    private void FixedUpdate()
    {
        _dashTimer += Time.fixedDeltaTime;

        if (_dashTimer >= _dashDuration)
        {
            IsDashing = false;
        }
    }

    public bool IsCanDash()
    {
        return _dashTimer >= _dashReloadTime;
    }

    public void StartDash()
    {
        _dashTimer = 0f;
        IsDashing = true;
    }

    public void SetDirection(Vector2 direction)
    {
        DashDirection = direction;
    }
}

