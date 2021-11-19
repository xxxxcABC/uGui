using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopItems : MonoBehaviour
{
    #region 成员变量
    private RectTransform rect;
    private RectTransform viewRect;

    private Vector3[] rectConrners;
    private Vector3[] viewConrners;
    #endregion
    #region 事件
    public Action onAddFirst;
    public Action onRemoveFirst;
    public Action onAddLast;
    public Action onRemoveLast;
    #endregion
    #region unity回调
    // Start is called before the first frame update
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        viewRect = GetComponentInParent<ScrollRect>().GetComponent<RectTransform>();

        rectConrners = new Vector3[4];
        viewConrners = new Vector3[4];
    }


    // Update is called once per frame
    void Update()
    {
        ConrnersListener();
    }
    #endregion

    #region 方法
    //监听边沿
    public void ConrnersListener() {
        rect.GetWorldCorners(rectConrners);
        viewRect.GetWorldCorners(viewConrners);
        if (IsFirst()) {
            if (rectConrners[0].y > viewConrners[1].y)
            {
                //物件最底部已移出ScrollView，隐藏该物件
                if (onRemoveFirst != null)
                {
                    onRemoveFirst();
                }
            }
            if (rectConrners[1].y < viewConrners[1].y)
            {
                //添加头节点
                if (onAddFirst != null)
                {
                    onAddFirst();
                }
            }
        }
        if (IsLast()) {
            if (rectConrners[1].y < viewConrners[0].y)
            {
                //物件最顶部已移出ScrollView，隐藏该物件
                if (onRemoveLast != null)
                {
                    onRemoveLast();
                }
            }
            if (rectConrners[0].y > viewConrners[0].y)
            {
                //添加尾节点
                if (onAddLast != null)
                {
                    onAddLast();
                }
            }
        }
        
    }

    public bool IsFirst() {
        for(int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                if (transform.parent.GetChild(i) == transform)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }
    public bool IsLast() {
        for (int i = transform.parent.childCount-1; i >=0; i--)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                if (transform.parent.GetChild(i) == transform)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }
    #endregion
}
