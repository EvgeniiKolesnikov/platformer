using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZoneEnter : MonoBehaviour
{
    [SerializeField] private Finish _finish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_finish.IsActive == false)
            {
                _finish.Activate();
            }
        }
    }
}
