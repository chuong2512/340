using System;
using UnityEngine;
using UnityEngine.UI;

public class Lang : MonoBehaviour
{
	public string mainString = string.Empty;

	public string eng = string.Empty;

	public string rus = string.Empty;

	private Text txt;

	private void Awake()
	{
		this.txt = base.GetComponent<Text>();
		if (this.txt)
		{
			if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian || Application.systemLanguage == SystemLanguage.Ukrainian)
			{
				this.rus = this.rus.Replace("\\n", "\n");
				this.mainString = this.rus;
			}
			else
			{
				this.eng = this.eng.Replace("\\n", "\n");
				this.mainString = this.eng;
			}
			this.txt.text = this.mainString;
		}
	}
}
