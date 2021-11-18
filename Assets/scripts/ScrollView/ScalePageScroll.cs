using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePageScroll : PageScrollView
{
    #region 成员变量
    private GameObject[] items;
    public float currentScale=1f;
    public float otherScale=0.6f;
    #endregion

    #region unity回调
    // Start is called before the first frame update
    protected override void Start()
    {   
        base.Start();
        items = new GameObject[pagesCount];
        for (int i = 0; i < pagesCount; i++)
        {
            items[i] = transform.Find("Viewport/Content").GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ScaleListner();
    }
    #endregion

    #region 方法
    private void ScaleListner() {
        int lastPage=0, nextPage=0;
        for(int i = 0; i < pagesCount; i++)
        {
            if (rect.horizontalNormalizedPosition >= pages[i]) {
                lastPage = i;
            }
        }
        for(int i = 0; i < pagesCount; i++)
        {
            if (rect.horizontalNormalizedPosition < pages[i]) {
                nextPage = i;
                break;//找到下一页立即退出循环
            }
        }
        if (nextPage == lastPage) {
            return;
        }

        float percent = (rect.horizontalNormalizedPosition - pages[lastPage]) / (pages[nextPage] - pages[lastPage]);
        items[lastPage].transform.localScale = Vector3.Lerp(Vector3.one * currentScale, Vector3.one * otherScale,percent);
        items[nextPage].transform.localScale = Vector3.Lerp(Vector3.one * otherScale, Vector3.one * currentScale, percent);
        for(int i = 0; i < pagesCount; i++)
        {
            if (i != nextPage && i != lastPage) {
                items[i].transform.localScale = Vector3.one * otherScale;
            }
        }

    }
    #endregion

}
