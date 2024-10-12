using Unity.Burst;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _scripts
{
    public class MathGen : MonoBehaviour
    {
        [BurstCompile]
        public static (int, int, string, int) GenerateNew()
        {
            int rand1 = Random.Range(0, 23);
            int outputSlot = Random.Range(1, 3);

            (int, int, string, int) result = (0, 0, null, 0); // result 1, result 2, op text, wrong answer

            int correctAnswer = Pow(rand1, 2);
            int wrongAnswer;

            // More complex wrong answer generation
            switch (Random.Range(1, 5))
            {
                case 1:
                    wrongAnswer = Pow(rand1 + Random.Range(1, 3), 2); // Close variation, different number squared
                    break;
                case 2:
                    wrongAnswer = Pow(rand1 - Random.Range(1, 3), 2); // Another close number squared
                    break;
                case 3:
                    wrongAnswer = Pow(rand1, 2) + Random.Range(1, 15); // Slight increase in the correct squared value
                    break;
                case 4:
                    wrongAnswer = Pow(rand1, 2) - Random.Range(1, 15); // Slight decrease in the correct squared value
                    break;
                default:
                    wrongAnswer = correctAnswer + Random.Range(5, 10); // Small random adjustment for safety
                    break;
            }

            // Ensure wrong answer is not exactly the correct one
            if (wrongAnswer == correctAnswer)
            {
                wrongAnswer += Random.Range(1, 3); // Adjust slightly if necessary
            }

            // Assign correct and wrong answers to slots
            switch (outputSlot)
            {
                case 1:
                    result.Item1 = correctAnswer;
                    result.Item2 = wrongAnswer;
                    break;
                case 2:
                    result.Item1 = wrongAnswer;
                    result.Item2 = correctAnswer;
                    break;
            }

            result.Item3 = $"{rand1} üzeri 2";
            result.Item4 = wrongAnswer;

            return result;
        }

        [BurstCompile]
        public static int Pow(int a, int b)
        {
            return (int)Mathf.Pow(a, b);
        }
    }
}
