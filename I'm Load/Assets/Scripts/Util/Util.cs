using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static string CountToString(long count, int length)
    {
        string str = count.ToString();

        if (str.Length > length)
        {
            int deltaLength = str.Length - length;

            if (deltaLength > 13)
            {
                double value = count / 1000000000000000000.0;
                double uscala = 1;
                double dscala = 1;
                string format = "#,##0.";
                for (int i = 0; i < 17 - deltaLength; i++)
                {
                    uscala *= 10;
                    dscala *= 0.1;
                    format += "#";
                }
                value = System.Math.Floor(value * uscala) * dscala;
                str = value.ToString(format) + "e";
            }
            else if (deltaLength > 10)
            {
                double value = count / 1000000000000000.0;
                double uscala = 1;
                double dscala = 1;
                string format = "#,##0.";
                for (int i = 0; i < 14 - deltaLength; i++)
                {
                    uscala *= 10;
                    dscala *= 0.1;
                    format += "#";
                }
                value = System.Math.Floor(value * uscala) * dscala;
                str = value.ToString(format) + "p";
            }
            else if (deltaLength > 7)
            {
                double value = count / 1000000000000.0;
                double uscala = 1;
                double dscala = 1;
                string format = "#,##0.";
                for (int i = 0; i < 11 - deltaLength; i++)
                {
                    uscala *= 10;
                    dscala *= 0.1;
                    format += "#";
                }
                value = System.Math.Floor(value * uscala) * dscala;
                str = value.ToString(format) + "t";
            }
            else if (deltaLength > 4)
            {
                double value = count / 1000000000.0;
                double uscala = 1;
                double dscala = 1;
                string format = "#,##0.";
                for (int i = 0; i < 8 - deltaLength; i++)
                {
                    uscala *= 10;
                    dscala *= 0.1;
                    format += "#";
                }
                value = System.Math.Floor(value * uscala) * dscala;
                str = value.ToString(format) + "g";
            }
            else if (deltaLength > 1)
            {
                float value = count / 1000000f;
                float uscala = 1;
                float dscala = 1;
                string format = "#,##0.";
                for (int i = 0; i < 5 - deltaLength ; i++)
                {
                    uscala *= 10f;
                    dscala *= 0.1f;
                    format += "#";
                }
                value = Mathf.Floor(value * uscala) * dscala;
                str = value.ToString(format) + "m";
            } 
            else
            {
                float value = count / 1000.0f;
                value = Mathf.Floor(value * 10f) * 0.1f;
                str = value.ToString("#,##0.#") + "k";
            }
        }
        else
        {
            str = count.ToString("#,##0");
        }

        return str;
    }

    public static uint Distance(Vector2Int v1, Vector2Int v2)
    {
        return (uint)(Mathf.Abs(v2.x - v1.x) + Mathf.Abs(v2.y - v1.y));
    }
}
