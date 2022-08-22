using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Path Finder")]
    [SerializeField] private Transform target;
    [SerializeField] private float followRange = 20f;
    [SerializeField] private float updateDelay = 0.5f;
    [SerializeField] private float nextWaypointDistence = 3f;

    [Header("Movement")]
    [SerializeField] private float speed = 300f;
    [SerializeField] private ForceMode2D forceMode;

    [HideInInspector] public bool pathHasEnded;

    private int curWaypoint = 0;

    private Rigidbody2D rb;
    private Seeker seeker;
    private Path path;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, updateDelay);
    }

	private void FixedUpdate()
	{
		if (!target)
		{
			if (GameManager.player)
				target = GameManager.player.transform;
			else return;
		}

		if (path == null)
            return;

		if (!IsTargetInRange())
			return;

		if (curWaypoint >= path.vectorPath.Count)
        {
            if (pathHasEnded)
                return;

            pathHasEnded = true;
            return;
        }
        else pathHasEnded = false;

        Vector2 direction = ((Vector2)path.vectorPath[curWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force, forceMode);

        float distance = Vector2.Distance(rb.position, path.vectorPath[curWaypoint]);
        if (distance < nextWaypointDistence)
		{
            curWaypoint++;
            return;
		}
    }

    private bool IsTargetInRange()
	{
        float distance = Vector2.Distance(rb.position, target.position);
        if (distance < followRange)
            return true;
        else return false;
	}

	private void OnPathComplete(Path p)
	{
		if (!p.error)
		{
            path = p;
            curWaypoint = 0;
		}
	}

    private void UpdatePath()
    {
        if (target)
		{
            if (IsTargetInRange())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
		}
    }
}
