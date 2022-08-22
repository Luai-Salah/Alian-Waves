using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Status stats = new Status();
	[SerializeField] private int damage = 10;
	[SerializeField] private StatusIndicator statusIndicator;
	public Transform dieParticals;

	private Rigidbody2D rb;

	private GameManager gManager;

	private void Awake()
	{
		gManager = GameManager.instance;
		GameManager.enemies.Add(this);
	}

	private void Start()
	{
		stats.Init(statusIndicator);
	}

	public void Damage(int _damage)
	{
		stats.TakeDamage(_damage);
		if (stats.CurHealth <= 0)
			Die();
	}

	private void Die()
	{
		gManager.KillEnemy(this);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Player player = collision.gameObject.GetComponent<Player>();
			player.Damage(damage); 
			
			Damage(99999);
		}
	}
}
