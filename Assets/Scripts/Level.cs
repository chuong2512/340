using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
	private sealed class _CompleteLevelCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string _langStr___0;

		internal bool _flag___0;

		internal float _timer___0;

		internal float _duration___1;

		internal float _val___1;

		internal Level _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _CompleteLevelCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				Globals.menu.txtPoints.gameObject.SetActive(false);
				Globals.menu.sliderLevel.SetActive(false);
				Globals.menu.txtCurrentLevel.gameObject.SetActive(false);
				Globals.menu.txtNextLevel.gameObject.SetActive(false);
				Globals.menu.levelCompleteScreen.SetActive(true);
				this._langStr___0 = Globals.menu.txtCompleteLevel.GetComponent<Lang>().mainString;
				Globals.menu.txtCompleteLevel.text = this._langStr___0 + " " + Globals.currentLevel.ToString();
				Globals.menu.txtSharePoints.text = Globals.playerPoints.ToString();
				Globals.menu.Show();
				Globals.currentLevel++;
				Globals.SaveLevel();
				Globals.SavePoints();
				this._flag___0 = false;
				this._timer___0 = Time.realtimeSinceStartup;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (!this._flag___0)
			{
				this._duration___1 = 3f;
				this._val___1 = (Time.realtimeSinceStartup - this._timer___0) * (1f / this._duration___1);
				Globals.menu.sliderShare.value = this._val___1;
				if (this._val___1 >= 1f || this._this.skipWaiting)
				{
					this._flag___0 = true;
				}
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			Globals.menu.levelCompleteScreen.SetActive(false);
			Globals.menu.txtPoints.gameObject.SetActive(true);
			Globals.menu.txtCurrentLevel.gameObject.SetActive(true);
			Globals.menu.txtNextLevel.gameObject.SetActive(true);
			Globals.menu.sliderLevel.SetActive(true);
			this._this.skipWaiting = false;
			Globals.menu.txtCurrentLevel.text = Globals.currentLevel.ToString();
			Globals.menu.txtNextLevel.text = (Globals.currentLevel + 1).ToString();
			Globals.menu.txtPlayerBestValue.text = Globals.playerBest.ToString();
			Globals.menu.BestShow();
			this._this.Clear();
			this._this.levelIsComplete = false;
			this._this.ChangeColorScheme();
			this._this.Generate();
			Globals.player.Reset();
			Globals.levelControl.Reset();
			if (UnityEngine.Random.value > 0.3f)
			{
			}
			Globals.gameState = State.game;
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Row[] rows;

	public Row currentRow;

	public GameObject rowContainer;

	public GameObject bucket;

	public GameObject background;

	public bool levelIsComplete;

	public Text textMessage;

	public bool skipWaiting;

	private int rowCounter;

	private const int standard = 0;

	private const int empty = 1;

	private const int plat01 = 2;

	private const int plat10 = 3;

	private const int plat001 = 4;

	private const int plat010 = 5;

	private const int plat100 = 6;

	private const int plat101 = 7;

	private const int plat011 = 8;

	private const int plat110 = 9;

	private const int sliderSimple = 10;

	private const int sliderDark = 11;

	private const int sliderDarkPlatShort = 12;

	private const int sliderDarkSmall = 13;

	private const int sliderPlatWSliderDark = 14;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 position = Camera.main.transform.position;
	}

	public void Generate()
	{
		int num = (Globals.currentLevel - 1) / 10;
		if (num != 1)
		{
			if (num == 2)
			{
			}
		}
		int num2 = (int)Mathf.Lerp(22f, 64f, (float)Globals.currentLevel / 30f);
		this.rows = new Row[num2];
		this.rowCounter = 0;
		this.rows[0] = this.CreateStartPlatform();
		for (int i = 1; i < this.rows.Length; i++)
		{
			Row row = this.CreateRowContainer(i, new Vector3(0f, (float)i * -2.2f + 1.34f, 0f));
			int num3 = 4;
			row.elements = new RowElement[num3];
			for (int j = 0; j < num3; j++)
			{
				RowElement rowElement;
				if ((double)UnityEngine.Random.value > 0.5)
				{
					rowElement = this.CreatePlatform(0, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
					if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.6f, (float)Globals.currentLevel / 20f))
					{
						rowElement.darkBlock[0].SetActive(true);
						if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 20f))
						{
							rowElement.darkBlock[2].SetActive(true);
						}
					}
					else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f))
					{
						rowElement.darkBlock[1].SetActive(true);
					}
					else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f))
					{
						rowElement.darkBlock[2].SetActive(true);
					}
					else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 20f))
					{
						rowElement.darkBlock[UnityEngine.Random.Range(3, 5)].SetActive(true);
					}
				}
				else if ((double)UnityEngine.Random.value > 0.5)
				{
					if (UnityEngine.Random.value > 0.4f)
					{
						rowElement = this.CreatePlatform(2, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
						if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.7f, (float)Globals.currentLevel / 10f - 1f))
						{
							rowElement.darkBlock[UnityEngine.Random.Range(2, 4)].SetActive(true);
						}
						else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.7f, (float)Globals.currentLevel / 10f - 1f))
						{
							rowElement.darkBlock[UnityEngine.Random.Range(0, 2)].SetActive(true);
						}
					}
					else
					{
						rowElement = this.CreatePlatform(3, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
					}
				}
				else if (Globals.currentLevel > 10 && UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f - 1f))
				{
					rowElement = this.CreatePlatform(10, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
				}
				else if (Globals.currentLevel > 10 && UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f - 1f))
				{
					rowElement = this.CreatePlatform(13, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
				}
				else if (Globals.currentLevel > 20 && UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f - 2f))
				{
					rowElement = this.CreatePlatform(11, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
				}
				else if (Globals.currentLevel > 20 && UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f - 2f))
				{
					rowElement = this.CreatePlatform(12, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
				}
				else if (Globals.currentLevel > 20 && UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f - 2f))
				{
					rowElement = this.CreatePlatform(14, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
				}
				else
				{
					rowElement = this.CreatePlatform(0, row.transform, new Vector3((float)(-(float)num3 / 2 + j * 2), 0f, 0f));
					if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.6f, (float)Globals.currentLevel / 20f))
					{
						rowElement.darkBlock[0].SetActive(true);
						if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 20f))
						{
							rowElement.darkBlock[2].SetActive(true);
						}
					}
					else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f))
					{
						rowElement.darkBlock[1].SetActive(true);
					}
					else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 10f))
					{
						rowElement.darkBlock[2].SetActive(true);
					}
					else if (UnityEngine.Random.value > Mathf.Lerp(0.95f, 0.8f, (float)Globals.currentLevel / 20f))
					{
						rowElement.darkBlock[UnityEngine.Random.Range(3, 5)].SetActive(true);
					}
				}
				rowElement.SetColor(Color.Lerp(Globals.colors.plat1, Globals.colors.plat2, (float)i / (float)this.rows.Length));
				row.elements[j] = rowElement;
				row.elements[j].id = j;
			}
			this.CheckHoles(i, row.elements);
			this.rows[i] = row;
		}
		this.currentRow = this.rows[0];
		this.bucket.transform.position = new Vector3(0f, this.rows[this.rows.Length - 1].transform.position.y - 2.2f, -1f);
		this.bucket.transform.GetChild(0).GetComponent<Renderer>().material.color = Globals.colors.plat2;
		float num4 = Mathf.Abs(this.bucket.transform.position.y);
		this.background.transform.localPosition = new Vector3(0f, -num4 / 2f, 10f);
		this.background.transform.localScale = new Vector3(10f, num4 + 11f, 1f);
		Globals.sliderProgress.ResetProgress();
		TextureScaler.ResizeTextures();
		for (int k = 5; k < this.rows.Length; k++)
		{
			this.rows[k].gameObject.SetActive(false);
		}
		if (Globals.noAds)
		{
			Globals.menu.btnContinueNoAds.gameObject.SetActive(true);
		}
	}

	public void RestartRewarded()
	{
		Row row = this.CreateStartPlatform();
		row.id = this.currentRow.id - 1;
		row.gameObject.name = "Rewarded_Row";
		row.transform.position = new Vector3(0f, this.currentRow.transform.position.y + 2.2f, 0f);
		this.currentRow = row;
		this.rows[row.id] = row;
		this.rowCounter = row.id;
		row.elements[0].gameObject.GetComponent<TextureScaler>().Resize();
		row.elements[0].SetColor(Color.Lerp(Globals.colors.plat1, Globals.colors.plat2, (float)row.id / (float)this.rows.Length));
		row.id = 0;
	}

	private RowElement CreatePlatform(int type, Transform parent, Vector3 pos)
	{
		GameObject original = Globals.data.platform;
		switch (type)
		{
		case 0:
			original = Globals.data.platform;
			break;
		case 1:
			original = Globals.data.emptyBlock;
			break;
		case 2:
			original = Globals.data.platform2[0];
			break;
		case 3:
			original = Globals.data.platform2[1];
			break;
		case 4:
			original = Globals.data.platform3[0];
			break;
		case 5:
			original = Globals.data.platform3[1];
			break;
		case 6:
			original = Globals.data.platform3[2];
			break;
		case 7:
			original = Globals.data.platform3[3];
			break;
		case 8:
			original = Globals.data.platform3[4];
			break;
		case 9:
			original = Globals.data.platform3[5];
			break;
		case 10:
			original = Globals.data.sliders[0];
			break;
		case 11:
			original = Globals.data.sliders[1];
			break;
		case 12:
			original = Globals.data.sliders[2];
			break;
		case 13:
			original = Globals.data.sliders[3];
			break;
		case 14:
			original = Globals.data.sliders[4];
			break;
		default:
			original = Globals.data.platform;
			break;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, Vector3.zero, Quaternion.identity);
		gameObject.transform.parent = parent.transform;
		gameObject.transform.localPosition = pos;
		return gameObject.GetComponent<RowElement>();
	}

	private Row CreateStartPlatform()
	{
		Row row = this.CreateRowContainer(0, new Vector3(0f, 1.34f, 0f));
		row.elements = new RowElement[1];
		row.elements[0] = this.CreatePlatform(0, row.transform, Vector3.zero);
		row.elements[0].SetColor(Globals.colors.plat1);
		return row;
	}

	private void CheckHoles(int rowId, RowElement[] r)
	{
		bool flag = false;
		for (int i = 0; i < r.Length; i++)
		{
			if (r[i].isEmpty)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			int num = UnityEngine.Random.Range(0, r.Length - 1);
			Vector3 localPosition = r[num].transform.localPosition;
			Transform parent = r[num].transform.parent;
			UnityEngine.Object.Destroy(r[num].gameObject);
			if (UnityEngine.Random.value > Mathf.Lerp(0.9f, 0.5f, (float)Globals.currentLevel / 10f))
			{
				if (UnityEngine.Random.value > 0.5f)
				{
					r[num] = this.CreatePlatform(3, parent, localPosition);
				}
				else
				{
					r[num] = this.CreatePlatform(2, parent, localPosition);
				}
			}
			else
			{
				r[num] = this.CreatePlatform(1, parent, localPosition);
			}
			r[num].SetColor(Color.Lerp(Globals.colors.plat1, Globals.colors.plat2, (float)rowId / (float)this.rows.Length));
			r[num].isEmpty = true;
			r[num].width = 2f;
		}
	}

	public void CompleteLevel()
	{
		this.levelIsComplete = true;
		Globals.gameState = State.menu;
        AdsControl.Instance.showAds();
		base.StartCoroutine(this.CompleteLevelCoroutine());
	}

	private IEnumerator CompleteLevelCoroutine()
	{
		Level._CompleteLevelCoroutine_c__Iterator0 _CompleteLevelCoroutine_c__Iterator = new Level._CompleteLevelCoroutine_c__Iterator0();
		_CompleteLevelCoroutine_c__Iterator._this = this;
		return _CompleteLevelCoroutine_c__Iterator;
	}

	public void SkipWaiting()
	{
		this.skipWaiting = true;
	}

	public void GameOver()
	{
		this.levelIsComplete = true;
		Globals.menu.gameOverScreen.SetActive(true);
        if (!Globals.noAds)
        {
            if (AdsControl.Instance.GetRewardAvailable())
            {
                Globals.menu.btnRewarded.gameObject.SetActive(true);
            }
        }
            AdsControl.Instance.showAds();
        Globals.menu.Show();
		Globals.gameState = State.gameOver;
	}

	public void Clear()
	{
		IEnumerator enumerator = this.rowContainer.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.rowContainer.transform.localPosition = Vector3.zero;
	}

	public void NextRow()
	{
		this.rowCounter++;
		if (this.rowCounter < this.rows.Length)
		{
			this.currentRow = this.rows[this.rowCounter];
			if (4 + this.rowCounter < this.rows.Length)
			{
				this.rows[4 + this.rowCounter].gameObject.SetActive(true);
			}
		}
		else if (this.rowCounter == this.rows.Length)
		{
			this.currentRow = this.bucket.transform.GetChild(0).GetComponent<Row>();
		}
	}

	private Row CreateRowContainer(int id, Vector3 pos)
	{
		Row row = new GameObject
		{
			name = "Row_" + id.ToString(),
			transform = 
			{
				parent = Globals.level.rowContainer.transform,
				localPosition = pos
			}
		}.AddComponent<Row>();
		row.id = id;
		return row;
	}

	public void ChangeColorScheme()
	{
		int num = Mathf.CeilToInt(Mathf.Repeat((float)(Globals.currentLevel - 1), 32f));
		ColorScheme colorScheme = Globals.data.colorScheme[num];
		MonoBehaviour.print("Color Scheme: " + num.ToString());
		Globals.colors.plat1 = colorScheme.player[0];
		Globals.colors.plat2 = colorScheme.player[1];
		MeshRenderer component = this.background.GetComponent<MeshRenderer>();
		component.material.color = colorScheme.gradient[0];
		component.material.SetColor("_Color2", colorScheme.gradient[1]);
		Color color = colorScheme.gradient[0];
		if (color.r + color.g + color.b < 1.1f)
		{
			Color color2 = new Color(0.9f, 0.9f, 0.9f);
			Globals.menu.txtSliderProgress.color = color2;
			Globals.menu.txtPoints.color = color2;
			Globals.menu.txtBest.color = color2;
			Globals.menu.txtPlayerBestValue.color = color2;
			Globals.data.scoreEffect.GetComponent<Text>().color = color2;
			Globals.menu.txtSliderProgress.GetComponent<Shadow>().enabled = true;
			Globals.menu.txtPoints.GetComponent<Shadow>().enabled = true;
			Globals.menu.txtBest.GetComponent<Shadow>().enabled = true;
			Globals.menu.txtPlayerBestValue.GetComponent<Shadow>().enabled = true;
			Globals.data.scoreEffect.GetComponent<Shadow>().enabled = true;
		}
		else
		{
			Color color3 = new Color(0.2f, 0.2f, 0.2f);
			Globals.menu.txtSliderProgress.color = color3;
			Globals.menu.txtPoints.color = color3;
			Globals.menu.txtBest.color = color3;
			Globals.menu.txtPlayerBestValue.color = color3;
			Globals.data.scoreEffect.GetComponent<Text>().color = color3;
			Globals.menu.txtSliderProgress.GetComponent<Shadow>().enabled = false;
			Globals.menu.txtPoints.GetComponent<Shadow>().enabled = false;
			Globals.menu.txtBest.GetComponent<Shadow>().enabled = false;
			Globals.menu.txtPlayerBestValue.GetComponent<Shadow>().enabled = false;
			Globals.data.scoreEffect.GetComponent<Shadow>().enabled = false;
		}
	}
}
