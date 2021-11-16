using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageScrollView : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<ScrollRect>();
        if (rect == null) {
            throw new System.Exception("未找到ScrollRect");
        }
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        pagesCount = content.childCount;
        pages = new float[pagesCount];
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i] = (i * (1.0f / ((float)pagesCount - 1.0f)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveListener();
        AutoMoveListener();
    }
    //监听滚动
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
    //监听自动滚动
    private void AutoMoveListener()
    {
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
    //滚动到某一页
    private void ScrollToPage(int index) {
        isMoving = true;
        currentPage = index;
        timer = 0;
        startPosition = rect.horizontalNormalizedPosition;
    }
}
