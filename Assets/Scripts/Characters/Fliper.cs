using UnityEngine;

public class Fliper : MonoBehaviour
{
    public bool IsTurnLeft { get; private set; } = true;

    public void LookAtTarget(Vector2 targetPosition)
    {
        IsTurnLeft = transform.localScale.x > 0;

        if ((transform.position.x < targetPosition.x && IsTurnLeft)
        || (transform.position.x > targetPosition.x && IsTurnLeft == false))
        {
            IsTurnLeft = !IsTurnLeft;
            transform.Flip();
        }
    }
}
