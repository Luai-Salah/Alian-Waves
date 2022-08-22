using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private float yPosRestriction = -8f;
    [SerializeField] private Transform cam;

    private float shakeAmount;

    private CinemachineVirtualCamera followCam;
    private GameManager gManager;

	private void Awake()
	{
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
	}

	private void Start()
    {
        gManager = GameManager.instance;
        followCam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (followCam.Follow != null)
		{
            if (followCam.Follow.position.y < yPosRestriction)
                followCam.Follow = null;
        }
		else
		{
            if (GameManager.player != null)
                followCam.Follow = GameManager.player.transform;
		}
    }

    public static void Shake(float amt, float length)
	{
        instance.shakeAmount = amt;
        instance.InvokeRepeating("DoShake", 0f, 0.01f);
        instance.Invoke("StopShake", length);
	}

    private void DoShake()
	{
        Vector3 pos = cam.position;

        float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
        float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

        pos.x += offsetX;
        pos.y += offsetY;

        cam.position = pos;
	}

    private void StopShake()
    {
        CancelInvoke("DoShake");
        cam.localPosition = Vector3.zero;
    }
}
