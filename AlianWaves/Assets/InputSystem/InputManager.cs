using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public static InputSystem input;
	public static Keyboard m_Keyboard;

	public static float Horizontal { get { return horizontal; } }
	public static Vector2 MousePosition { get { return cam.ScreenToWorldPoint(mousePos); } }
	public static bool Jump { get { return input.Player.Jump.triggered; } }
	public static bool Thrust { get { return input.Player.Fly.triggered; } }
	public static bool ShootTriggered { get { return input.Player.Shoot.triggered; } }
	public static bool ReloadTriggered { get { return input.Player.Reload.triggered; } }
	public static bool Shoot { get { return shoot == 0 ? false : true; } }

	private static float horizontal;
	private static Vector2 mousePos;
	private static float shoot;

	private static Camera cam;

    public static void Init()
	{
		input = new InputSystem();
		input.Enable();

		cam = GameManager.mainCam;
		m_Keyboard = UnityEngine.InputSystem.InputSystem.GetDevice<Keyboard>();

		input.Player.Horizontal.performed += _ctx => horizontal = _ctx.ReadValue<float>();
		input.Player.Horizontal.canceled += _ctx => horizontal = 0f;

		input.Player.MousePosition.performed += _ctx => mousePos = _ctx.ReadValue<Vector2>();
		input.Player.MousePosition.canceled += _ctx => mousePos = Vector2.zero;

		input.Player.Shoot.performed += _ctx => shoot = _ctx.ReadValue<float>();
		input.Player.Shoot.canceled += _ctx => shoot = _ctx.ReadValue<float>();
	}
}
