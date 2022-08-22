using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField] private Transform healthBar;
    [SerializeField] private Gradient healthColor;

    private Image healthBarImage;

	private void Start()
	{
		healthBarImage = healthBar.GetComponent<Image>();
	}

	public void SetHealth(int _cur, int _max)
	{
        float _value = (float)_cur / _max;

        healthBar.localScale = new Vector3(_value, 1f, 1f);
		if (healthBarImage)
			healthBarImage.color = healthColor.Evaluate(_value);
		else
		{
			healthBarImage = healthBar.GetComponent<Image>();
			healthBarImage.color = healthColor.Evaluate(_value);
		}
	}
}
