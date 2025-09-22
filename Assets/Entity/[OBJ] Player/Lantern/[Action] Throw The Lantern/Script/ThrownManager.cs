using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrownManager : MonoBehaviour
{
	#region Singleton class: ThrownManager

	public static ThrownManager Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	#endregion

	Camera cam;

	public LanternThrown lantern;
	public TrajectoryLine trajectory;
	[SerializeField] float pushForce = 4f;

	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;
	[SerializeField] private float maxDistance;

	//---------------------------------------

	public bool canThrown; // For state machine control
	private LanternCore lanternCore;

	//---------------------------------------
	void Start()
	{
		cam = Camera.main;
		canThrown = false;

		lanternCore = GameObject.FindWithTag("Lantern").GetComponent<LanternCore>();
	}

	void Update()
	{
		if (!canThrown)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			OnDragStart();
		}
		if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			OnDragEnd();
		}

		if (isDragging)
		{
			OnDrag();
		}
	}

	//-Drag--------------------------------------
	void OnDragStart()
	{
		lantern.DisRB();
		
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		trajectory.Show();
	}

	void OnDrag()
	{
		endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		distance = Mathf.Clamp(Vector2.Distance(startPoint, endPoint), 0f, maxDistance);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		Debug.DrawLine(startPoint, endPoint);


		trajectory.UpdateDots(lantern.pos, force);
	}

	void OnDragEnd()
	{
		//push the lantern
		lanternCore.SwitchState(LanternState.UnAttach);
		lantern.ResumeRB();
		lantern.Push(force);

		trajectory.Hide();
		canThrown = false;
		StartCoroutine(lanternCore.PassableCounter());
	}

	public void CancleThrown()
	{
		lantern.ResumeRB();
		lanternCore.SwitchState(LanternState.Attach);
		trajectory.Hide();
		canThrown = false;
	}

}
