using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Sys : MonoBehaviour
{
	private sealed class _ShareFacebook_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string _imageUrl___0;

		internal WWW _hs_get___1;

		internal string _image___2;

		internal string _FACEBOOK_URL___3;

		internal string appId;

		internal string url;

		internal string title;

		internal string caption;

		internal string description;

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

		public _ShareFacebook_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			bool flag = false;
			switch (num)
			{
			case 0u:
				this._imageUrl___0 = "http://anionsoftware.com/getimage.php";
				MonoBehaviour.print("share Facebook");
				this._hs_get___1 = new WWW(this._imageUrl___0 + "?game=paperfall");
				num = 4294967293u;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			try
			{
				switch (num)
				{
				case 1u:
					if (!string.IsNullOrEmpty(this._hs_get___1.error))
					{
						this._image___2 = "http://www.anionsoftware.com/img/socialscreenshots/paperfall/8.jpg";
					}
					else
					{
						this._image___2 = this._hs_get___1.text;
						if (this._image___2.Length == 0)
						{
							this._image___2 = "http://www.anionsoftware.com/img/socialscreenshots/paperfall/8.jpg";
						}
					}
					this._FACEBOOK_URL___3 = "http://www.facebook.com/dialog/feed";
					Application.OpenURL(string.Concat(new string[]
					{
						this._FACEBOOK_URL___3,
						"?app_id=",
						this.appId,
						"&link=",
						WWW.EscapeURL(this.url),
						"&name=",
						WWW.EscapeURL(this.title),
						"&caption=",
						WWW.EscapeURL(this.caption),
						"&description=",
						WWW.EscapeURL(this.description),
						"&picture=",
						WWW.EscapeURL(this._image___2),
						"&redirect_uri=",
						WWW.EscapeURL("http://www.facebook.com/")
					}));
					break;
				default:
					this._current = this._hs_get___1;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					flag = true;
					return true;
				}
			}
			finally
			{
				if (!flag)
				{
					this.____Finally0();
				}
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			uint num = (uint)this._PC;
			this._disposing = true;
			this._PC = -1;
			switch (num)
			{
			case 1u:
				try
				{
				}
				finally
				{
					this.____Finally0();
				}
				break;
			}
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		private void ____Finally0()
		{
			if (this._hs_get___1 != null)
			{
				((IDisposable)this._hs_get___1).Dispose();
			}
		}
	}

	private sealed class _ShareTwitter_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string _imageUrl___0;

		internal WWW _hs_get___1;

		internal string _image___2;

		internal string _twitUrl___3;

		internal string text;

		internal string url;

		internal string related;

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

		public _ShareTwitter_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			bool flag = false;
			switch (num)
			{
			case 0u:
				this._imageUrl___0 = "http://anionsoftware.com/getimage.php";
				this._hs_get___1 = new WWW(this._imageUrl___0 + "?game=paperfall");
				num = 4294967293u;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			try
			{
				switch (num)
				{
				case 1u:
					if (!string.IsNullOrEmpty(this._hs_get___1.error))
					{
						this._image___2 = "http://www.anionsoftware.com/img/socialscreenshots/paperfall/8.jpg";
					}
					else
					{
						this._image___2 = this._hs_get___1.text;
						if (this._image___2.Length != 0)
						{
							this._image___2 = "http://www.anionsoftware.com/img/socialscreenshots/paperfall/8.jpg";
						}
					}
					this._twitUrl___3 = "http://twitter.com/intent/tweet";
					Application.OpenURL(string.Concat(new string[]
					{
						this._twitUrl___3,
						"?text=",
						WWW.EscapeURL(this.text),
						"&url=",
						WWW.EscapeURL(this.url),
						"&related=",
						WWW.EscapeURL(this.related),
						"&lang=",
						WWW.EscapeURL("en")
					}));
					break;
				default:
					this._current = this._hs_get___1;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					flag = true;
					return true;
				}
			}
			finally
			{
				if (!flag)
				{
					this.____Finally0();
				}
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			uint num = (uint)this._PC;
			this._disposing = true;
			this._PC = -1;
			switch (num)
			{
			case 1u:
				try
				{
				}
				finally
				{
					this.____Finally0();
				}
				break;
			}
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		private void ____Finally0()
		{
			if (this._hs_get___1 != null)
			{
				((IDisposable)this._hs_get___1).Dispose();
			}
		}
	}

	private sealed class _ShareVk_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string _imageUrl___0;

		internal WWW _hs_get___1;

		internal string _image___2;

		internal string _vkUrl___3;

		internal string url;

		internal string description;

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

		public _ShareVk_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			bool flag = false;
			switch (num)
			{
			case 0u:
				this._imageUrl___0 = "http://anionsoftware.com/getimage.php";
				this._hs_get___1 = new WWW(this._imageUrl___0 + "?game=paperfall");
				num = 4294967293u;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			try
			{
				switch (num)
				{
				case 1u:
					if (!string.IsNullOrEmpty(this._hs_get___1.error))
					{
						this._image___2 = "http://www.anionsoftware.com/img/socialscreenshots/paperfall/8.jpg";
					}
					else
					{
						this._image___2 = this._hs_get___1.text;
						if (this._image___2.Length == 0)
						{
							this._image___2 = "http://www.anionsoftware.com/img/socialscreenshots/paperfall/8.jpg.png";
						}
					}
					this._vkUrl___3 = "http://vk.com/share.php";
					Application.OpenURL(string.Concat(new string[]
					{
						this._vkUrl___3,
						"?url=",
						WWW.EscapeURL(this.url),
						"&title=",
						WWW.EscapeURL(this.description),
						"&description=",
						WWW.EscapeURL(this.description),
						"&image=",
						WWW.EscapeURL(this._image___2),
						"&noparse=true"
					}));
					break;
				default:
					this._current = this._hs_get___1;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					flag = true;
					return true;
				}
			}
			finally
			{
				if (!flag)
				{
					this.____Finally0();
				}
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			uint num = (uint)this._PC;
			this._disposing = true;
			this._PC = -1;
			switch (num)
			{
			case 1u:
				try
				{
				}
				finally
				{
					this.____Finally0();
				}
				break;
			}
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		private void ____Finally0()
		{
			if (this._hs_get___1 != null)
			{
				((IDisposable)this._hs_get___1).Dispose();
			}
		}
	}

	public static float MathDist(float a, float b)
	{
		return Mathf.Abs(b - a);
	}

	public static float SmoothLerp(float a, float b, float t)
	{
		float f = Mathf.Lerp(-1.57079637f, 1.57079637f, Mathf.Clamp01(t));
		return Mathf.Lerp(a, b, (Mathf.Sin(f) + 1f) / 2f);
	}

	public static int BoolToInt(bool val)
	{
		int result = 0;
		if (val)
		{
			result = 1;
		}
		return result;
	}

	public static bool IntToBool(int val)
	{
		bool result = false;
		if ((float)val > 0.5f)
		{
			result = true;
		}
		return result;
	}

	public static Color RGB(float r, float g, float b)
	{
		return new Color(Mathf.Clamp(r, 0f, 255f) / 255f, Mathf.Clamp(g, 0f, 255f) / 255f, Mathf.Clamp(b, 0f, 255f) / 255f, 1f);
	}

	public static Color RGAB(float r, float g, float b, float a)
	{
		return new Color(Mathf.Clamp(r, 0f, 255f) / 255f, Mathf.Clamp(g, 0f, 255f) / 255f, Mathf.Clamp(b, 0f, 255f) / 255f, Mathf.Clamp(a, 0f, 255f) / 255f);
	}

	public static bool PointInTriangle(Vector2 point, Vector2 v1, Vector2 v2, Vector2 v3)
	{
		bool result = false;
		int num = (int)Mathf.Sign((v1.x - point.x) * (v2.y - v1.y) - (v2.x - v1.x) * (v1.y - point.y));
		int obj = (int)Mathf.Sign((v2.x - point.x) * (v3.y - v2.y) - (v3.x - v2.x) * (v2.y - point.y));
		int obj2 = (int)Mathf.Sign((v3.x - point.x) * (v1.y - v3.y) - (v1.x - v3.x) * (v3.y - point.y));
		if (num.Equals(obj) && num.Equals(obj2))
		{
			result = true;
		}
		return result;
	}

	public static string NormalizeBigNumber(string str)
	{
		string text = str;
		int num = 1;
		while ((float)num < Mathf.Ceil((float)str.Length / 3f))
		{
			text = text.Insert(str.Length - num * 3, " ");
			num++;
		}
		return text;
	}

	public static int RandomSign()
	{
		int result = 1;
		if ((double)UnityEngine.Random.value < 0.5)
		{
			result = -1;
		}
		return result;
	}

	public static float StepAngle(float angle, float step)
	{
		int num = (int)Mathf.Sign(angle);
		angle = Mathf.Abs(angle);
		float num2 = Mathf.Round(angle / step) * step;
		num2 -= 360f * Mathf.Floor(angle / 360f);
		return num2 * (float)num;
	}

	public static Vector2 AngleToVector2(float angle)
	{
		Vector2 result = new Vector2(Mathf.Sin(angle * 0.0174532924f), Mathf.Cos(angle * 0.0174532924f));
		return result;
	}

	public static string MD5(string strToEncrypt)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(strToEncrypt);
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, "0"[0]);
		}
		return text.PadLeft(32, "0"[0]);
	}

	public static IEnumerator ShareFacebook(string appId, string url, string title, string caption, string description)
	{
		Sys._ShareFacebook_c__Iterator0 _ShareFacebook_c__Iterator = new Sys._ShareFacebook_c__Iterator0();
		_ShareFacebook_c__Iterator.appId = appId;
		_ShareFacebook_c__Iterator.url = url;
		_ShareFacebook_c__Iterator.title = title;
		_ShareFacebook_c__Iterator.caption = caption;
		_ShareFacebook_c__Iterator.description = description;
		return _ShareFacebook_c__Iterator;
	}

	public static IEnumerator ShareTwitter(string text, string url, string related)
	{
		Sys._ShareTwitter_c__Iterator1 _ShareTwitter_c__Iterator = new Sys._ShareTwitter_c__Iterator1();
		_ShareTwitter_c__Iterator.text = text;
		_ShareTwitter_c__Iterator.url = url;
		_ShareTwitter_c__Iterator.related = related;
		return _ShareTwitter_c__Iterator;
	}

	public static IEnumerator ShareVk(string url, string title, string description)
	{
		Sys._ShareVk_c__Iterator2 _ShareVk_c__Iterator = new Sys._ShareVk_c__Iterator2();
		_ShareVk_c__Iterator.url = url;
		_ShareVk_c__Iterator.description = description;
		return _ShareVk_c__Iterator;
	}
}
