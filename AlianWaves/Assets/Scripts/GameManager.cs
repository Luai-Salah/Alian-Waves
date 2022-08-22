using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public static Camera mainCam;
	public static List<Enemy> enemies = new List<Enemy>();

	public static Player player;
	private static int m_RemainingLives = 3;
	public static int RemainingLives { get { return m_RemainingLives; } }

	[Header("Player")]
	[SerializeField] private Transform playerPrefab;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Transform spawnPrefab;
	[SerializeField] private float spawnDelay = 1f;
	[SerializeField] private int m_StartingLives = 3;

	[Header("Enemy")]
	[SerializeField] private float cameraShakeAmount = 0.2f;
	[SerializeField] private float cameraShakeLength = 0.1f;

	[Header("Other")]
	public Transform m_TempObjecsParent;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(this);

		enemies = new List<Enemy>();

		mainCam = Camera.main;
		InputManager.Init();

		m_RemainingLives = m_StartingLives;
	}

	public IEnumerator RespawnPlayer()
	{
		yield return new WaitForSeconds(spawnDelay);
		Transform newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
		AudioManager.PlaySound("Spawn");
		if (spawnPrefab)
			Instantiate(spawnPrefab, spawnPoint.position, Quaternion.identity, m_TempObjecsParent);
		player = newPlayer.GetComponent<Player>();
	}

	public void KillPlayer(Player _player)
	{
		Destroy(_player.gameObject);
		AudioManager.PlaySound("Cry");

		m_RemainingLives--;
		LivesUI.SubtractLives();
		if (m_RemainingLives <= 0)
		{
			EndGame();
			return;
		}

		StopCoroutine(RespawnPlayer());
		StartCoroutine(RespawnPlayer());
	}

	public void KillEnemy(Enemy _enemy)
	{
		CameraController.Shake(cameraShakeAmount, cameraShakeLength);
		enemies.Remove(_enemy);
		Instantiate(_enemy.dieParticals, _enemy.transform.position, Quaternion.identity, m_TempObjecsParent);
		AudioManager.PlaySound("Explode");
		Destroy(_enemy.gameObject);
	}

	private void EndGame()
	{
		GameOverUI.GameOver();
		m_RemainingLives = m_StartingLives;
	}
}
