// 8. Написать программу со следующими командами:
// - SetNumbers – пользователь вводит числа через пробел, а программа запоминает их в массив
// - AddNumbers – пользователь вводит числа, которые добавятся к уже существующему массиву
// - RemoveNumbers -  пользователь вводит числа, которые если  найдутся в массиве, то будут удалены
// - Numbers – ввывод текущего массива
// - Sum – найдется сумма всех элементов чисел
// По желанию можно добавить методов работы с числовыми массивами:
// - найти локальные максимумы
// - вывести статистику чисел (в любом красивом виде)
// - любые дополнительные по желанию

double[] numbersArray = new double[0];
char splitSymbol = ' ';
double sum;
bool programstop = false;

Console.WriteLine("Это программа работы с массивами чисел. Для корректной работы программы при вводе разделяйте числа пробелом.");
PrintMenu("Список доступных команд: ");
Console.WriteLine();

while (!programstop)
{
    string command = ReadString("Введите команду: ");
    Console.WriteLine();
    switch (command)
    {
        case "Menu":
            PrintMenu("Список доступных команд: ");
            Console.WriteLine();
            break;
        case "SetNumbers":
            string numbersInput = ReadString("Для заполнения массива введите числа через пробел: ");
            numbersArray = ParseArray(numbersInput, splitSymbol);
            Console.WriteLine("Текущий массив: ");
            Console.WriteLine('[' + String.Join("; ", numbersArray) + ']');
            Console.WriteLine();
            break;
        case "AddNumbers":
            string numbersAddingInput = ReadString("Для дополнения массива введите числа через пробел: ");
            double[] numbersToAddInArray = ParseArray(numbersAddingInput, splitSymbol);
            for (int i = 0; i < numbersToAddInArray.Length; i++)
            {
                numbersArray = AddToArray(numbersArray, numbersToAddInArray[i]);
            }
            Console.WriteLine("Текущий массив: ");
            Console.WriteLine('[' + String.Join("; ", numbersArray) + ']');
            Console.WriteLine();
            break;
        case "RemoveNumbers":
            string numbersToRemoveOnce = ReadString("Для удаления чисел из массива, введите эти числа через пробел (ввод одного числа удалит только один элемент в массиве): ");
            double[] numbersToRemoveFromArrayOnce = ParseArray(numbersToRemoveOnce, splitSymbol);
            for (int i = 0; i < numbersToRemoveFromArrayOnce.Length; i++)
            {
                bool noNumber = CheckNumberInArray(numbersArray, numbersToRemoveFromArrayOnce[i]);
                if (!noNumber)
                {
                    for (int j = 0; j < numbersToRemoveFromArrayOnce.Length; j++)
                    {
                        numbersArray = RemoveFromArrayNumber(numbersArray, numbersToRemoveFromArrayOnce[j]);
                    }
                    Console.WriteLine("Текущий массив: ");
                    Console.WriteLine('[' + String.Join("; ", numbersArray) + ']');
                    Console.WriteLine();
                }
                else
                    Console.WriteLine("Такого(их) числа(чисел) нет в текущем массиве");
                    Console.WriteLine();
            }
            break;
        case "RemoveAllNumbers":
            string numbersToRemove = ReadString("Для полного удаления чисел из массива, введите эти числа через пробел (ввод одного числа удалит все соответсвующие значения в массиве): ");
            double[] numbersToRemoveFromArray = ParseArray(numbersToRemove, splitSymbol);
            for (int i = 0; i < numbersToRemoveFromArray.Length; i++)
            {
                bool noNumber = CheckNumberInArray(numbersArray, numbersToRemoveFromArray[i]);
                if (!noNumber)
                {
                    for (int j = 0; j < numbersToRemoveFromArray.Length; j++)
                    {
                        numbersArray = RemoveAllNumbers(numbersArray, numbersToRemoveFromArray[j]);
                    }
                    Console.WriteLine("Текущий массив: ");
                    Console.WriteLine('[' + String.Join("; ", numbersArray) + ']');
                    Console.WriteLine();
                }
                else
                    Console.WriteLine("Такого(их) числа(чисел) нет в текущем массиве");
                    Console.WriteLine();
            }
            break;
        case "Numbers":
            Console.WriteLine("Текущий массив: ");
            Console.WriteLine('[' + String.Join("; ", numbersArray) + ']');
            Console.WriteLine();
            break;
        case "Sum":
            sum = SumAllNumbersInArray(numbersArray);
            Console.WriteLine($"Сумма всех элементов текущего массива = {sum}");
            break;
        case "Exit":
            programstop = true;
            break;
    }
}


