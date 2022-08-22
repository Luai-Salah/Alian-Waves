using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon m_WeaponOne;
    [SerializeField] private Weapon m_WeaponTwo;

    [SerializeField] private GameObject m_WeaponOneImage;
    [SerializeField] private GameObject m_WeaponTwoImage;

    private Keyboard kb;

    private bool weaponOne = true;

    private void Start()
    {
        kb = InputManager.m_Keyboard;

        m_WeaponOne.gameObject.SetActive(true);
        m_WeaponOneImage.SetActive(true);

        m_WeaponTwo.gameObject.SetActive(false);
        m_WeaponTwoImage.SetActive(false);
    }
    private void Update()
    {
        if (kb.digit1Key.wasPressedThisFrame && !weaponOne)
		{
            AudioManager.PlaySound("ChangeWeapon");
            m_WeaponOne.gameObject.SetActive(true);
			m_WeaponOneImage.SetActive(true);

            m_WeaponTwo.gameObject.SetActive(false);
            m_WeaponTwoImage.SetActive(false);

            if (m_WeaponTwo.IsReloading)
			{
                m_WeaponOne.StopAllCoroutines();
                m_WeaponOne.IsReloading = false;
                m_WeaponOne.AmmoText.alpha = 1f;
            }
            weaponOne = true;
		}
        if (kb.digit2Key.wasPressedThisFrame && weaponOne)
		{
            AudioManager.PlaySound("ChangeWeapon");
            m_WeaponTwo.gameObject.SetActive(true);
            m_WeaponTwoImage.SetActive(true);

            m_WeaponOne.gameObject.SetActive(false);
            m_WeaponOneImage.SetActive(false);

            if (m_WeaponOne.IsReloading)
            {
                m_WeaponOne.StopAllCoroutines();
                m_WeaponOne.IsReloading = false;
                m_WeaponOne.AmmoText.alpha = 1f;
            }
            weaponOne = false;
        }
    }
}
