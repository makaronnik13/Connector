using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]

public class TranslationDictionary : ScriptableObject {

	public enum Languages
	{
		Eng,
		Rus,
		Ger,
		Fra,
		Chi,
		Jap
	}

	public List<Languages> languages = new List<Languages>();

	public List<KeyValuePair<string, List<string>>> translationPair = new List<KeyValuePair<string, List<string>>>();

	public string GetValue(string s)
	{
		return s;
	}

	public string GetValue(string s, Languages l)
	{
		return GetValue (s, (int)l);
	}

	private string GetValue(string value, int id)
	{
		try{
			KeyValuePair<string, List<string>> pair = translationPair.Find (p=>p.Key == value);
			return pair.Value [id];
		}
		catch
		{
			//fail
		}

		return value;
	}

}
