using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private static Animator s_Animator;

	private void Start() => s_Animator = GetComponent<Animator>();

	public static void GameOver() => s_Animator.SetTrigger("GameOver");
	public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	public void Quit() => Application.Quit();
}