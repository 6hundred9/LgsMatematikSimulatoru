using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MathGen : MonoBehaviour
{
    public static (int, int, string, int) GenerateNew()
    {
        start:
        float rand = Random.Range(1, 5);
        int rand1 = Random.Range(1, 65);
        int rand2 = Random.Range(1, 65);
        int otherChoice = Random.Range(1, 129);
        int outputSlot = Random.Range(1, 3);
        
        Debug.Log(rand);
        switch (rand)
        {
            case 1:
                float output1 = Gcd(rand1, rand2);
                int blabla = Random.Range(1, 3);
                if (blabla == 1) otherChoice = Mathf.RoundToInt(output1 * 1.5f);
                else otherChoice = Mathf.RoundToInt(output1 / 2);
                if (outputSlot == 1) return ((int)output1, otherChoice, "EBOB(%num1%, %num2%)".Replace("%num1%", rand1.ToString()).Replace("%num2%", rand2.ToString()), otherChoice);
                return (otherChoice, (int)output1, "EBOB(%num1%, %num2%)".Replace("%num1%", rand1.ToString()).Replace("%num2%", rand2.ToString()), otherChoice);
            case 2:
                int output2 = Lcm(rand1, rand2);
                int blablabla = Random.Range(1, 3);
                if (blablabla == 1) otherChoice = Mathf.RoundToInt(output2 * 1.5f);
                else otherChoice = Mathf.RoundToInt(output2 / 2);
                if (outputSlot == 1) return (output2, otherChoice, "EKOK(%num1%, %num2%)".Replace("%num1%", rand1.ToString()).Replace("%num2%", rand2.ToString()), otherChoice);
                return (otherChoice, output2, "EKOK(%num1%, %num2%)".Replace("%num1%", rand1.ToString()).Replace("%num2%", rand2.ToString()), otherChoice);
            case 3:
                float sqrtResult = Sqrt(rand1);
                if (sqrtResult % 1 != 0) goto start; // Restart if sqrtResult is not an integer
                if (Mathf.Approximately(otherChoice, sqrtResult)) goto start;
                int output3 = (int)sqrtResult;
                if (outputSlot == 1) return (output3, otherChoice, "Karekök: %num1%".Replace("%num1%", rand1.ToString()), otherChoice);
                return (otherChoice, output3, "Karekök: %num1%".Replace("%num1%", rand1.ToString()), otherChoice);
            case 4:
                rand1 = Random.Range(1, 21);
                int output4 = PowerOfTwo(rand1);
                int blablablabla = Random.Range(1, 3);
                if (blablablabla == 1) otherChoice = PowerOfTwo(rand1 + 1);
                else otherChoice = (int)Mathf.Pow(rand1, 3);
                if (outputSlot == 1) return (output4, otherChoice, "%num1% üzeri 2".Replace("%num1%", rand1.ToString()), otherChoice);
                return (otherChoice, output4, "%num1% üzeri 2".Replace("%num1%", rand1.ToString()), otherChoice);
        }

        return (1, 1, "BOŞ", 1);
    }
    
    public static int Gcd(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int Lcm(int a, int b)
    {
        return Mathf.Abs(a * b) / Gcd(a, b);
    }

    public static float Sqrt(int a)
    {
        return Mathf.Sqrt(a);
    }

    public static int PowerOfTwo(int a)
    {
        return (int)Mathf.Pow(a, 2);
    }
}
