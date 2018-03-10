using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public static UIManager _instance;

	public Text UIscore;

	void Awake()
	{
		_instance = this;

	}


}
