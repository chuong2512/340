using System;
using UnityEngine;

public class SparkElement : MonoBehaviour
{
	public SpriteRenderer sprRen;

	private Vector3 dir;

	private Vector3 gravityVector;

	private float speed;

	private float lifetime;

	private float startTime;

	private void Start()
	{
		Vector3 vector = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0f, 1f), 0f);
		this.dir = vector.normalized;
		this.gravityVector = new Vector3(0f, 0.04f, 0f);
		this.speed = UnityEngine.Random.Range(0.01f, 0.05f);
		this.startTime = Time.time;
		this.lifetime = 1f;
		float num = UnityEngine.Random.Range(0.03f, 0.04f);
		base.transform.localScale = new Vector3(num, num, 1f);
	}

	private void Update()
	{
		this.gravityVector = new Vector3(0f, this.gravityVector.y - 0.2f * Time.deltaTime, 0f);
		base.transform.Translate(this.dir * this.speed + this.gravityVector, Space.World);
		Color color = this.sprRen.color;
		this.sprRen.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0f, Time.time - this.startTime));
		if (Time.time - this.startTime > this.lifetime)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
