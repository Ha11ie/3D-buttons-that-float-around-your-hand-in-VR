using UnityEngine;
using System.Collections;

public class FinanceCharter : MonoBehaviour
{
    public string Symbol;
    public FinancePeriod Period;
    public UnityEngine.UI.Image ChartImage;
    public bool AutoUpdate;
    const int FRAME_UPDATE_RATE = 500;
    int framesUntilNextUpdate = 0;
   
    public enum FinancePeriod
    {
        Last1Days,
        Last5Days,
        Last3Months,
        Last6Months,
        Last1Year,
        Last2Years,
        Last5Years,
        Maxiumum
    }
    string baseURL = "http://ichart.finance.yahoo.com/";
    public void Start()
    {
        StartCoroutine(RefreshChart());
    }
    public void RefreshChartStart()
    {
        StartCoroutine(RefreshChart());

    }
    private IEnumerator RefreshChart()
    {
        print("DOIN");
        //  Symbol = "GOOG";
        //   Period = FinancePeriod.Last1Year;
        string url = "";
        string perdiodText = "";
        switch (Period)
        {
            case FinancePeriod.Last1Days:
                {
                    perdiodText = "b?s=" + Symbol;
                    break;
                }
            case FinancePeriod.Last5Days:
                {
                    perdiodText = "w?s=" + Symbol;
                    break;
                }
            case FinancePeriod.Last3Months:
                {
                    perdiodText = "c/3m/" + Symbol + "?0";
                    break;
                }
            case FinancePeriod.Last6Months:
                {
                    perdiodText = "c/6m/" + Symbol + "?0";
                    break;
                }
            case FinancePeriod.Last1Year:
                {
                    perdiodText = "c/1y/" + Symbol + "?0";

                    break;
                }
            case FinancePeriod.Last2Years:
                {
                    perdiodText = "c/2y/" + Symbol + "?0";

                    break;
                }
            case FinancePeriod.Last5Years:
                {
                    perdiodText = "c/5y/" + Symbol + "?6";

                    break;
                }
        }
        url = baseURL + perdiodText;
        WWW www = new WWW(url);
        yield return www;

        if (www.texture != null)
        {
            ChartImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector3.zero);

        }
    }

    public void Update()
    {
        if (AutoUpdate)
        {
            if (framesUntilNextUpdate <= 0)
            {
                framesUntilNextUpdate = FRAME_UPDATE_RATE;
                StartCoroutine(RefreshChart());
            }
            framesUntilNextUpdate--;
        }
    }

}
