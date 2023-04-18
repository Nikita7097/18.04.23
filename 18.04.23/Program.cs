using System;
using System.Reflection;
Type myType = typeof(SamplesArray);
Console.WriteLine("Методы:");
foreach (MethodInfo method in myType.GetMethods())
{
    string modificator = "";
    if (method.IsAbstract) modificator += "Abstract ";
    if (method.IsFamily) modificator += "Family ";
    if (method.IsFamilyAndAssembly) modificator += "FamilyAndAssambly ";
    if (method.IsFamilyOrAssembly) modificator += "FamilyOrAssambly ";
    if (method.IsAssembly) modificator += "Assembly ";
    if (method.IsPrivate) modificator += "Private ";
    if (method.IsPublic) modificator += "Public ";
    if (method.IsConstructor) modificator += "Constructor ";
    if (method.IsStatic) modificator += "static ";
    if (method.IsVirtual) modificator += "virtual ";
    Console.WriteLine($"{modificator}{method.ReturnType.Name} {method.Name} ()");
}
Console.WriteLine("Конструкторы:");
foreach (ConstructorInfo ctor in myType.GetConstructors(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
    string modificator = "";

    // получаем модификатор доступа
    if (ctor.IsPublic)
        modificator += "public";
    else if (ctor.IsPrivate)
        modificator += "private";
    else if (ctor.IsAssembly)
        modificator += "internal";
    else if (ctor.IsFamily)
        modificator += "protected";
    else if (ctor.IsFamilyAndAssembly)
        modificator += "private protected";
    else if (ctor.IsFamilyOrAssembly)
        modificator += "protected internal";

    Console.Write($"{modificator} {myType.Name}(");
    // получаем параметры конструктора
    ParameterInfo[] parameters = ctor.GetParameters();
    for (int i = 0; i < parameters.Length; i++)
    {
        var param = parameters[i];
        Console.Write($"{param.ParameterType.Name} {param.Name}");
        if (i < parameters.Length - 1) Console.Write(", ");
    }
    Console.WriteLine(")");
}
Console.WriteLine("Поля:");
foreach (FieldInfo field in myType.GetFields())
{
    string modificator = "";

    // получаем модификатор доступа
    if (field.IsFamily)
        modificator += "public ";
    else if (field.IsFamilyAndAssembly)
        modificator += "private ";
    else if (field.IsFamilyOrAssembly)
        modificator += "internal ";
    else if (field.IsAssembly)
        modificator += "protected ";
    else if (field.IsPrivate)
        modificator += "private protected ";
    else if (field.IsPublic)
        modificator += "protected internal ";

    // если поле статическое
    if (field.IsStatic) modificator += "static ";

    Console.WriteLine($"{modificator}{field.FieldType.Name} {field.Name}");
}
foreach (PropertyInfo prop in myType.GetProperties(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
{
    Console.Write($"{prop.PropertyType} {prop.Name} {{");

    // если свойство доступно для чтения
    if (prop.CanRead) Console.Write("get;");
    // если свойство доступно для записи
    if (prop.CanWrite) Console.Write("set;");
    Console.WriteLine("}");
}

public class SamplesArray
{
    public static void Main1()
    {
        String[] myArr = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
        Console.WriteLine("The original array initially contains:");
        PrintIndexAndValues(myArr);
        ArraySegment<string> myArrSegAll = new ArraySegment<string>(myArr);
        Console.WriteLine("The first array segment (with all the array's elements) contains:");
        PrintIndexAndValues(myArrSegAll);
        ArraySegment<string> myArrSegMid = new ArraySegment<string>(myArr, 2, 5);
        Console.WriteLine("The second array segment (with the middle five elements) contains:");
        PrintIndexAndValues(myArrSegMid);
        myArrSegAll.Array[3] = "LION";
        Console.WriteLine("After the first array segment is modified, the second array segment now contains:");
        PrintIndexAndValues(myArrSegMid);
    }

    public static void PrintIndexAndValues(ArraySegment<string> arrSeg)
    {
        for (int i = arrSeg.Offset; i < (arrSeg.Offset + arrSeg.Count); i++)
        {
            Console.WriteLine("   [{0}] : {1}", i, arrSeg.Array[i]);
        }
        Console.WriteLine();
    }

    public static void PrintIndexAndValues(String[] myArr)
    {
        for (int i = 0; i < myArr.Length; i++)
        {
            Console.WriteLine("   [{0}] : {1}", i, myArr[i]);
        }
        Console.WriteLine();
    }
}