using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageScrollView : MonoBehaviour, IEndDragHandler,IBeginDragHandler
{
    #region ��Ա����
    private ScrollRect rect;
    private RectTransform content;
    private int pagesCount;
    public float[] pages;

    private bool isMoving = true;
    private float moveTime = 0.3f;
    private float timer = 0;
    private float startPosition;
    private int currentPage = 0;

    private bool isAtuoMoving=true;
    private float autoTimer = 0;
    private float autoScrollTime = 2f;
    private bool isDruging;
    #endregion
    // Start is called before the first frame update
    #region unity�ص�
    void Start()
    {
        Initi();
    }

    // Update is called once per frame
    void Update()
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
            pages[i] = (i * (1.0f / ((float)pagesCount - 1.0f)));
        }
    }
    //��������
    private void MoveListener() {
        if (isMoving)
        {
            timer += Time.deltaTime * (1 / moveTime);
            rect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, pages[currentPage], timer);
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
        startPosition = rect.horizontalNormalizedPosition;
    }
    //������������ҳ��
    private int CaculateMinPage() {
        int minPage = 0;
        for (int i = 0; i < pages.Length; i++)
        {
            minPage = (Mathf.Abs(pages[i] - rect.horizontalNormalizedPosition) < Mathf.Abs(pages[minPage] - rect.horizontalNormalizedPosition)) ? i : minPage;
        }
        return minPage;
    }
    #endregion
}
