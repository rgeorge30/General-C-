using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Pankaj's app for O(n) search for longest pattern comprising of 2 characters
    namespace ConsoleApplication1
    {
        class Program
        {
            static void Main(string[] args)
            {
                string pattern = "aedabbbcccffffffffefefsdfsdewew";


                FindLongestRepeatedPattern(pattern);

            }


            static void FindLongestRepeatedPattern(string pattern)
            {
                char[] patternArray = pattern.ToCharArray();
                PattenTracker currentSequence, potentialSequence;
                List<PattenTracker> patterns = new List<PattenTracker>();
                currentSequence = new PattenTracker(patternArray[0], null, 0, 1);
                potentialSequence = null;
                for (int i = 1; i < patternArray.Length; i++)
                {
                    if (currentSequence.FirstChar == patternArray[i])
                        currentSequence.Length = currentSequence.Length + 1;
                    else if (!currentSequence.SecondChar.HasValue)
                    {
                        currentSequence.SecondChar = patternArray[i];
                        currentSequence.Length = currentSequence.Length + 1;
                        potentialSequence = new PattenTracker(patternArray[i], null, i, 1);
                    }
                    else if (currentSequence.SecondChar == patternArray[i])
                    {
                        currentSequence.Length = currentSequence.Length + 1;
                        if (potentialSequence.FirstChar == patternArray[i])
                        {
                            potentialSequence.Length = potentialSequence.Length + 1;
                        }
                        else
                        {
                            potentialSequence = new PattenTracker(patternArray[i], null, i, 1);
                        }
                    }
                    else
                    {
                        patterns.Add(currentSequence);
                        if (potentialSequence != null)
                        {
                            currentSequence = potentialSequence;
                            currentSequence.SecondChar = patternArray[i];
                            currentSequence.Length = currentSequence.Length + 1;
                            potentialSequence = new PattenTracker(patternArray[i], null, i, 1);
                        }
                        else
                        {
                            currentSequence = new PattenTracker(patternArray[i], null, 0, 1);

                        }
                    }
                }


                patterns.Add(currentSequence);
            }
        }


        public class PattenTracker
        {
            public char FirstChar { get; set; }
            public char? SecondChar { get; set; }
            public int StartIndex { get; set; }
            public int Length { get; set; }


            public PattenTracker(char firstChar, char? secondChar, int startIndex, int length)
            {
                FirstChar = firstChar;
                SecondChar = secondChar;
                StartIndex = startIndex;
                Length = length;
            }


        }
    }

