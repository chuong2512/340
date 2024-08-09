using System;
using UnityEngine;

public class Row : MonoBehaviour
{
	public int id;

	public RowElement[] elements;

	private bool onceFlag;

	private bool hideFlag;

	private void Start()
	{
		this.onceFlag = false;
	}

	private void Update()
	{
		float num = 4.1f;
		int num2 = this.elements.Length - 1;
		if (!this.hideFlag && this.id != 0 && (this.elements[0].transform.position.x < -num || this.elements[num2].transform.position.x > num))
		{
			bool flag = false;
			int num3 = 0;
			while (!flag)
			{
				flag = true;
				if (this.elements[0].transform.position.x < -num)
				{
					Vector3 position = new Vector3(this.elements[0].transform.position.x, this.elements[0].transform.position.y, this.elements[0].transform.position.z);
					position.x = this.elements[num2].transform.position.x + this.elements[num2].width / 2f + this.elements[0].width / 2f;
					this.elements[0].transform.position = position;
					RowElement[] array = new RowElement[this.elements.Length];
					this.elements.CopyTo(array, 0);
					for (int i = 0; i < this.elements.Length; i++)
					{
						this.elements[i] = array[Mathf.CeilToInt(Mathf.Repeat((float)(i + 1), (float)this.elements.Length))];
					}
					flag = false;
				}
				if (this.elements[num2].transform.position.x > num)
				{
					Vector3 position2 = new Vector3(this.elements[num2].transform.position.x, this.elements[num2].transform.position.y, this.elements[num2].transform.position.z);
					position2.x = this.elements[0].transform.position.x - this.elements[0].width / 2f - this.elements[num2].width / 2f;
					this.elements[this.elements.Length - 1].transform.position = position2;
					RowElement[] array2 = new RowElement[this.elements.Length];
					this.elements.CopyTo(array2, 0);
					for (int j = 0; j < this.elements.Length; j++)
					{
						this.elements[j] = array2[Mathf.CeilToInt(Mathf.Repeat((float)(j - 1), (float)this.elements.Length))];
					}
					flag = false;
				}
				num3++;
			}
		}
	}

	public void HideElements()
	{
		if (!this.onceFlag)
		{
			RowHider rowHider = base.gameObject.AddComponent<RowHider>();
			rowHider.rowElems = this.elements;
			this.hideFlag = true;
			this.onceFlag = true;
		}
	}
}
