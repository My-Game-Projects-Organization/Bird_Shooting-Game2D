using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pres
{

    // Lưu dữ liệu vào máy để khi mở lại vẫn còn
    
    public static int bestScore
    {
          get => PlayerPrefs.GetInt(GameConst.BEST_SCORE,0);

          set
        {
            int curBestScore = PlayerPrefs.GetInt(GameConst.BEST_SCORE);

            if(value > curBestScore)
            {
                PlayerPrefs.SetInt(GameConst.BEST_SCORE,value);

            }
        }
    }
}
