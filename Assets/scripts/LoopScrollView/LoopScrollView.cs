using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollView : MonoBehaviour
{
    #region ��Ա����
    private GridLayoutGroup contentGridLayout;
    private ContentSizeFitter contentSizeFitter;
    private RectTransform content;
    public GameObject itemPrefab;
    #endregion

    #region unity�ص�
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        contentGridLayout.enabled = true;
        contentSizeFitter.enabled = true;
        OnAddFirst();
        Invoke("EnableFalseGrid", 0.1f);
    }

    #endregion

    #region ����
    //��ʼ��
    private void Init()
    {
        content = GameObject.Find("Task/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        if (content == null)
        {
            throw new System.Exception("�Ҳ���content");
        }
        contentGridLayout = content.GetComponent<GridLayoutGroup>();
        if (contentGridLayout == null)
        {
            throw new System.Exception("�Ҳ���contentGridLayout");
        }
        contentSizeFitter = content.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter == null)
        {
            throw new System.Exception("�Ҳ���contentSizeFitter");
        }


    }
    //��ȡ�ӽڵ�
    private GameObject GetChildItem() {
        for (int i = 0; i < content.childCount; i++)
        {
            if (!content.GetChild(i).gameObject.activeSelf)
            {
                content.GetChild(i).gameObject.SetActive(true);
                return content.GetChild(i).gameObject;
            }
        }

        GameObject childItem = Instantiate(itemPrefab, content.transform);
        //��ʼ��childItem
        //��ʼ������
        childItem.transform.localScale = Vector3.one;
        childItem.transform.localPosition = Vector3.zero;
        //��ʼ��ê��
        childItem.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        childItem.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        //��ʼ�����
        childItem.GetComponent<RectTransform>().sizeDelta = contentGridLayout.cellSize;
        LoopItems loopItems = childItem.AddComponent<LoopItems>();
        loopItems.onAddFirst += OnAddFirst;
        loopItems.onAddLast += OnAddLast;
        loopItems.onRemoveFirst += OnRemoveFirst;
        loopItems.onRemoveLast += OnRemoveLast;
        return childItem;
    }

    //�Ƴ���ǰ���һ������
    public void OnRemoveLast()
    {
        Transform last = FindLast();
        if (last != null)
        {
            last.gameObject.SetActive(false);
        }
    }
    //�Ƴ���ǰ��һ������
    public void OnRemoveFirst()
    {
        Transform first = FindFirst();
        if (first != null)
        {
            first.gameObject.SetActive(false);
        }
    }
    //���β����
    public void OnAddLast()
    {
        Transform last = FindLast();
        GameObject obj = GetChildItem();
        obj.transform.SetAsLastSibling();
        if (last != null)
        {
            obj.transform.localPosition = last.localPosition - new Vector3(0, contentGridLayout.cellSize.y + contentGridLayout.spacing.y, 0);
        }

        if (IsNeedAddContentHeight(obj.transform)) {
            content.sizeDelta += new Vector2(0, contentGridLayout.cellSize.y + contentGridLayout.spacing.y);
        }
    }
    //���ͷ����
    public void OnAddFirst()
    {
        Transform first = FindFirst();
        GameObject obj = GetChildItem();
        obj.transform.SetAsFirstSibling();
        if (first != null) {
            obj.transform.localPosition = first.localPosition + new Vector3(0, contentGridLayout.cellSize.y + contentGridLayout.spacing.y, 0);
        }

    }

    private Transform FindFirst()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    private Transform FindLast()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            if (content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    private void EnableFalseGrid() {
        contentGridLayout.enabled = false;
        contentSizeFitter.enabled = false;
    }
    //�ж��Ƿ���Ҫ���Ӹ߶�
    private bool IsNeedAddContentHeight(Transform obj) {
        Vector3[] rectConrners = new Vector3[4];
        Vector3[] contentConrners = new Vector3[4];
        content.GetWorldCorners(contentConrners);
        obj.GetComponent<RectTransform>().GetWorldCorners(rectConrners);

        if (rectConrners[0].y < contentConrners[0].y)
        {
            return true;
        }

        return false;
    }
    #endregion
}
