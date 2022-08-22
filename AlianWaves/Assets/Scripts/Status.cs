[System.Serializable]
public class Status
{
	public int maxHealth = 50;

	public int CurHealth { get { return curHealth; } }
	private int curHealth;

	private StatusIndicator _statusIndicator;

	public void Init(StatusIndicator status)
	{
		curHealth = maxHealth;
		_statusIndicator = status;
		_statusIndicator.SetHealth(curHealth, maxHealth);
	}

	public void Init()
	{
		curHealth = maxHealth;
	}

	public void TakeDamage(int _damage)
	{
		curHealth -= _damage;
		if (_statusIndicator)
			_statusIndicator.SetHealth(curHealth, maxHealth);
	}

	public void Heal(int _amt)
	{
		curHealth += _amt;
		if (curHealth > 100)
			curHealth = 100;
		if (_statusIndicator)
			_statusIndicator.SetHealth(curHealth, maxHealth);
	}
}
