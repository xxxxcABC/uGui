using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopItems : MonoBehaviour
{
    #region ��Ա����
    private RectTransform rect;
    private RectTransform viewRect;

    private Vector3[] rectConrners;
    private Vector3[] viewConrners;
    #endregion
    #region �¼�
    public Action onAddFirst;
    public Action onRemoveFirst;
    public Action onAddLast;
    public Action onRemoveLast;
    #endregion
    #region unity�ص�
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

    #region ����
    //��������
    public void ConrnersListener() {
        rect.GetWorldCorners(rectConrners);
        viewRect.GetWorldCorners(viewConrners);
        if (IsFirst()) {
            if (rectConrners[0].y > viewConrners[1].y)
            {
                //�����ײ����Ƴ�ScrollView�����ظ����
                if (onRemoveFirst != null)
                {
                    onRemoveFirst();
                }
            }
            if (rectConrners[1].y < viewConrners[1].y)
            {
                //���ͷ�ڵ�
                if (onAddFirst != null)
                {
                    onAddFirst();
                }
            }
        }
        if (IsLast()) {
            if (rectConrners[1].y < viewConrners[0].y)
            {
                //���������Ƴ�ScrollView�����ظ����
                if (onRemoveLast != null)
                {
                    onRemoveLast();
                }
            }
            if (rectConrners[0].y > viewConrners[0].y)
            {
                //���β�ڵ�
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
