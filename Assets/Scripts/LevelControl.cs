using System;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
	public bool isDisabled;

	private bool holdFlag;

	private float oldPos;

	private float oldMPos;

	private float offset;

	private float deltaSpeed;

	private float oldSpeed;

	private void Start()
	{
	}

	private void Update()
	{
		float num = 2.5f;
#if !UNITY_EDITOR
        if (!Globals.level.levelIsComplete && Globals.gameState == State.game)
		{
			if (UnityEngine.Input.touchCount > 0)
			{
				if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began && !this.holdFlag)
				{
					this.holdFlag = true;
					this.oldMPos = this.MouseWorldPos().x;
					this.oldPos = Globals.level.rowContainer.transform.localPosition.x;
					this.oldSpeed = Globals.level.rowContainer.transform.localPosition.x;
				}
				else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					this.holdFlag = false;
				}
			}
			if (this.holdFlag)
			{
				if (!Menu.isHided && Mathf.Abs(this.MouseWorldPos().x - this.oldMPos) > 0.5f)
				{
					Globals.menu.Hide();
				}
				this.offset = this.oldPos + (this.MouseWorldPos().x - this.oldMPos) * num;
				Globals.level.rowContainer.transform.localPosition = new Vector3(this.offset, 0f, 0f);
				this.deltaSpeed = Globals.level.rowContainer.transform.localPosition.x - this.oldSpeed;
				this.oldSpeed = Globals.level.rowContainer.transform.localPosition.x;
			}
			else
			{
				this.deltaSpeed = Mathf.Lerp(this.deltaSpeed, 0f, Time.deltaTime * 7f);
				Globals.level.rowContainer.transform.localPosition += new Vector3(this.deltaSpeed, 0f, 0f);
			}
		}
#endif
#if UNITY_EDITOR


        if (!Globals.level.levelIsComplete && Globals.gameState == State.game)
          {

                if (Input.GetMouseButtonDown(0) && !this.holdFlag)
                {
                    this.holdFlag = true;
                    this.oldMPos = this.MouseWorldPos().x;
                    this.oldPos = Globals.level.rowContainer.transform.localPosition.x;
                    this.oldSpeed = Globals.level.rowContainer.transform.localPosition.x;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    this.holdFlag = false;
                }
            }
            if (this.holdFlag)
            {
                if (!Menu.isHided && Mathf.Abs(this.MouseWorldPos().x - this.oldMPos) > 0.5f)
                {
                    Globals.menu.Hide();
                }
                this.offset = this.oldPos + (this.MouseWorldPos().x - this.oldMPos) * num;
                Globals.level.rowContainer.transform.localPosition = new Vector3(this.offset, 0f, 0f);
                this.deltaSpeed = Globals.level.rowContainer.transform.localPosition.x - this.oldSpeed;
                this.oldSpeed = Globals.level.rowContainer.transform.localPosition.x;
            }
            else
            {
                this.deltaSpeed = Mathf.Lerp(this.deltaSpeed, 0f, Time.deltaTime * 7f);
                Globals.level.rowContainer.transform.localPosition += new Vector3(this.deltaSpeed, 0f, 0f);
            }
        

#endif

    }

    private Vector3 MouseWorldPos()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, Camera.main.nearClipPlane));
	}

	public void Reset()
	{
		Input.ResetInputAxes();
		this.holdFlag = false;
		this.deltaSpeed = 0f;
	}
}
