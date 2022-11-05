using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CSVReader : MonoBehaviour
{
    static string SPLIT = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string,object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();

        TextAsset data = Resources.Load(file) as TextAsset;                 //리소스 폴더에 있는 파일을 TextAsset 파일로 변환해서 가져옴

        var lines = Regex.Split(data.text, LINE_SPLIT);                     //파일의 텍스트를 한 줄씩 스트링 배열에 담아줌

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT);                          //스트링 배열의 제일 첫번째 요소에 들어있는 줄을 잘라서 Key로 사용

        for(int i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT);                      //배열의 요소에 들어있는 줄을 단어별로 잘라서 스트링 배열에 담음
            if (values.Length == 0 || values[0] == "") continue;

            var dic = new Dictionary<string, object>();

            for(int j = 0; j < header.Length && j < values.Length; j++)
            {
                var value = values[j];

                object temp = value;
                int n;
                float f;

                if(int.TryParse(value, out n))
                {
                    temp = n;
                }
                else if(float.TryParse(value, out f))
                {
                    temp = f;
                }
                dic[header[j]] = temp;                                      //헤더의 Key에 밸류 값을 대입
            }
            list.Add(dic);
        }
        return list;
    }
}