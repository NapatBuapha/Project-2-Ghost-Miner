using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternThrown : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }
	[SerializeField] private float grav_Multiplier = 1;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();
	}

	public void Push(Vector2 force)
	{
		rb.velocity = Vector2.zero;
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void DisRB()
	{
		rb.velocity = Vector2.zero;
		rb.gravityScale = 0;
		rb.isKinematic = true;
	}

	public void ResumeRB()
	{
		rb.isKinematic = false;
		rb.gravityScale = grav_Multiplier;
	}

}
