using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] private float offset = 90;

    private void Update()
    {
        Vector3 direction = (transform.position - (Vector3)InputManager.MousePosition);
        direction = Vector3.Normalize(direction);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, -angle - offset);
    }
}
