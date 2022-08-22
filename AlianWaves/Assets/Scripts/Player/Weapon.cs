using TMPro;
using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Prefabs, Raycasts
}

public class Weapon : MonoBehaviour
{
    [Header("Default Weapon Settings")]
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private Transform muzzleFlashPrefab;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private string FireSoundName;
    [SerializeField] private TextMeshProUGUI ammoText;
    public Transform hitParticals;

    public int damage = 10;
    public float pushForce = 50f;

    [Header("Camera Shake")]
    [SerializeField] private float shakeAmount = 0.1f;
    [SerializeField] private float shakeLength = 0.2f;

    [Header("Shooting Bullets Type")]
    public WeaponType weaponType;
    [HideInInspector] public Transform bulletPrefab;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public float effectRate = 30f;

    private Transform m_TempObjects;
    private Transform firePoint;
    public TextMeshProUGUI AmmoText
    {
        get { return ammoText; }
        set { ammoText = value; }
    }
    private float timeToFire = 0f;
    private float timeToSpawnEffect;
    private int ammo;
    public bool IsReloading
    {
        get { return isReloading; }
        set { isReloading = value; }
    }
    private bool isReloading;

    private Rigidbody2D rb;

    private void Awake()
    {
        m_TempObjects = GameManager.instance.m_TempObjecsParent;
        ammoText.alpha = 1f;
        rb = GetComponentInParent<Rigidbody2D>();
        firePoint = transform.GetChild(0);
        ammo = maxAmmo;
        if (firePoint == null)
            Debug.LogError("Child Not Found! Make Sure That You Add The Fire Point As The First Child Of The Weapon", gameObject);
    }

	private void Update()
	{
        ammoText.text = $"{maxAmmo} / {ammo}";

        if (isReloading && ammo > 0 && InputManager.Shoot)
		{
            StopAllCoroutines();
            isReloading = false;
            ammoText.alpha = 1f;
		}

		if ((ammo <= 0 && !isReloading) || (InputManager.ReloadTriggered && ammo != maxAmmo))
            StartCoroutine( Reload() );

        if (!isReloading)
        {
            if (fireRate == 0)
            {
                if (InputManager.ShootTriggered)
                {
                    Shoot();
                }
            }
            else
            {
                if (InputManager.Shoot && Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
        }
    }

    private IEnumerator Reload()
	{
        ammoText.alpha = .5f;
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        ammoText.alpha = 1f;

        ammo = maxAmmo;
        isReloading = false;
	}

    private void Shoot()
	{
        ammo--;
        Vector2 mousePos = InputManager.MousePosition;
        Vector2 firePointPos = firePoint.position;
        Vector2 direction = (mousePos - firePointPos);

        if (weaponType == WeaponType.Raycasts)
		{
            RaycastHit2D _hit = Physics2D.Raycast(firePointPos, direction, 100f, whatToHit);

            if (_hit)
			{
                if (_hit.collider.CompareTag("Enemy"))
                {
                    Transform enemyT = _hit.collider.GetComponent<Transform>();
                    enemyT.GetComponent<Enemy>().Damage(damage);
                    enemyT.GetComponent<Rigidbody2D>().AddForce(-_hit.normal * pushForce);
                }
            }

            if (Time.time > timeToSpawnEffect)
			{
                StartCoroutine( RaycastEffect(_hit, direction) );
                timeToSpawnEffect = Time.time + 1f / effectRate;
			}
        }
		else if (weaponType == WeaponType.Prefabs)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, m_TempObjects).GetComponent<Bullet>().weapon = this;

            if (Time.time > timeToSpawnEffect)
            {
                PrefabsEffect(direction);
                timeToSpawnEffect = Time.time + 1f / effectRate;
            }
        }
	}

    private IEnumerator RaycastEffect(RaycastHit2D _hit, Vector2 direction)
	{
        DefualtEffects(direction);
        
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, firePoint.position);
        if (_hit)
		{
            lineRenderer.SetPosition(1, _hit.point);
            Instantiate(hitParticals, _hit.point, Quaternion.Euler(_hit.normal), m_TempObjects);
		}
        else lineRenderer.SetPosition(1, direction * 100f);

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }

    private void PrefabsEffect(Vector2 direction)
    {
        DefualtEffects(direction);
    }

    private void DefualtEffects(Vector2 direction)
	{
        AudioManager.PlaySound(FireSoundName);
        CameraController.Shake(shakeAmount, shakeLength);
        rb.AddForce(-direction.normalized * pushForce * Time.fixedDeltaTime);
        if (muzzleFlashPrefab)
		{
            Transform muzzle = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation, m_TempObjects);
            muzzle.parent = firePoint.parent;
            float size = Random.Range(0.6f, 0.9f);
            muzzle.localScale = new Vector3(size, size, muzzle.localScale.z);
            Destroy(muzzle.gameObject, 0.05f);
        }
    }
}
