using System;
using System.Collections.Generic;
using System.Text;
using Task2.Core.Model;
using Task2.Core.Services;

namespace Task2.Service.Services
{
    public class TextService : ITextService
    {
        public int NumberOFWordsInString(Text text)
        {
            int count = 0;
            bool wasInWord = false;
            bool inWord = false;

            for (int i = 0; i < text.Input.Length; i++)
            {
                if (inWord)
                {
                    wasInWord = true;
                }

                if (Char.IsWhiteSpace(text.Input[i]))
                {
                    if (wasInWord)
                    {
                        count++;
                        wasInWord = false;
                    }
                    inWord = false;
                }
                else
                {
                    inWord = true;
                }
            }

            // Check to see if we got out with seeing a word
            if (wasInWord)
            {
                count++;
            }

            return count;
            }


    }


}
