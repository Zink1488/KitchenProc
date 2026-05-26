using KitchenProc.Enums;
using KitchenProc.Factories;
using KitchenProc.Implementations;
using KitchenProc.Interfaces;
using KitchenProc.Models;

namespace KitchenProc
{
    internal class Program
    {
        private static readonly List<KitchenProcessor> _processors = new();

        private static readonly Dictionary<int, (string Name, IKitchenFactory Factory)> _factories = new()
        {
            { 1, ("Простая (Plastic + Шинковка)",        new SimpleFactory())       },
            { 2, ("Профессиональная (Steel + Взбивание)", new ProfessionalFactory()) },
            { 3, ("Универсальная (Aluminum + Помол)",     new UniversalFactory())    },
        };

        private static readonly Dictionary<int, (string Name, IProcessingTool Tool)> _tools = new()
        {
            { 1, ("Шинковка",   new Cutting())  },
            { 2, ("Взбивание",  new Whisking()) },
            { 3, ("Помол",      new Grinding()) },
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _processors.Add(new SimpleFactory().CreateProcessor("BasicChop 100"));
            _processors.Add(new ProfessionalFactory().CreateProcessor("Сталин 3000"));
            _processors.Add(new UniversalFactory().CreateProcessor("Ультра правый 5000"));

            bool running = true;
            while (running)
            {
                PrintMainMenu();

                try
                {
                    string? input = Console.ReadLine();

                    if (!int.TryParse(input, out int choice))
                        throw new FormatException($"«{input}» — не число. Введите цифру от 1 до 6.");

                    switch (choice)
                    {
                        case 1: ShowAll();          break;
                        case 2: RunProcessor();     break;
                        case 3: ShowInfo();         break;
                        case 4: AddProcessor();     break;
                        case 5: RemoveProcessor();  break;
                        case 6: running = false;    break;
                        default: throw new ArgumentOutOfRangeException(nameof(choice), "Введите число от 1 до 6.");
                    }
                }
                catch (FormatException ex)
                {
                    PrintError($"Ошибка ввода: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    PrintError($"Неверный пункт меню: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    PrintError($"Недопустимая операция: {ex.Message}");
                }
                catch (Exception ex)
                {
                    PrintError($"Неожиданная ошибка: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey(intercept: true);
                }
            }

            Console.WriteLine("\nДо свидания!");
        }

        static void ShowAll()
        {
            if (_processors.Count == 0)
                throw new InvalidOperationException("Коллекция пуста. Сначала добавьте процессор.");

            PrintHeader("Список всех процессоров");
            for (int i = 0; i < _processors.Count; i++)
            {
                Console.Write($"  [{i + 1}] ");
                _processors[i].GetInfo();

                var caps = new List<string>();
                if (_processors[i] is IBlender)    caps.Add("IBlender");
                if (_processors[i] is IMixer)      caps.Add("IMixer");
                if (_processors[i] is IDoughMixer) caps.Add("IDoughMixer");
                if (caps.Count > 0)
                    Console.WriteLine($"       Доп. функции: {string.Join(", ", caps)}");
            }
        }
        static void RunProcessor()
        {
            if (_processors.Count == 0)
                throw new InvalidOperationException("Коллекция пуста. Сначала добавьте процессор.");

            ShowAll();
            Console.Write("\nВведите номер процессора для запуска: ");

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int idx))
                throw new FormatException($"«{input}» — не число.");

            if (idx < 1 || idx > _processors.Count)
                throw new ArgumentOutOfRangeException(nameof(idx), $"Номер должен быть от 1 до {_processors.Count}.");

            var p = _processors[idx - 1];
            PrintHeader($"Запуск: {p.Model}");

            p.GetInfo();
            p.Process();

            if (p is IBlender blender)    blender.Blend();
            if (p is IMixer mixer)        mixer.Mix();
            if (p is IDoughMixer dough)   dough.KneadDough();
        }

        static void ShowInfo()
        {
            if (_processors.Count == 0)
                throw new InvalidOperationException("Коллекция пуста. Сначала добавьте процессор.");

            PrintHeader("GetInfo() для каждого процессора");
            foreach (var p in _processors)
            {
                Console.Write("  → ");
                p.GetInfo();
            }
        }

