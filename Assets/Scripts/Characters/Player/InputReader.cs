using UnityEngine;

public class InputReader : MonoBehaviour
{
    private float _directionX;
    private float _directionY;
    private bool _isInteract;
    private bool _isDash;
    private bool _isAttack;

    public Vector2 Direction { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _isDash = true;

        if (Input.GetKeyDown(KeyCode.F))
            _isInteract = true;

        // if (Input.GetAxis("Fire1") == 1)
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
        }
        _directionX = Input.GetAxis(Constants.InputData.HORIZONTAL_AXIS);
        _directionY = Input.GetAxis(Constants.InputData.VERTICAL_AXIS);

        Direction = new Vector2(_directionX, _directionY).normalized;
    }

    public bool GetIsDash() => GetBoolAsTrigger(ref _isDash);
    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);
    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInteract);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
