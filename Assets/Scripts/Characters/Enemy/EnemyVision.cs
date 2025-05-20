using UnityEngine;

[RequireComponent(typeof(Fliper))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Vector2 _initAreaSize = new(3.5f, 2.5f);
    [SerializeField] private LayerMask _targetLayer;
    private Vector2 _seeAreaSize;
    private Vector2 _directionCoeff = Vector2.right;
    private Fliper _fliper;

    private void Awake()
    {
        _fliper = GetComponent<Fliper>();
        _seeAreaSize = _initAreaSize;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookOrigin(_directionCoeff), _seeAreaSize);
    }

    public bool TrySeeTarget(out Transform target, Vector2 visionDirection)
    {
        if (visionDirection.y > Mathf.Abs(visionDirection.x))
        {
            _seeAreaSize.x = _initAreaSize.y;
            _seeAreaSize.y = _initAreaSize.x;
            _directionCoeff = Vector2.up;
            // print("UP");
        }
        else if (visionDirection.y < -Mathf.Abs(visionDirection.x))
        {
            _seeAreaSize.x = _initAreaSize.y;
            _seeAreaSize.y = _initAreaSize.x;
            _directionCoeff = Vector2.down;
            // print("DOWN");
        }
        else
        {
            _directionCoeff = Vector2.right;
            _seeAreaSize = _initAreaSize;
            // print("RIGHT");
        }

        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookOrigin(_directionCoeff), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            Vector2 rayDirection = (hit.transform.position - transform.position).normalized;
            Vector2 rayPosition = transform.position;
            float rayDistance = _seeAreaSize.x;
            LayerMask rayMask = ~(1 << gameObject.layer);

            RaycastHit2D hit2D = Physics2D.Raycast(rayPosition, rayDirection, rayDistance, rayMask);
            if (hit2D.collider != null)
            {
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    target = hit2D.transform;
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.white);
                }
            }
        }
        return false;
    }

    private Vector2 GetLookOrigin(Vector2 directionCoeff)
    {
        float halfCoeff = 4f;
        int flipCoeff = _fliper?.IsTurnLeft ?? true ? -1 : 1;
        float originX = transform.position.x + _seeAreaSize.x / halfCoeff * flipCoeff * directionCoeff.x;
        float originY = transform.position.y + _seeAreaSize.y / halfCoeff * directionCoeff.y;
        return new Vector2(originX, originY);
    }
}