        static void AddProcessor()
        {
            PrintHeader("Добавление нового процессора");

            // Выбор фабрики
            Console.WriteLine("Выберите тип процессора:");
            foreach (var kv in _factories)
                Console.WriteLine($"  [{kv.Key}] {kv.Value.Name}");
            Console.Write("Ваш выбор: ");

            string? factInput = Console.ReadLine();
            if (!int.TryParse(factInput, out int factChoice))
                throw new FormatException($"«{factInput}» — не число.");
            if (!_factories.ContainsKey(factChoice))
                throw new ArgumentOutOfRangeException(nameof(factChoice), $"Нет фабрики с номером {factChoice}.");

            Console.Write("Введите название модели: ");
            string? modelName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException("Название модели не может быть пустым.");

            Console.WriteLine("\nВыберите инструмент (Bridge-паттерн):");
            foreach (var kv in _tools)
                Console.WriteLine($"  [{kv.Key}] {kv.Value.Name}");
            Console.Write("Ваш выбор: ");

            string? toolInput = Console.ReadLine();
            if (!int.TryParse(toolInput, out int toolChoice))
                throw new FormatException($"«{toolInput}» — не число.");
            if (!_tools.ContainsKey(toolChoice))
                throw new ArgumentOutOfRangeException(nameof(toolChoice), $"Нет инструмента с номером {toolChoice}.");

            Console.WriteLine("\nВыберите материал корпуса:");
            var materials = Enum.GetValues<BodyMaterial>();
            for (int i = 0; i < materials.Length; i++)
                Console.WriteLine($"  [{i + 1}] {materials[i]}");
            Console.Write("Ваш выбор: ");

            string? matInput = Console.ReadLine();
            if (!int.TryParse(matInput, out int matChoice))
                throw new FormatException($"«{matInput}» — не число.");
            if (matChoice < 1 || matChoice > materials.Length)
                throw new ArgumentOutOfRangeException(nameof(matChoice), $"Нет материала с номером {matChoice}.");

            BodyMaterial selectedMaterial = materials[matChoice - 1];
            IProcessingTool selectedTool  = _tools[toolChoice].Tool;

            KitchenProcessor newProcessor = factChoice switch
            {
                1 => new SimpleProcessor(modelName,       selectedMaterial, selectedTool),
                2 => new ProfessionalProcessor(modelName, selectedMaterial, selectedTool),
                3 => new UniversalProcessor(modelName,    selectedMaterial, selectedTool),
                _ => throw new InvalidOperationException("Неизвестный тип фабрики.")
            };

            _processors.Add(newProcessor);
            Console.WriteLine($"\n✓ Процессор «{modelName}» успешно добавлен в коллекцию.");
        }

        
        static void RemoveProcessor()
        {
            if (_processors.Count == 0)
                throw new InvalidOperationException("Коллекция пуста — нечего удалять.");

            ShowAll();
            Console.Write("\nВведите номер процессора для удаления: ");

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int idx))
                throw new FormatException($"«{input}» — не число.");
            if (idx < 1 || idx > _processors.Count)
                throw new ArgumentOutOfRangeException(nameof(idx), $"Номер должен быть от 1 до {_processors.Count}.");

            string removedName = _processors[idx - 1].Model;
            _processors.RemoveAt(idx - 1);
            Console.WriteLine($"\n✓ Процессор «{removedName}» удалён из коллекции.");
        }

        
        static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║      КУХОННЫЙ ПРОЦЕССОР — МЕНЮ       ║");
            Console.WriteLine($"║  Процессоров в коллекции: {_processors.Count,-11}║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. Показать все процессоры           ║");
            Console.WriteLine("║  2. Запустить процессор               ║");
            Console.WriteLine("║  3. GetInfo всех процессоров          ║");
            Console.WriteLine("║  4. Добавить процессор                ║");
            Console.WriteLine("║  5. Удалить процессор                 ║");
            Console.WriteLine("║  6. Выход                             ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("Ваш выбор: ");
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine($"\n── {title} {new string('─', Math.Max(0, 38 - title.Length))}");
        }

        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✗ {message}");
            Console.ResetColor();
        }
    }
}
