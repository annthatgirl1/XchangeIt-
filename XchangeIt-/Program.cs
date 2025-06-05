// See https://aka.ms/new-console-template for more information

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; }

    public Task(int id, string title, string description, Priority priority = Priority.Medium)
    {
        Id = id;
        Title = title;
        Description = description;
        IsCompleted = false;
        CreatedDate = DateTime.Now;
        Priority = priority;
    }

    public override string ToString()
    {
        string status = IsCompleted ? "✓" : "○";
        string prioritySymbol = Priority switch
        {
            Priority.High => "🔴",
            Priority.Medium => "🟡",
            Priority.Low => "🟢",
            _ => "⚪"
        };

        string dueDateStr = DueDate.HasValue ? $" (Due: {DueDate.Value:MM/dd/yyyy})" : "";

        return $"{status} [{Id}] {prioritySymbol} {Title}{dueDateStr}";
    }
}

// Priority enumeration
public enum Priority
{
    Low = 1,
    Medium = 2,
    High = 3
}

// Main TaskManager class

namespace CurrencyConverter
{
    // Currency class to store currency information
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public Currency(string code, string name, string symbol)
        {
            Code = code;
            Name = name;
            Symbol = symbol;
        }

        public override string ToString()
        {
            return $"{Code} - {Name} ({Symbol})";
        }
    }

    // Conversion history record
    public class ConversionRecord
    {
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public decimal Result { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp:MM/dd/yyyy HH:mm}] {Amount} {FromCurrency} → {Result:F2} {ToCurrency} (Rate: {Rate:F4})";
        }
    }

    // Main Currency Converter class
    public class CurrencyConverter
    {
        private Dictionary<string, Currency> currencies;
        private Dictionary<string, Dictionary<string, decimal>> exchangeRates;
        private List<ConversionRecord> conversionHistory;
        private List<string> favoritePairs;

        public CurrencyConverter()
        {
            InitializeCurrencies();
            InitializeExchangeRates();
            conversionHistory = new List<ConversionRecord>();
            favoritePairs = new List<string>();
        }

        private void InitializeCurrencies()
        {
            currencies = new Dictionary<string, Currency>
            {
                { "USD", new Currency("USD", "US Dollar", "$") },
                { "EUR", new Currency("EUR", "Euro", "€") },
                { "GBP", new Currency("GBP", "British Pound", "£") },
                { "JPY", new Currency("JPY", "Japanese Yen", "¥") },
                { "CAD", new Currency("CAD", "Canadian Dollar", "C$") },
                { "AUD", new Currency("AUD", "Australian Dollar", "A$") },
                { "CHF", new Currency("CHF", "Swiss Franc", "CHF") },
                { "CNY", new Currency("CNY", "Chinese Yuan", "¥") },
                { "INR", new Currency("INR", "Indian Rupee", "₹") },
                { "KRW", new Currency("KRW", "South Korean Won", "₩") },
                { "VND", new Currency("VND", "Vietnamese Dong", "₫") },
                { "SGD", new Currency("SGD", "Singapore Dollar", "S$") },
                { "THB", new Currency("THB", "Thai Baht", "฿") },
                { "MYR", new Currency("MYR", "Malaysian Ringgit", "RM") }
            };
        }

        private void InitializeExchangeRates()
        {
            exchangeRates = new Dictionary<string, Dictionary<string, decimal>>
            {
                ["USD"] = new Dictionary<string, decimal>
                {
                    ["EUR"] = 0.85m,
                    ["GBP"] = 0.73m,
                    ["JPY"] = 110m,
                    ["CAD"] = 1.25m,
                    ["AUD"] = 1.35m,
                    ["CHF"] = 0.92m,
                    ["CNY"] = 6.45m,
                    ["INR"] = 74.5m,
                    ["KRW"] = 1180m,
                    ["VND"] = 23500m,
                    ["SGD"] = 1.35m,
                    ["THB"] = 33.5m,
                    ["MYR"] = 4.2m
                },
                ["EUR"] = new Dictionary<string, decimal>
                {
                    ["USD"] = 1.18m,
                    ["GBP"] = 0.86m,
                    ["JPY"] = 129.5m,
                    ["CAD"] = 1.47m,
                    ["AUD"] = 1.59m,
                    ["CHF"] = 1.08m,
                    ["CNY"] = 7.6m,
                    ["INR"] = 87.8m,
                    ["KRW"] = 1391m,
                    ["VND"] = 27650m,
                    ["SGD"] = 1.59m,
                    ["THB"] = 39.4m,
                    ["MYR"] = 4.95m
                },
                ["GBP"] = new Dictionary<string, decimal>
                {
                    ["USD"] = 1.37m,
                    ["EUR"] = 1.16m,
                    ["JPY"] = 150.7m,
                    ["CAD"] = 1.71m,
                    ["AUD"] = 1.85m,
                    ["CHF"] = 1.26m,
                    ["CNY"] = 8.84m,
                    ["INR"] = 102.1m,
                    ["KRW"] = 1616m,
                    ["VND"] = 32150m,
                    ["SGD"] = 1.85m,
                    ["THB"] = 45.8m,
                    ["MYR"] = 5.75m
                },
                ["VND"] = new Dictionary<string, decimal>
                {
                    ["USD"] = 0.0000426m,
                    ["EUR"] = 0.0000362m,
                    ["GBP"] = 0.0000311m,
                    ["JPY"] = 0.0047m,
                    ["CAD"] = 0.0000532m,
                    ["AUD"] = 0.0000575m,
                    ["CHF"] = 0.0000390m,
                    ["CNY"] = 0.000275m,
                    ["INR"] = 0.00317m,
                    ["KRW"] = 0.050m,
                    ["SGD"] = 0.0000575m,
                    ["THB"] = 0.00143m,
                    ["MYR"] = 0.00018m
                }
                // Add more rates as needed...
            };

            // Fill in reverse rates automatically
            foreach (var fromCurrency in exchangeRates.Keys.ToList())
            {
                foreach (var toCurrency in exchangeRates[fromCurrency].Keys.ToList())
                {
                    if (!exchangeRates.ContainsKey(toCurrency))
                        exchangeRates[toCurrency] = new Dictionary<string, decimal>();

                    if (!exchangeRates[toCurrency].ContainsKey(fromCurrency))
                        exchangeRates[toCurrency][fromCurrency] = 1 / exchangeRates[fromCurrency][toCurrency];
                }
            }
        }

        public void Run()
        {
            Console.Clear();
            ShowWelcomeMessage();

            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine()?.Trim();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ConvertCurrency();
                            break;
                        case "2":
                            ShowAllCurrencies();
                            break;
                        case "3":
                            ShowConversionHistory();
                            break;
                        case "4":
                            ManageFavorites();
                            break;
                        case "5":
                            QuickCalculator();
                            break;
                        case "6":
                            ShowExchangeRates();
                            break;
                        case "7":
                            ClearHistory();
                            break;
                        case "0":
                            Console.WriteLine("\n👋 Thank you for using Currency Converter!");
                            return;
                        default:
                            Console.WriteLine("❌ Invalid choice!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine("💱 CURRENCY CONVERTER - C# CONSOLE APP");
            Console.WriteLine("=====================================");
            Console.WriteLine("Welcome to the Currency Converter application!");
            Console.WriteLine();
        }

        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("💱 CURRENCY CONVERTER - MAIN MENU");
            Console.WriteLine("==================================");
            Console.WriteLine("1. 💰 Convert Currency");
            Console.WriteLine("2. 📋 View Currency List");
            Console.WriteLine("3. 📊 Conversion History");
            Console.WriteLine("4. ⭐ Manage Favorites");
            Console.WriteLine("5. 🧮 Quick Calculator");
            Console.WriteLine("6. 📈 Exchange Rates Table");
            Console.WriteLine("7. 🗑️ Clear History");
            Console.WriteLine("0. 🚪 Exit");
            Console.WriteLine();
            Console.Write("Select function (0-7): ");
        }

        private void ConvertCurrency()
        {
            Console.Clear();
            Console.WriteLine("💰 CONVERT CURRENCY");
            Console.WriteLine("=====================");

            // Show favorite pairs if any
            if (favoritePairs.Count > 0)
            {
                Console.WriteLine("⭐ Favorite Currency Pairs:");
                for (int i = 0; i < favoritePairs.Count; i++)
                {
                    Console.WriteLine($"   {i + 1}. {favoritePairs[i]}");
                }
                Console.WriteLine();
            }

            // Get amount
            Console.Write("Enter amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("❌ Invalid amount!");
                return;
            }

            // Get from currency
            Console.Write("From currency (e.g., USD): ");
            string fromCurrency = Console.ReadLine()?.ToUpper().Trim();
            if (!currencies.ContainsKey(fromCurrency))
            {
                Console.WriteLine("❌ Invalid currency code!");
                return;
            }

            // Get to currency
            Console.Write("To currency (e.g., VND): ");
            string toCurrency = Console.ReadLine()?.ToUpper().Trim();
            if (!currencies.ContainsKey(toCurrency))
            {
                Console.WriteLine("❌ Invalid currency code!");
                return;
            }

            // Perform conversion
            if (fromCurrency == toCurrency)
            {
                Console.WriteLine($"\n✅ Result: {amount:F2} {fromCurrency}");
                return;
            }

            if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates[fromCurrency].ContainsKey(toCurrency))
            {
                Console.WriteLine("❌ No exchange rate for this currency pair!");
                return;
            }

            decimal rate = exchangeRates[fromCurrency][toCurrency];
            decimal result = amount * rate;

            // Display result
            Console.WriteLine("\n✅ CONVERSION RESULT");
            Console.WriteLine("====================");
            Console.WriteLine($"Original amount: {amount:F2} {fromCurrency} ({currencies[fromCurrency].Name})");
            Console.WriteLine($"Exchange rate: 1 {fromCurrency} = {rate:F4} {toCurrency}");
            Console.WriteLine($"Result: {result:F2} {toCurrency} ({currencies[toCurrency].Name})");
            Console.WriteLine($"Symbol: {currencies[toCurrency].Symbol}{result:F2}");

            // Add to history
            conversionHistory.Insert(0, new ConversionRecord
            {
                Timestamp = DateTime.Now,
                Amount = amount,
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Rate = rate,
                Result = result
            });

            // Keep only last 20 records
            if (conversionHistory.Count > 20)
                conversionHistory.RemoveAt(conversionHistory.Count - 1);

            // Ask to add to favorites
            string pairKey = $"{fromCurrency} → {toCurrency}";
            if (!favoritePairs.Contains(pairKey))
            {
                Console.Write("\n⭐ Add this currency pair to favorites? (y/n): ");
                if (Console.ReadLine()?.ToLower().Trim() == "y")
                {
                    favoritePairs.Add(pairKey);
                    Console.WriteLine("✅ Added to favorites list!");
                }
            }
        }

        private void ShowAllCurrencies()
        {
            Console.Clear();
            Console.WriteLine("📋 CURRENCY LIST");
            Console.WriteLine("====================");

            int count = 0;
            foreach (var currency in currencies.Values)
            {
                Console.WriteLine($"{++count,2}. {currency}");
            }

            Console.WriteLine($"\nTotal: {currencies.Count} currencies");
        }

        private void ShowConversionHistory()
        {
            Console.Clear();
            Console.WriteLine("📊 CONVERSION HISTORY");
            Console.WriteLine("======================");

            if (conversionHistory.Count == 0)
            {
                Console.WriteLine("📝 No conversion history yet.");
                return;
            }

            Console.WriteLine($"Displaying {conversionHistory.Count} most recent conversions:\n");

            for (int i = 0; i < conversionHistory.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {conversionHistory[i]}");
            }

            // Show statistics
            var totalAmount = conversionHistory.Sum(h => h.Amount);
            var mostUsedFrom = conversionHistory.GroupBy(h => h.FromCurrency)
                                               .OrderByDescending(g => g.Count())
                                               .FirstOrDefault()?.Key;
            var mostUsedTo = conversionHistory.GroupBy(h => h.ToCurrency)
                                             .OrderByDescending(g => g.Count())
                                             .FirstOrDefault()?.Key;

            Console.WriteLine("\n📈 STATISTICS:");
            Console.WriteLine($"- Total conversions: {conversionHistory.Count}");
            Console.WriteLine($"- Most used source currency: {mostUsedFrom}");
            Console.WriteLine($"- Most used target currency: {mostUsedTo}");
        }

        private void ManageFavorites()
        {
            Console.Clear();
            Console.WriteLine("⭐ MANAGE FAVORITES");
            Console.WriteLine("===================");

            if (favoritePairs.Count == 0)
            {
                Console.WriteLine("📝 No favorite currency pairs yet.");
                Console.WriteLine("\n1. Add favorite currency pair");
                Console.WriteLine("0. Back");
                Console.Write("\nChoose: ");

                var choice = Console.ReadLine()?.Trim();
                if (choice == "1")
                {
                    AddFavoritePair();
                }
                return;
            }

            Console.WriteLine("List of favorite currency pairs:\n");
            for (int i = 0; i < favoritePairs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {favoritePairs[i]}");
            }

            Console.WriteLine("\n1. Use favorite currency pair");
            Console.WriteLine("2. Add new currency pair");
            Console.WriteLine("3. Remove favorite currency pair");
            Console.WriteLine("0. Back");
            Console.Write("\nChoose: ");

            var option = Console.ReadLine()?.Trim();
            switch (option)
            {
                case "1":
                    UseFavoritePair();
                    break;
                case "2":
                    AddFavoritePair();
                    break;
                case "3":
                    RemoveFavoritePair();
                    break;
            }
        }

        private void AddFavoritePair()
        {
            Console.Write("\nEnter source currency (e.g., USD): ");
            string from = Console.ReadLine()?.ToUpper().Trim();

            Console.Write("Enter target currency (e.g., VND): ");
            string to = Console.ReadLine()?.ToUpper().Trim();

            if (currencies.ContainsKey(from) && currencies.ContainsKey(to))
            {
                string pair = $"{from} → {to}";
                if (!favoritePairs.Contains(pair))
                {
                    favoritePairs.Add(pair);
                    Console.WriteLine($"✅ Added '{pair}' to favorites!");
                }
                else
                {
                    Console.WriteLine("⚠️ This currency pair is already in the favorites list!");
                }
            }
            else
            {
                Console.WriteLine("❌ Invalid currency code!");
            }
        }

        private void UseFavoritePair()
        {
            Console.Write("\nSelect the number of the currency pair: ");
            if (int.TryParse(Console.ReadLine(), out int index) &&
                index > 0 && index <= favoritePairs.Count)
            {
                string[] parts = favoritePairs[index - 1].Split(" → ");
                Console.Write("Enter amount: ");

                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    // Simulate conversion with the selected pair
                    string fromCurrency = parts[0];
                    string toCurrency = parts[1];

                    if (exchangeRates.ContainsKey(fromCurrency) &&
                        exchangeRates[fromCurrency].ContainsKey(toCurrency))
                    {
                        decimal rate = exchangeRates[fromCurrency][toCurrency];
                        decimal result = amount * rate;

                        Console.WriteLine($"\n✅ {amount:F2} {fromCurrency} = {result:F2} {toCurrency}");
                        Console.WriteLine($"Exchange rate: {rate:F4}");
                    }
                }
            }
            else
            {
                Console.WriteLine("❌ Invalid choice!");
            }
        }

        private void RemoveFavoritePair()
        {
            Console.Write("\nSelect the number of the currency pair to remove: ");
            if (int.TryParse(Console.ReadLine(), out int index) &&
                index > 0 && index <= favoritePairs.Count)
            {
                string removed = favoritePairs[index - 1];
                favoritePairs.RemoveAt(index - 1);
                Console.WriteLine($"✅ Removed '{removed}' from favorites list!");
            }
            else
            {
                Console.WriteLine("❌ Invalid choice!");
            }
        }

        private void QuickCalculator()
        {
            Console.Clear();
            Console.WriteLine("🧮 QUICK CALCULATOR");
            Console.WriteLine("==================");

            Console.Write("Enter first number: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal num1))
            {
                Console.WriteLine("❌ Invalid number!");
                return;
            }

            Console.Write("Enter operation (+, -, *, /): ");
            string operation = Console.ReadLine()?.Trim();

            Console.Write("Enter second number: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal num2))
            {
                Console.WriteLine("❌ Invalid number!");
                return;
            }

            decimal result;
            string operationName = "";

            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    operationName = "addition";
                    break;
                case "-":
                    result = num1 - num2;
                    operationName = "subtraction";
                    break;
                case "*":
                    result = num1 * num2;
                    operationName = "multiplication";
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("❌ Cannot divide by zero!");
                        return;
                    }
                    result = num1 / num2;
                    operationName = "division";
                    break;
                default:
                    Console.WriteLine("❌ Invalid operation!");
                    return;
            }

            Console.WriteLine($"\n✅ Result of {operationName} operation:");
            Console.WriteLine($"{num1} {operation} {num2} = {result:F2}");
        }

        private void ShowExchangeRates()
        {
            Console.Clear();
            Console.WriteLine("📈 EXCHANGE RATES TABLE");
            Console.WriteLine("===============");

            Console.Write("Enter base currency code (e.g., USD): ");
            string baseCurrency = Console.ReadLine()?.ToUpper().Trim();

            if (!currencies.ContainsKey(baseCurrency))
            {
                Console.WriteLine("❌ Invalid currency code!");
                return;
            }

            if (!exchangeRates.ContainsKey(baseCurrency))
            {
                Console.WriteLine("❌ No exchange rates for this currency!");
                return;
            }

            Console.WriteLine($"\nExchange rates from {baseCurrency} ({currencies[baseCurrency].Name}):");
            Console.WriteLine(new string('-', 50));

            foreach (var rate in exchangeRates[baseCurrency].OrderBy(r => r.Key))
            {
                string targetCurrency = rate.Key;
                decimal exchangeRate = rate.Value;

                Console.WriteLine($"1 {baseCurrency} = {exchangeRate:F4} {targetCurrency} ({currencies[targetCurrency].Name})");
            }
        }

        private void ClearHistory()
        {
            Console.Clear();
            Console.WriteLine("🗑️ CLEAR HISTORY");
            Console.WriteLine("===============");

            if (conversionHistory.Count == 0)
            {
                Console.WriteLine("📝 No history to clear.");
                return;
            }

            Console.Write($"Are you sure you want to delete {conversionHistory.Count} history records? (y/n): ");
            if (Console.ReadLine()?.ToLower().Trim() == "y")
            {
                conversionHistory.Clear();
                Console.WriteLine("✅ All history cleared!");
            }
            else
            {
                Console.WriteLine("❌ Operation canceled.");
            }
        }
    }

    // Main Program class
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Set console encoding for Vietnamese characters
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.InputEncoding = System.Text.Encoding.UTF8;

                var converter = new CurrencyConverter();
                converter.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ System error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}