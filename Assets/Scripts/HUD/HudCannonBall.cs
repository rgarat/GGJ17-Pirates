using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudCannonBall : MonoBehaviour
{
    public Image ball;
    public Image highlight;

	// Use this for initialization
	void Start () {
		
	}

    public void Show(bool doHighlight)
    {
        ball.enabled = true;
        if (doHighlight)
        {
            highlight.enabled = true;
            highlight.color = Color.white;
            LeanTween.alpha(highlight.rectTransform, 0, 0.6f);
        }
    }

    public void Hide()
    {
        ball.enabled = false;
        highlight.enabled = false;

    }


}
