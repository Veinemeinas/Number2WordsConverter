using System;
using System.Text.RegularExpressions;

namespace Number2WordsConverter
{
    public class Number2Words
    {
        private int Length { get; set; }
        private string Expression { get; set; }

        string[] name1 = new string[] { "nulis", "vienas", "du", "trys", "keturi", "penki", "šeši", "septyni", "aštuoni", "devyni" };
        string[] name2 = new string[] { "dešimt", "vienuolika", "dvylika", "trylika", "keturiolika", "penkiolika", "šešiolika", "septyniolika", "aštuoniolika", "devyniolika" };
        string[] name3 = new string[] { "dvidešimt", "trisdešimt", "keturiasdešimt", "penkiasdešimt", "šešiasdešimt", "septyniasdešimt", "aštuoniasdešimt", "devyniasdešimt" };
        string[] name4 = new string[] { "šimtas", "šimtai" };
        string[] name5 = new string[] { "tūkstantis", "tūkstančiai", "tūkstančių", "milijonas", "milijonai", "milijonų", "milijardas", "milijardai", "milijardų", "trilijonas", "trilijonai", "trilijonų", "kvadrilijonas", "kvadrilijonai", "kvadrilijonų", "kvintilijonas", "kvintilijonai", "kvintilijonų", "sekstilijonas", "sekstilijonai", "sekstilijonų", "septilijonas", "septilijonai", "septilijonų", "oktilijonas", "oktilijonai", "oktilijonų", "nonilijonas", "nonilijonai", "nonilijonų" };

        /// <summary>
        /// Returns the number in the Lithuanian writing language format
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetNumber(string number)
        {
            Length = number.Length;
            int position = Length;

            Validation(number);
            int[] numberArray = ToIntArray(number);

            for (int i = 0; i < Length; i++)
            {
                //Vykdoma kai skaičius yra 0.
                if (numberArray[i] == 0 && Length == 1)
                {
                    Expression = NumberUnits(numberArray[0]) + " ";
                }

                //Vykdoma kai skaičius patenka į intervalą 1-9.
                if (numberArray[i] > 0 && (position % 3 == 1 || position % 3 == 0) && position >= 1) // (position == 1 || position == 3 || position == 4 || position == 6 || position == 7 || position == 9 || position == 10 || position == 12 || position == 13 || position == 15 || position == 16 || position == 18 || position == 19 || position == 21 || position == 22 || position == 24 || position == 25 || position == 27 || position == 28 || position == 30 || position == 31 || position == 33)) || (numberArray[i] == 0 && length == 1)
                {
                    Expression += NumberUnits(numberArray[i]) + " ";
                }

                //Vykdoma kai skaičius patenka į intervalą 20-90 ir yra šimtosios dalies 10 kartotinis.
                if (numberArray[i] >= 2 && position % 3 == 2 && position >= 2) // (position == 2 || position == 5 || position == 8 || position == 11 || position == 14 || position == 17 || position == 20 || position == 23 || position == 26 || position == 29 || position == 32)
                {
                    Expression += NumberTens(numberArray[i]) + " ";
                }

                //Vykdoma kai skaičius patenka į intervalą 10-19.
                if (numberArray[i] == 1 && position % 3 == 2 && position >= 2) // position == 2 || position == 5 || position == 8 || position == 11 || position == 14 || position == 17 || position == 20 || position == 23 || position == 26 || position == 29 || position == 32)
                {
                    Expression += NumberTenToNineteen(numberArray[i + 1]) + " ";
                    position -= 1;
                    i += 1;
                }

                //Vykdoma kai skaičius yra tūkstantųjų dalių 100 kartotinis.
                if (numberArray[i] >= 1 && position % 3 == 0 && position >= 3) // position == 3 || position == 6 || position == 9 || position == 12 || position == 15 || position == 18 || position == 21 || position == 24 || position == 27 || position == 30 || position == 33
                {
                    Expression += NumberHundreds(numberArray[i]) + " ";
                }

                //Vykdoma kai skaičius yra 1000 kartotinis.
                if (position % 3 == 1 && position >= 4) // position == 4 || position == 7 || position == 10 || position == 13 || position == 16 || position == 19 || position == 22 || position == 25 || position == 28 || position == 31
                {
                    Expression += NumberThousands(numberArray[i], Length > position ? numberArray[i - 1] : 0, Length > position + 1 ? numberArray[i - 2] : 0, position) + " ";
                }

                position -= 1;
            }
            return Expression;
        }

        private void Validation(string number)
        {
            if (number.Length > 33)
            {
                throw new ArgumentOutOfRangeException("Number is to large of 999999999999999999999999999999999");
            }
            Regex regex = new Regex(@"^[0-9]+$");

            if (!regex.IsMatch(number))
            {
                throw new FormatException("Number is not in the correct format.");
            }
        }

        private int[] ToIntArray(string number)
        {
            int[] array = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                array[i] = Int32.Parse(number.Substring(i, 1));
            }
            return array;
        }

        private string NumberUnits(int value)
        {
            return name1[value];
        }

        private string NumberTens(int value)
        {
            return name3[value - 2];
        }

        private string NumberTenToNineteen(int value)
        {
            return name2[value];
        }

        private string NumberHundreds(int value)
        {
            if (value == 1)
            {
                value = 0;
            }
            else
            {
                value = 1;
            }
            return name4[value];
        }

        private string NumberThousands(int value, int valueBefore, int valueTwiceBifore, int position)
        {
            if (value == 1 && valueBefore != 1) //Tūkstantis
            {
                value = position - 4;
            }
            else if (value > 1 && valueBefore != 1) //Tūkstančiai
            {
                value = position - 3;
            }
            else if (valueBefore == 1 || ((valueBefore == 0 && valueTwiceBifore != 0) || (value == 0 && valueBefore >= 2))) //Tūkstančių
            {
                value = position - 2;
            }
            else if (value == 0 && valueBefore == 0 && valueTwiceBifore == 0)
            {
                return null; //Tuščia reikšmė
            }

            return name5[value];
        }
    }
}
