using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowAttackRotation : MonoBehaviour
{
	public Transform orb;
	public float radius;
	private Transform pivot;
	public bool isBow;
	public GameObject player;
	public void Start()
	{
			pivot = orb.transform;
			transform.parent = pivot;
			transform.position += Vector3.up * radius;
			player = GameObject.Find("Player");
	}

	public void FixedUpdate()
	{
		isBow = player.GetComponent<playerMovement>().isBow;

		if (isBow == true)
		{
		Vector3 orbVector = Camera.main.WorldToScreenPoint(orb.position);
		orbVector = Input.mousePosition - orbVector;
		float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

		pivot.position = orb.position;
		pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		}
	}
}