// Считывание ввода числа
int ReadInt(string message)
{
    Console.Write(message);
    return Convert.ToInt32(Console.ReadLine());
}

// Считывание ввода текста
string ReadString(string message)
{
    Console.Write(message);
    return Console.ReadLine();
}

// AddToArray
double[] AddToArray(double[] array, double newNumber, int newPosIndex = -1)
{
    int i = 0;
    double[] arrayNew = new double[array.Length + 1];

    if (newPosIndex == -1)
        newPosIndex = arrayNew.Length - 1;

    for (i = 0; i < arrayNew.Length; i++)
    {
        if (i == newPosIndex)
            arrayNew[i] = Convert.ToDouble(newNumber);
        else if (i > newPosIndex)
            arrayNew[i] = Convert.ToDouble(array[i - 1]);
        else
            arrayNew[i] = Convert.ToDouble(array[i]);
    }
    return arrayNew;
}

double[] ParseArray(string rowOfNumbersInString, char split)
{
    // считаем количество чисел
    rowOfNumbersInString = rowOfNumbersInString.Trim(); // убираем случайные пробелы в начале и конце строки
    int numbersCount = 1;
    for (int i = 0; i < rowOfNumbersInString.Length; i++)
    {
        if (rowOfNumbersInString[i] == split)
            numbersCount++;
        while (rowOfNumbersInString[i] == split) // долистываем до значения
        {
            if (i < rowOfNumbersInString.Length - 1)
                i++;
            else
                break;
        }
    }
    // заполняем массив
    double[] numbers = new double[numbersCount];
    int numberIndex = 0;
    string temp = "";
    for (int i = 0; i < rowOfNumbersInString.Length; i++)
    {
        if (rowOfNumbersInString[i] == split)
        {
            numbers[numberIndex] = Convert.ToDouble(temp);
            numberIndex++;
            temp = "";
            while (rowOfNumbersInString[i + 1] == split) // долистываем до следующего значения
            {
                if (i < rowOfNumbersInString.Length - 1)
                    i++;
                else
                    break;
            }
        }
        else
        {
            temp += rowOfNumbersInString[i];
        }
    }
    numbers[numberIndex] = Convert.ToDouble(temp);
    return numbers;
}

// Проверка наличия элемента в массиве
bool CheckNumberInArray(double[] array, double numberToCheck)
{
    bool numberDoNotExist = true;
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] == numberToCheck)
            numberDoNotExist = false;
    }
    return numberDoNotExist;
}

// Убрать из массива какое-то значение
double[] RemoveFromArrayNumber(double[] array, double removeNumber)
{
    int i = 0;
    double[] arrayNew = new double[array.Length - 1];
    int removePosIndex = IndexOf(array, removeNumber);
    for (i = 0; i < arrayNew.Length; i++)
    {
        if (i >= removePosIndex)
            arrayNew[i] = array[i + 1];
        else
            arrayNew[i] = array[i];
    }
    return arrayNew;
}

// Убрать из массива все одинаковые заданные значения
double[] RemoveAllNumbers(double[] array, double removeNumber)
{
    int count = 1;
    bool numberIsNotInArray = false;
    while (!numberIsNotInArray)
    {
        array = RemoveFromArrayNumber(array, removeNumber);
        numberIsNotInArray = CheckNumberInArray(array, removeNumber);
        count++;
    }
    return array;
}

// Найти в массиве индекс желаемого элемента
int IndexOf(double[] array, double find)
{
    int i = 0;
    int position = -1;
    for (i = 0; i < array.Length; i++)
    {
        if (array[i] == find)
        {
            position = i;
            break;
        }
    }
    return position;
}

// Сумма всех элементов массива
double SumAllNumbersInArray(double[] array)
{
    double sumOfAllNumbers = 0;
    for (int i = 0; i < array.Length; i++)
    {
        sumOfAllNumbers += array[i];
    }
    return sumOfAllNumbers;
}

void PrintMenu(string message)
{
    Console.WriteLine("1. Menu - вызов всех доступных команд");
    Console.WriteLine("2. SetNumbers - задать новый массив чисел");
    Console.WriteLine("3. AddNumbers - добавить числа к существующему массиву");
    Console.WriteLine("4. RemoveNumbers - убрать из массива по одному элементу каждого заданного числа");
    Console.WriteLine("5. RemoveAllNumbers - убрать из массива все заданные числа");
    Console.WriteLine("6. Numbers - вывод текущего массива");
    Console.WriteLine("7. Sum - сумма всех элементов массива");
    Console.WriteLine("8. Exit - завершение программы");
}


