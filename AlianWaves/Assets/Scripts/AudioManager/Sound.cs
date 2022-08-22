using UnityEngine;

[System.Serializable]
public class Sound
{
    public string m_Name;

    public AudioClip m_Clip;

    [Range(0f, 1f)]
    public float m_Volume = 0.7f;
    [Range(0f, .5f)]
    public float m_RandomVolume = 0.0f;

    [Range(0.1f, 3f)]
    public float m_Pitch = 1.0f;
    [Range(0f, .5f)]
    public float m_RandomPitch = 0.0f;

    public bool m_Loop = false;
    public bool m_PlayedConstantly = false;

    [HideInInspector] public GameObject m_GameObject;

    public AudioSource Source
    {
        get { return m_Source; }
        set
        {
            m_Source = value;
            m_Source.clip = m_Clip;
        }
    }
    private AudioSource m_Source;

    public void Play()
	{
        m_Source.volume = m_Volume * (1 + Random.Range(-m_RandomVolume / 2f, m_RandomVolume / 2f));
        m_Source.pitch = m_Pitch * (1 + Random.Range(-m_RandomPitch / 2f, m_RandomPitch / 2f));
		m_Source.loop = m_Loop;
        m_Source.Play();
	}
}
