using UnityEngine;

public class EnterZoneExit : MonoBehaviour
{
    [SerializeField] private SpawnPlace _spawnPlace;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_spawnPlace.IsActive)
            {
                _spawnPlace.Deactivate();
            }
        }
    }
}
