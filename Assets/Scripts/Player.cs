using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public AnimationCurve jumpCurve;

	public SpriteRenderer trail;

	public SpriteRenderer air;

	public AudioClip ballSound;

	public AudioClip gameOverSound;

	public AudioClip bonusSound;

	public AudioClip speedCrash;

	public AudioClip finishSound;

	public int levelPoints;

	private Vector3 startPos;

	private float jumpTimer;

	private bool jumpFlag;

	private bool rayOnceFlag;

	private float jumpHeight;

	private float jumpSpeed;

	private float fallSpeed;

	private AudioSource aSrc;

	private int flownCounter;

	private SpriteRenderer playerSpr;

	private void Awake()
	{
		this.startPos = base.transform.position;
		this.rayOnceFlag = false;
		this.jumpSpeed = 1.5f;
		this.jumpHeight = 1.3f;
		this.fallSpeed = 0.0647f;
		this.aSrc = base.GetComponent<AudioSource>();
		this.flownCounter = 0;
		this.levelPoints = 0;
		this.playerSpr = base.GetComponent<SpriteRenderer>();
		string mainString = Globals.menu.txtPoints.GetComponent<Lang>().mainString;
		Globals.menu.txtPoints.text = mainString + " " + Globals.playerPoints.ToString();
	}

	private void Update()
	{
		int num = 256;
		num = ~num;
		if (!Globals.level.levelIsComplete && Globals.gameState == State.game)
		{
			this.UpdateJump();
			if (!this.jumpFlag)
			{
				this.fallSpeed += Time.deltaTime * 0.06f;
				this.fallSpeed = Mathf.Clamp(this.fallSpeed, 0f, 0.1f);
				base.transform.Translate(new Vector3(0f, -this.fallSpeed * this.jumpHeight * this.jumpSpeed, 0f));
				if (this.flownCounter > 2)
				{
					this.trail.gameObject.SetActive(true);
					float y = Mathf.Lerp(this.trail.transform.localScale.y, 10f, Time.deltaTime * 5f);
					this.trail.transform.localScale = new Vector3(1f, y, 1f);
					Color color = Color.Lerp(this.playerSpr.color, Color.white, Time.deltaTime * 10f);
					this.playerSpr.color = color;
					this.trail.color = color;
					this.air.gameObject.SetActive(true);
				}
			}
			float num2 = 0.19f;
			float num3 = num2;
			RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.position - new Vector3(0.1f, 0f, 0f), -Vector2.up, num3);
			RaycastHit2D[] array2 = Physics2D.RaycastAll(base.transform.position + new Vector3(0.1f, 0f, 0f), -Vector2.up, num3);
			RaycastHit2D raycastHit2D = default(RaycastHit2D);
			RaycastHit2D raycastHit2D2 = default(RaycastHit2D);
			RaycastHit2D[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				RaycastHit2D raycastHit2D3 = array3[i];
				raycastHit2D = raycastHit2D3;
				if (raycastHit2D3.collider.gameObject.layer == 9)
				{
					break;
				}
			}
			RaycastHit2D[] array4 = array2;
			for (int j = 0; j < array4.Length; j++)
			{
				RaycastHit2D raycastHit2D4 = array4[j];
				raycastHit2D2 = raycastHit2D4;
				if (raycastHit2D4.collider.gameObject.layer == 9)
				{
					break;
				}
			}
			if ((raycastHit2D || raycastHit2D2) && !this.rayOnceFlag)
			{
				if (!raycastHit2D)
				{
					raycastHit2D = raycastHit2D2;
				}
				else if (!raycastHit2D2)
				{
					raycastHit2D2 = raycastHit2D;
				}
				base.transform.position = new Vector3(base.transform.position.x, raycastHit2D.point.y + num3, base.transform.position.z);
				this.startPos = base.transform.position;
				bool flag = false;
				if (this.flownCounter > 2)
				{
					Globals.level.currentRow.HideElements();
					Globals.level.NextRow();
					this.trail.gameObject.SetActive(false);
					this.trail.transform.localScale = new Vector3(1f, 1f, 1f);
					this.playerSpr.color = new Color(1f, 0.39f, 0f);
					this.trail.color = new Color(1f, 0.39f, 0f);
					this.air.gameObject.SetActive(false);
					flag = true;
					this.aSrc.pitch = UnityEngine.Random.Range(0.97f, 1.03f);
					this.aSrc.clip = this.speedCrash;
					this.aSrc.Play();
				}
				if ((raycastHit2D.collider.gameObject.layer == 9 || raycastHit2D2.collider.gameObject.layer == 9) && !flag)
				{
					this.aSrc.pitch = 1f;
					this.aSrc.clip = this.gameOverSound;
					this.aSrc.Play();
					Globals.level.GameOver();
				}
				else
				{
					this.rayOnceFlag = true;
					this.jumpFlag = false;
					this.fallSpeed = 0.0647f;
					this.Jump();
					if (this.flownCounter < 3)
					{
						this.aSrc.pitch = UnityEngine.Random.Range(0.97f, 1.03f);
						this.aSrc.clip = this.ballSound;
						this.aSrc.Play();
					}
					this.flownCounter = 0;
					this.CreateSparks();
				}
			}
			else
			{
				UnityEngine.Debug.DrawRay(base.transform.position - new Vector3(0.1f, 0f, 0f), base.transform.TransformDirection(-Vector3.up) * num3, Color.green);
				UnityEngine.Debug.DrawRay(base.transform.position + new Vector3(0.1f, 0f, 0f), base.transform.TransformDirection(-Vector3.up) * num3, Color.green);
				this.rayOnceFlag = false;
				if (base.transform.position.y - 1.67f < Camera.main.transform.position.y && !this.jumpFlag)
				{
					float num4 = 4.3f;
					if (!Globals.noAds)
					{
						num4 = 3.7f;
					}
					if (Sys.MathDist(Camera.main.transform.position.y, Globals.level.bucket.transform.position.y) > num4)
					{
						Camera.main.transform.position = new Vector3(0f, base.transform.position.y - 1.67f, -10f);
					}
				}
				if (!this.jumpFlag && Globals.level.currentRow)
				{
					if (base.transform.position.y - num2 < Globals.level.currentRow.transform.position.y + 0.15f)
					{
						Globals.level.currentRow.HideElements();
						Globals.level.NextRow();
						this.aSrc.pitch = Mathf.Lerp(1f, 1.6f, (float)this.flownCounter / 10f);
						this.aSrc.clip = this.bonusSound;
						this.aSrc.Play();
						this.flownCounter++;
						this.levelPoints += this.flownCounter;
						string mainString = Globals.menu.txtPoints.GetComponent<Lang>().mainString;
						Globals.menu.txtPoints.text = mainString + " " + (Globals.playerPoints + this.levelPoints).ToString();
						this.CreateScoreEffect(this.flownCounter);
					}
					if (base.transform.position.y - num2 < Globals.level.bucket.transform.position.y)
					{
						this.trail.gameObject.SetActive(false);
						this.air.gameObject.SetActive(false);
						this.aSrc.pitch = 1f;
						this.aSrc.clip = this.finishSound;
						this.aSrc.Play();
						Globals.playerPoints += this.levelPoints;
						if (this.levelPoints > Globals.playerBest)
						{
							Globals.playerBest = this.levelPoints;
						}
						Globals.level.CompleteLevel();
					}
				}
			}
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.W))
		{
			Globals.level.CompleteLevel();
		}
	}

	private void CreateSparks()
	{
		for (int i = 0; i < 5; i++)
		{
			UnityEngine.Object.Instantiate<GameObject>(Globals.data.spark, base.transform.position - new Vector3(0f, base.transform.localScale.y / 2f, 0f), Quaternion.identity);
		}
	}

	private void Jump()
	{
		if (!this.jumpFlag)
		{
			this.jumpFlag = true;
			this.jumpTimer = Time.time;
		}
	}

	private void UpdateJump()
	{
		if (this.jumpFlag)
		{
			float num = (Time.time - this.jumpTimer) * this.jumpSpeed;
			float y = this.startPos.y + this.jumpCurve.Evaluate(num) * this.jumpHeight;
			base.transform.position = new Vector3(this.startPos.x, y, this.startPos.z);
			if (num >= 1f)
			{
				this.jumpFlag = false;
			}
		}
	}

	public void MoveToControlPoint()
	{
		base.transform.position = Vector3.zero;
	}

	public void Reset()
	{
		base.transform.position = new Vector3(0f, 1.67f, 0f);
		this.startPos = base.transform.position;
		this.rayOnceFlag = false;
		this.jumpSpeed = 1.5f;
		this.jumpHeight = 1.3f;
		this.fallSpeed = 0.0647f;
		this.flownCounter = 0;
		Camera.main.transform.position = new Vector3(0f, 0f, -10f);
		this.levelPoints = 0;
		string mainString = Globals.menu.txtPoints.GetComponent<Lang>().mainString;
		Globals.menu.txtPoints.text = mainString + " " + (Globals.playerPoints + this.levelPoints).ToString();
		this.trail.gameObject.SetActive(false);
		this.air.gameObject.SetActive(false);
		this.trail.transform.localScale = new Vector3(1f, 1f, 1f);
		this.playerSpr.color = new Color(1f, 0.39f, 0f);
		this.trail.color = new Color(1f, 0.39f, 0f);
	}

	public void ResetForRewarded()
	{
		Row currentRow = Globals.level.currentRow;
		base.transform.position = new Vector3(0f, currentRow.transform.position.y + 0.33f, 0f);
		this.startPos = base.transform.position;
		this.rayOnceFlag = false;
		this.jumpSpeed = 1.5f;
		this.jumpHeight = 1.3f;
		this.fallSpeed = 0.0647f;
		this.flownCounter = 0;
		Camera.main.transform.position = new Vector3(0f, base.transform.position.y - 1.67f, -10f);
		string mainString = Globals.menu.txtPoints.GetComponent<Lang>().mainString;
		Globals.menu.txtPoints.text = mainString + " " + (Globals.playerPoints + this.levelPoints).ToString();
		this.trail.gameObject.SetActive(false);
		this.air.gameObject.SetActive(false);
		this.trail.transform.localScale = new Vector3(1f, 1f, 1f);
		this.playerSpr.color = new Color(1f, 0.39f, 0f);
		this.trail.color = new Color(1f, 0.39f, 0f);
	}

	private void CreateScoreEffect2(int score)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Globals.data.scoreEffect, Vector3.zero, Quaternion.identity);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(Globals.menu.txtPoints.transform);
		component.anchoredPosition = new Vector3(0f, 0f, 0f);
		Text component2 = gameObject.GetComponent<Text>();
		component2.text = "+" + score.ToString() + "  ";
	}

	private void CreateScoreEffect(int score)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Globals.data.scoreEffect, Vector3.zero, Quaternion.identity);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(Globals.canvas.transform);
		component.anchoredPosition = new Vector3(0f, 450f, 0f);
		Text component2 = gameObject.GetComponent<Text>();
		component2.text = "+" + score.ToString() + "  ";
	}
}
