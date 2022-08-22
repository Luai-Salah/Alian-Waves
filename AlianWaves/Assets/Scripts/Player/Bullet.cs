using UnityEngine;

public class Bullet : MonoBehaviour
{
	[HideInInspector] public Weapon weapon;

	[SerializeField] private float speed = 100f;
	[SerializeField] private float destroyTime = 1f;

	private Rigidbody2D rb;
	private Transform m_TempObjects;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
		Destroy(gameObject, destroyTime);
		m_TempObjects = GameManager.instance.m_TempObjecsParent;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		Instantiate(weapon.hitParticals, transform.position, transform.rotation, m_TempObjects);

		if (collider.CompareTag("Enemy"))
		{
			Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
			rb.AddForce(transform.right * weapon.pushForce * Time.deltaTime);
			Enemy enemy = collider.GetComponent<Enemy>();
			enemy.Damage(weapon.damage);
		}

		Destroy(gameObject);
	}
}
