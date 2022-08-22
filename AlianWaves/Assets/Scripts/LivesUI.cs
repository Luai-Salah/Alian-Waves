using UnityEngine;
using System.Collections.Generic;

public class LivesUI : MonoBehaviour
{
	public static LivesUI s_Instance;

    [SerializeField] private GameObject m_LivePrefab;

    private List<GameObject> m_Lives = new List<GameObject>();

	private void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		else if (s_Instance != this)
			Destroy(gameObject);
	}

	private void Start()
	{
		for (int i = 0; i < GameManager.RemainingLives; i++)
			m_Lives.Add(Instantiate(m_LivePrefab, transform));
	}

	public static void SubtractLives(int lives = 1) => s_Instance._SubtractLives(lives);
	private void _SubtractLives(int lives)
    {
		for (int i = 0; i < lives; i++)
		{
			if (m_Lives.Count <= 0)
				return;

			int index = m_Lives.Count - 1;
			Destroy(m_Lives[index]);
			m_Lives.RemoveAt(index);
		}
    }

	public static void AddLives(int lives = 1) => s_Instance._AddLives(lives);
	private void _AddLives(int lives)
    {
		for (int i = 0; i < lives; i++)
		{
			if (m_Lives.Count == 10)
				return;

			m_Lives.Add(Instantiate(m_LivePrefab, transform));
		}
	}
}