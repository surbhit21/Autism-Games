﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSquareUIScrollBehavior : MonoBehaviour {

    float height;
    Vector2 hidePos;
    Vector2 showPos;

    // Use this for initialization
    void Start () {
        height = this.GetComponent<RectTransform>().sizeDelta.y;
        showPos = this.GetComponent<RectTransform>().anchoredPosition;
        hidePos = new Vector2(showPos.x, showPos.y + height);
    }

    public void Show(bool show)
    {
        this.GetComponent<RectTransform>().anchoredPosition = (show) ? showPos: hidePos;
    }

    public IEnumerator MoveToAnchoredPosition(Vector2 pos)
    {
        while(Vector2.Distance(transform.GetComponent<RectTransform>().anchoredPosition, pos) > 0.01f)
        {
            Vector2.MoveTowards(transform.GetComponent<RectTransform>().anchoredPosition, pos, Time.deltaTime);
            yield return null;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = pos;
    }

    public IEnumerator MoveDown()
    {
        var shiftHeight = transform.GetComponent<RectTransform>().sizeDelta.y;
        var anchPos = transform.GetComponent<RectTransform>().anchoredPosition;
        yield return StartCoroutine(MoveToAnchoredPosition(new Vector2(anchPos.x, anchPos.y - shiftHeight)));
    }

    //scrolls by shiftCount reward squares
    public IEnumerator ScrollTo(int rewardIndex, int n, int maxVisibleRewardSq)
    {
        var scrollStep = 1f / (n - maxVisibleRewardSq);
        var initScrollVal = GetComponent<ScrollRect>().horizontalNormalizedPosition;
        var scrollTarget = Mathf.Min(1f,scrollStep * rewardIndex);
        
        //Debug.Log("Scroll Step: " + scrollStep);
        Debug.Log("Scrolling to " + scrollTarget);

        var t = 0f;
        while (Mathf.Abs(GetComponent<ScrollRect>().horizontalNormalizedPosition - scrollTarget) > 0.02f)
        {
            GetComponent<ScrollRect>().horizontalNormalizedPosition = Mathf.Lerp(initScrollVal, scrollTarget, t);
            t += 0.1f;
            yield return null;
        }
        GetComponent<ScrollRect>().horizontalNormalizedPosition = scrollTarget;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
