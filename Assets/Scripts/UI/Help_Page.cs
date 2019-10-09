using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help_Page : MonoBehaviour
{
	private void Update () {
		if(Input.touchCount > 0)
        {
            gameObject.SetActive(false);
        }
	}
}
