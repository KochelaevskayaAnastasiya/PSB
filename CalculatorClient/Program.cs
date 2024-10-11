using System.Net;

while (true)
{
    try
    {
        //зацикленное меню для многократного ввода с возможностью выхода
        Console.WriteLine("Выберите операцию: \n1 - Сложение \n2 - Вычитание \n3 - Умножение \n4 - Деление \n0 - Выход");
        var choice = Console.ReadLine();
        if (choice == "0") break;
        string operation = choice switch
        {
            "1" => "add",
            "2" => "subtract",
            "3" => "multiply",
            "4" => "divide",
            _ => throw new InvalidOperationException("Неверный выбор операции.")
        };

        Console.Write("Введите первое число: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите второе число: ");
        double b = double.Parse(Console.ReadLine());

        //замена запятой на точку необходима для использования дробных чисел
        string url = $"http://localhost:5075/api/calculator/{operation}?a={a}&b={b}".Replace(",", ".");

        //DangerousAcceptAnyServerCertificateValidator нужен для обхода ненадежного сертификата, код взят с https://learn.microsoft.com/ru-ru/aspnet/core/grpc/troubleshoot?view=aspnetcore-8.0
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        using var httpClient = new HttpClient(handler);

        //отправляем Get запрос
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            //если произошла ошибка, выводим ее
            string error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[Ошибка на сервере]: {error}");
        }
        else
        {
            //если успешно выводим ответ, приводим к double для общего вида дробным чисел на консоли (тк сервер возвращает их с точкой)
            string result = await response.Content.ReadAsStringAsync();
            double result2 = double.Parse(result.Replace(".", ","));
            Console.WriteLine($"Ответ: {result2}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Ошибка]: {ex.Message}");
    }
}
