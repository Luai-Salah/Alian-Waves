using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s_Instance;

    [SerializeField] private Sound[] m_Sounds;

    private void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            Destroy(gameObject);

        for (int i = 0; i < m_Sounds.Length; i++)
        {
            GameObject _go = new GameObject($"Sound_{i}_{m_Sounds[i].m_Name}");
            _go.transform.SetParent(transform);
            if (!m_Sounds[i].m_PlayedConstantly)
                m_Sounds[i].Source = _go.AddComponent<AudioSource>();
            m_Sounds[i].m_GameObject = _go;
        }
    }

    public static void PlaySound(string name) => s_Instance._PlaySound(name);
    public static void PlaySound(int index) => s_Instance._PlaySound(index);

    private void _PlaySound(string name)
	{
		foreach (Sound s in m_Sounds)
		{
            if (!s.m_PlayedConstantly)
			{
                if (s.m_Name == name)
                {
                    s.Play();
                    return;
                }
			}
			else
			{
                if (s.m_Name == name)
				{
                    s.Source = s.m_GameObject.AddComponent<AudioSource>();

                    s.Play();
                    Destroy(s.Source, s.m_Clip.length);
                    return;
                }
            }
		}

        Debug.LogError($"AudioManager: Sound '{name}' not found!", this);
	}

    private void _PlaySound(int index)
	{
        Sound s = m_Sounds[index];
        if (!s.m_PlayedConstantly)
        {
            if (s.m_Name == name)
            {
                s.Play();
                return;
            }
        }
        else
        {
            if (s.m_Name == name)
            {
                s.Source = s.m_GameObject.AddComponent<AudioSource>();

                s.Play();
                Destroy(s.Source, s.m_Clip.length);
                return;
            }
        }
    }
}
