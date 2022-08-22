using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private Status stats = new Status();
	[SerializeField] private float fallBoundry = -12f;
	[SerializeField] private StatusIndicator statusIndicator;

	private GameManager gManager;

	private void Awake()
	{
		gManager = GameManager.instance;
		GameManager.player = this;
	}

	private void Start()
	{
		stats.Init(statusIndicator);
		InvokeRepeating("Heal", 0.0f, 5f);
	}

	private void Update()
	{
		if (transform.position.y <= fallBoundry)
			Damage(99999);
	}

	public void Damage(int _damage)
	{
		stats.TakeDamage(_damage);
		if (stats.CurHealth <= 0)
			Die();
	}

	private void Die()
	{
		gManager.KillPlayer(this);
	}

	private void Heal()
	{
		stats.Heal(10);
	}
}
