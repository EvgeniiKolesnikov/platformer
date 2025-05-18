using UnityEngine;

[RequireComponent(typeof(Fliper))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private LayerMask _targetLayer;

    private Fliper _fliper;

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookOrigin(), _seeAreaSize);
    }

    public bool TrySeeTarget(out Transform target)
    {
        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            Vector2 rayPosition = transform.position;
            float rayDistance = _seeAreaSize.x;
            LayerMask rayMask = ~(1 << gameObject.layer);

            RaycastHit2D hit2D = Physics2D.Raycast(rayPosition, direction, rayDistance, rayMask);
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

    private Vector2 GetLookOrigin()
    {
        float halfCoeff = 4f;
        int directionCoeff = _fliper?.IsTurnLeft ?? true ? -1 : 1;
        // int directionCoeff = 1;
        float originX = transform.position.x + _seeAreaSize.x / halfCoeff * directionCoeff;
        float originY = transform.position.y;
        return new Vector2(originX, originY);
    }



}
