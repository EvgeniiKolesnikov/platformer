using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private float _directionX;
    private float _directionY;

    public bool SpaceKeyPressed { get; private set; }
    public Vector2 Direction { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceKeyPressed = true;
        }
        else
        {
            SpaceKeyPressed = false;
        }

        _directionX = Input.GetAxis(HORIZONTAL_AXIS);
        _directionY = Input.GetAxis(VERTICAL_AXIS);

        Direction = new Vector2(_directionX, _directionY).normalized;
    }
}
