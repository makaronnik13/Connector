using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameModel/News")]
public class News : ScriptableObject 
{
	public List<NewsVariant> Variants = new List<NewsVariant>();
	public NewsVariant defaultVariant;
}
