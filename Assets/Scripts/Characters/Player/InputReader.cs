using UnityEngine;

public class InputReader : MonoBehaviour
{
    private float _directionX;
    private float _directionY;
    private bool _isInteract;
    private bool _isDash;

    public Vector2 Direction { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _isDash = true;

        if (Input.GetKeyDown(KeyCode.F))
            _isInteract = true;

        _directionX = Input.GetAxis(Constants.InputData.HORIZONTAL_AXIS);
        _directionY = Input.GetAxis(Constants.InputData.VERTICAL_AXIS);

        Direction = new Vector2(_directionX, _directionY).normalized;
    }

    public bool GetIsDash() => GetBoolAsTrigger(ref _isDash);
    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInteract);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
