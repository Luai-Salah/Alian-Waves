using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float smoothness = 10f;

	private bool jump;
	private bool thrust;
	private float hori;

	private PlayerMotor motor;

	private void Start()
	{
		motor = GetComponent<PlayerMotor>();
	}

	private void Update()
	{
		if (!jump)
			jump = InputManager.Jump;

		if (!thrust)
			thrust = InputManager.Thrust;
	}

	private void FixedUpdate()
    {
		hori = Mathf.Lerp(hori, InputManager.Horizontal, Time.fixedDeltaTime * smoothness);

		motor.Move(hori, jump, thrust);

		jump = false;
    }
}
