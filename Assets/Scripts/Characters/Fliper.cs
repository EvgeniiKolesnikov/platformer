using UnityEngine;

public class Fliper : MonoBehaviour
{
    public bool IsTurnLeft { get; private set; } = true;

    public void LookAtTarget(Vector2 targetPosition)
    {
        print("pos x = " + transform.position.x + "target x = " + targetPosition.x);
        if ((transform.position.x < targetPosition.x && IsTurnLeft)
        || (transform.position.x > targetPosition.x && IsTurnLeft == false))
        {
            print(transform.position.x > targetPosition.x);
            IsTurnLeft = !IsTurnLeft;
            transform.Flip();
        }
    }
}
