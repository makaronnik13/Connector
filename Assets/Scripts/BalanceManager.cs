using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceManager : Singleton<BalanceManager> {

    public Balance balanceAsset;

	public float GetRate(int day, int call)
	{
		return balanceAsset.days [0].fillersRate.Evaluate (call);
	}
}
