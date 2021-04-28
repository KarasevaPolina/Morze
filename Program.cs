using System;

namespace Morze
{
    public static class Morze
    {
        private static String _string; //Хранит сообщение

        //Массив, содержащий латинский алфавит и цифры
        private static char[] _arrayLetter = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                                               'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2',
                                               '3', '4', '5', '6', '7', '8', '9', '0'};
        //Массив шифров из азбуки Морзе
        private static String[] _arrayMorze = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---",
                                                "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-",
                                                "..-", "...-", ".--", "-..-", "-.--", "--.", ".----", "..---", "...--",
                                                "....-", ".....", "-....", "--...", "---..", "----.", "-----"};

        //Функция проверки корректности ввода сообщения на Морзе
        private static bool IsMorze()
        {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только символы '-', '.' и пробел
            while (index < length)
            {
                if (_string[index] == '-' || _string[index] == '.' || _string[index] == ' ')
                    index++;
                else
                    return false;
            }
            return true;
        }

        //Функция проверки корректности ввода сообщения на латинице
        private static bool IsLetter()
        {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только буквы латинского алфавита и цифры
            while (index < length)
            {
                if ((_string[index] >= 'A' && _string[index] <= 'Z') ||
                    (_string[index] >= 'a' && _string[index] <= 'z') ||
                    (_string[index] >= '0' && _string[index] <= '9') ||
                    _string[index] == ' ')
                    index++;
                else
                    return false;
            }
            return true;
        }

        //Функция перевода сообщения на латинице в сообщение Морзе
        public static String TranslateToMorze(String stringLetter) //stringLetter - введённое сообщение
        {
            _string = stringLetter;

            //Если сообщение введено корректно, выполняем перевод
            if (IsLetter() == true)
            {
                _string = _string.Trim(); //Удаление пробелов спереди, в конце сообщения
                _string = _string.ToUpper();
                String resultString = "\0"; //Результирующая строка в виде сообщения на Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    indexFromArrays = 0, //Индекс для итераций по массивам латинского алфавита и азбуки Морзе
                    countSpaces = 0; //Количество пробелов
                while (index < length)
                {
                    //Перевод символа из сообщения в шифр Морзе
                    while (indexFromArrays < _arrayLetter.Length)
                    {
                        if (_string[index] == _arrayLetter[indexFromArrays])
                        {
                            resultString += _arrayMorze[indexFromArrays] + " ";
                            countSpaces = 0;
                            break;
                        }
                        indexFromArrays++;
                    }
                    //Устранение лишних пробелов внутри сообщения
                    if (_string[index] == ' ')
                    {
                        if (countSpaces > 0)
                        {
                            index++;
                            indexFromArrays = 0;
                            continue;
                        }
                        resultString += "  ";
                        countSpaces++;
                    }
                    index++;
                    indexFromArrays = 0;
                }
                return resultString;
            }
            else
            {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }

        //Функция перевода сообщения на абуке Морзе в сообщение на латинице
        public static String TranslateToLetter(String stringMorze) //stringMorze - введённое сообщение
        {
            _string = stringMorze;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsMorze() == true)
            {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                String resultString = "\0"; //Результирующая строка в виде сообщения на Морзе
                String bufString = ""; //Строка для хранения шифров символов Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    indexFromArray = 0, //Индекс для итераций по массивам латинского алфавита и азбуки Морзе
                    countSpaces = 0; //Количество пробелов
                bool isCorrect = false; //Выполняет проверку на правильность введённых кодов
                while (index < length)
                {
                    //Чтение шифра
                    if (_string[index] != ' ')
                        bufString += _string[index];
                    else
                    {
                        //Устранение лишних пробелов внутри сообщения
                        if (bufString == "")
                        {
                            if (countSpaces > 0)
                            {
                                index++;
                                continue;
                            }
                            resultString += " ";
                            countSpaces++;
                        }
                        else
                        {
                            countSpaces = 0;

                            //Перевод шифра из сообщения на Морзе в символ на латинице
                            while (indexFromArray < _arrayMorze.Length)
                            {
                                if (bufString == _arrayMorze[indexFromArray])
                                {
                                    isCorrect = true;
                                    resultString += _arrayLetter[indexFromArray];
                                    bufString = "";
                                    indexFromArray = 0;
                                    break;
                                }
                                indexFromArray++;
                            }

                            //Вывод ошибки при содержании несуществующего шифра в сообщении
                            if (isCorrect == false)
                            {
                                Console.WriteLine("Letter isn't correct!");
                                return "\0";
                            }
                            isCorrect = false;
                        }
                    }
                    index++;
                }

                //Перевод последнего шифра из сообщения на азбуке Морзе в символ на латинице
                while (indexFromArray < _arrayMorze.Length)
                {
                    if (bufString == _arrayMorze[indexFromArray])
                    {
                        isCorrect = true;
                        resultString += _arrayLetter[indexFromArray];
                        break;
                    }
                    indexFromArray++;
                }

                //Вывод ошибки при содержании несуществующего шифра
                if (isCorrect == false)
                {
                    Console.WriteLine("Letter isn't correct!");
                    return "\0";
                }
                return resultString;
            }
            else
            {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            String stringLetter;

            Console.WriteLine("Choose: Morze or Leter? [m/l] ");
            char answer = Convert.ToChar(Console.ReadLine());

            switch (answer)
            {
                case 'm':
                    {
                        Console.Write("Enter morze-code: ");
                        stringLetter = Console.ReadLine();
                        Console.WriteLine(Morze.TranslateToLetter(stringLetter));
                    }
                    break;
                case 'l':
                    {
                        Console.Write("Enter letter: ");
                        stringLetter = Console.ReadLine();
                        Console.WriteLine(Morze.TranslateToMorze(stringLetter));
                    }
                    break;
                default:
                    {
                        Console.WriteLine("You can choose only m or l! ");
                    }
                    break;
            }
        }
    }
}
