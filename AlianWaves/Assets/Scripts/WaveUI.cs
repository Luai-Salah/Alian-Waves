using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_WaveCountdownText;
    [SerializeField] private TextMeshProUGUI m_WaveCountText;

    private WaveSpawner m_WaveSpawner;
    private Animator m_Animator;

	private SpawnState m_PrevState;

    private void Start()
    {
        m_WaveSpawner = GameManager.instance.GetComponent<WaveSpawner>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
		switch (m_WaveSpawner.State)
		{
			case SpawnState.COUNTING:
                UpdateCountingUI();
				break;
			case SpawnState.SPAWNING:
                UpdateSpawningUI();
				break;
			default:
				break;
		}

		m_PrevState = m_WaveSpawner.State;
	}

	private void UpdateCountingUI()
	{
		if (m_PrevState != SpawnState.COUNTING)
		{
			m_Animator.SetBool("WaveCountdown", true);
			m_Animator.SetBool("WaveIncoming", false);
		}

		int count = Mathf.FloorToInt(m_WaveSpawner.WaveCountDown + 1);
		if (count < 1) count = 1;
		m_WaveCountdownText.text = count.ToString();
	}

	private void UpdateSpawningUI()
	{
		if (m_PrevState != SpawnState.SPAWNING)
		{
			m_Animator.SetBool("WaveIncoming", true);
			m_Animator.SetBool("WaveCountdown", false);
			m_WaveCountText.text = m_WaveSpawner.NextWave.ToString();
		}
	}
}