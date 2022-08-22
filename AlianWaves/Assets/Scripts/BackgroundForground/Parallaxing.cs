using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    [SerializeField] private Transform[] parallaxingTransforms;
    [SerializeField] private float smoothness = 1f;

    private float[] parallaxScales;

    private Transform cam;
    private Vector3 prevCamPos;

	private void Awake()
	{
        cam = GameManager.mainCam.transform;
	}

	private void Start()
    {
		prevCamPos = cam.position;

		parallaxScales = new float[parallaxingTransforms.Length];
		for (int i = 0; i < parallaxingTransforms.Length; i++)
			parallaxScales[i] = parallaxingTransforms[i].position.z * -1;
	}

    private void Update()
    {
		for (int i = 0; i < parallaxingTransforms.Length; i++)
		{
			Transform parallaxT = parallaxingTransforms[i];
			float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

			float parallaxTargetPosX = parallaxT.position.x + parallax;

			Vector3 parallaxTargetPos = new Vector3(parallaxTargetPosX, parallaxT.position.y, parallaxT.position.z);

			parallaxT.position = Vector3.Lerp(parallaxT.position, parallaxTargetPos, smoothness);
		}

		prevCamPos = cam.position;
	}
}
