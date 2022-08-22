using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
	public string Name = "Wave";
	public Transform EnemyPrefab;
	public int EnemiesCount = 2;
	public float SpawnDelay = 1;

	public Wave(string name, Transform prefab, int count, float spawnDelay)
	{
		Name = name;
		EnemyPrefab = prefab;
		EnemiesCount = count;
		SpawnDelay = spawnDelay;
	}
}

public enum SpawnState
{
	SPAWNING,
	COUNTING,
	WAITING
}

public class WaveSpawner : MonoBehaviour
{
	public float timeBetweenWaves = 5f;
	public Wave[] m_Waves;
	public Transform[] spawnPoints;
	[SerializeField] private WaveGenerator m_WaveGenerator;

	private int curWave = 0;
	private float waveCountdown;
	private SpawnState state = SpawnState.COUNTING;

	private Transform m_TempObjects;

	public SpawnState State { get { return state; } }
	public float WaveCountDown { get { return waveCountdown; } }
	public int NextWave { get { return curWave + 1; } }

	private void Start()
	{
		SetDataFromWaveGenerator(m_WaveGenerator);
		waveCountdown = timeBetweenWaves;
		m_TempObjects = GameManager.instance.m_TempObjecsParent;
	}

	private void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemiesAreAlive())
			{
				WaveCompleted();
			}
			else return;
		}

		if (waveCountdown <= 0 && state != SpawnState.SPAWNING)
		{
			StartCoroutine(SpawnWave(m_Waves[curWave]));
		}
		else waveCountdown -= Time.deltaTime;
	}

	private IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.EnemiesCount; i++)
		{
			SpawnEnemy(_wave.EnemyPrefab);
			yield return new WaitForSeconds(1f / _wave.SpawnDelay);
		}

		state = SpawnState.WAITING;
	}

	private void WaveCompleted()
	{
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (curWave + 1 > m_Waves.Length - 1)
		{
			curWave = 0;
		} else curWave++;
	}

	private void SpawnEnemy(Transform _enemy)
	{
		int randomNum = Random.Range(0, spawnPoints.Length);
		Instantiate(_enemy, spawnPoints[randomNum].position, Quaternion.identity, m_TempObjects);
	}

	private bool EnemiesAreAlive()
	{
		if (GameManager.enemies.Count == 0)
			return false;
		else return true;
	}

	public void SetDataFromWaveGenerator(WaveGenerator generator)
	{
		m_Waves = new Wave[generator.WavesCount];
		for (int i = 0; i < generator.WavesCount; i++)
		{
			m_Waves[i] = new Wave(
				i.ToString(),
				generator.EnemyPrefab,
				i * generator.SpaceShipsMultiplier + 1,
				generator.SpawnDelayMultiplier - .1f
			);
		}
	}
}
