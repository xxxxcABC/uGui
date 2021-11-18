using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//��������ö����
public enum PageType {
    Horizontal,
    Vertical
}

public class PageScrollView : MonoBehaviour, IEndDragHandler,IBeginDragHandler
{
    #region ��Ա����
    protected ScrollRect rect;
    private RectTransform content;
    protected int pagesCount;
    protected float[] pages;

    private bool isMoving = true;
    private float moveTime = 0.3f;
    private float timer = 0;
    private float startPosition;
    private int currentPage = 0;

    private bool isAtuoMoving=true;
    private float autoTimer = 0;
    private float autoScrollTime = 2f;
    private bool isDruging;
    public PageType pageType = PageType.Horizontal;
    #endregion

    #region unity�ص�
    protected virtual void Start()
    {
        Initi();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveListener();
        AutoMoveListener();
    }
    //�����������ҳ��
    public void OnEndDrag(PointerEventData eventData)
    {
        ScrollToPage(CaculateMinPage());
        isDruging = false;
        autoTimer = 0;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDruging = true;
    }
    #endregion

    #region ����
    //��ʼ��
    private void Initi() {
        rect = GetComponent<ScrollRect>();
        if (rect == null)
        {
            throw new System.Exception("δ�ҵ�ScrollRect");
        }
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        pagesCount = content.childCount;
        if (pagesCount == 1)
        {
            throw new System.Exception("ֻ��һ��ҳ��");
        }
        pages = new float[pagesCount];
        for (int i = 0; i < pages.Length; i++)
        {
            switch (pageType)
            {
                case PageType.Horizontal:
                    pages[i] = (i * (1.0f / ((float)pagesCount - 1.0f)));
                    break;
                case PageType.Vertical:
                    pages[i] = 1-(i * (1.0f / ((float)pagesCount - 1.0f)));
                    break;
            }
            
        }
    }
    //��������
    private void MoveListener() {
        if (isMoving)
        {
            timer += Time.deltaTime * (1 / moveTime);
            switch (pageType)
            {
                case PageType.Horizontal:
                    rect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, pages[currentPage], timer);
                    break;
                case PageType.Vertical:
                    rect.verticalNormalizedPosition = Mathf.Lerp(startPosition, pages[currentPage], timer);
                    break;
            }
           
            if (timer >= 1)
            {
                isMoving = false;
            }
        }
    }
    //�����Զ�����
    private void AutoMoveListener()
    {
        if (isDruging) {
            return;
        }
        if (isAtuoMoving)
        {
            autoTimer += Time.deltaTime;
            if (autoTimer >= autoScrollTime)
            {   autoTimer = 0;
                currentPage++;
                currentPage %= pagesCount;
                ScrollToPage(currentPage);
            }
        }
    }
    //������ĳһҳ
    private void ScrollToPage(int index) {
        isMoving = true;
        currentPage = index;
        timer = 0;
        switch (pageType)
        {
            case PageType.Horizontal:
                startPosition = rect.horizontalNormalizedPosition;
                break;
            case PageType.Vertical:
                startPosition = rect.verticalNormalizedPosition;
                break;
        }
    }
    //������������ҳ��
    private int CaculateMinPage() {
        int minPage = 0;
        for (int i = 0; i < pages.Length; i++)
        {
            switch (pageType)
            {
                case PageType.Horizontal:
                    minPage = (Mathf.Abs(pages[i] - rect.horizontalNormalizedPosition) < Mathf.Abs(pages[minPage] - rect.horizontalNormalizedPosition)) ? i : minPage;
                    break;
                case PageType.Vertical:
                    minPage = (Mathf.Abs(pages[i] - rect.verticalNormalizedPosition) < Mathf.Abs(pages[minPage] - rect.verticalNormalizedPosition)) ? i : minPage;
                    break;
            }
        }
        return minPage;
    }
    #endregion
}
