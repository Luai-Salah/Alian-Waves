using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    [SerializeField] private int offset = 2;
    [SerializeField] private bool reversScale = false;

    private float spriteWidth;

    private bool hasRightBody = false;
    private bool hasLeftBody = false;

    private Camera cam;
    private Transform _transform;

	private void Awake()
	{
        cam = GameManager.mainCam;
        _transform = transform;
	}

	private void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (!hasLeftBody || !hasRightBody)
		{
            float camHoriExtend = cam.orthographicSize * Screen.width / Screen.height;

            float edgeVisiablePosRight = (_transform.position.x + spriteWidth / 2) - camHoriExtend;
            float edgeVisiablePosLeft = (_transform.position.x - spriteWidth / 2) + camHoriExtend;

            if (cam.transform.position.x >= edgeVisiablePosRight - offset && !hasRightBody)
			{
                MakeNewBody(1);
                hasRightBody = true;
			}
            else if (cam.transform.position.x <= edgeVisiablePosLeft + offset && !hasLeftBody)
			{
                MakeNewBody(-1);
                hasLeftBody = true;
			}
		}
    }

    private void MakeNewBody(int rightOrLeft)
	{
        Vector3 newPos = new Vector3(_transform.position.x + spriteWidth * rightOrLeft, _transform.position.y);
        Transform newBody = Instantiate(_transform, newPos, _transform.rotation, _transform.parent);

        if (reversScale)
		{
            Vector3 scale = newBody.localScale;
            newBody.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
		}

        if (rightOrLeft > 0)
            newBody.GetComponent<Tiling>().hasLeftBody = true;
        if (rightOrLeft < 0)
            newBody.GetComponent<Tiling>().hasRightBody = true;
		
	}
}
