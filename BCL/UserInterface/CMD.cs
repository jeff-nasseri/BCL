using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BCL
{
    public static class CMD
    {
        private static StringBuilder builder = new StringBuilder();
        private static ConsoleKey[] keys_sp = new ConsoleKey[] { ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow,
            ConsoleKey.DownArrow, ConsoleKey.Enter, ConsoleKey.Tab ,ConsoleKey.Backspace};

        /// <summary>
        /// display program message to user
        /// </summary>
        /// <param name="message">current message</param>
        /// <param name="showType">current message type</param>
        /// <param name="line">if true console.writeLine used</param>
        public static void ShowApplicationMessageToUser(string message, ShowType showType = ShowType.INFO, bool line = true)
        {
            SetConsoleColor(showType);
            if (line)
                Console.WriteLine($"[{showType.ToString()}] : {message}");
            else
                Console.Write($"[{showType.ToString()}] : {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// set color in console
        /// </summary>
        /// <param name="showType">current message type</param>
        public static void SetConsoleColor(ShowType showType)
        {
            switch (showType)
            {
                case ShowType.INFO: Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; break;
                case ShowType.SUCCESS: Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Green; break;
                case ShowType.DANGER: Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red; break;
                case ShowType.ALERT: Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Yellow; break;
                case ShowType.DataTarget: Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Yellow; break;
            }
        }

        /// <summary>
        /// return ienumerable of all class command and functions command in specific class
        /// </summary>
        /// <param name="className">name of class command</param>
        private static IEnumerable<string> AllCommandsByClassName(string className)
        {
            var commands = (ProgramStorageQueries.GetInfoValueOfClassCommand()).OfType<string>().ToList();
            if (className != "")
            {
                commands.AddRange(ProgramStorageQueries.GetFunctionsCommand(className));
            }
            return commands;
        }

        /// <summary>
        /// read line from console
        /// </summary>
        /// <param name="className">name of class command</param>
        public static string[] ReadeUserCommandLineInput(string className = "")
        {
            var prefix = $"\n{className} > ";
            Console.Write(prefix);
            var commands = (ProgramStorageQueries.GetInfoValueOfClassCommand()).OfType<string>().ToList();
            var input = ReadeKey(prefix.Length - 1, AllCommandsByClassName(className));
            Console.WriteLine();

            var array = input.Split(' ');
            var length = array.Length;

            var args = new string[length];
            for (int i = 0; i < length; i++)
            {
                args[i] = array[i];
            }

            return args;
        }

        /// <summary>
        /// console configuration .please dont change argumants they are fixed and 
        /// change when route changed in console
        /// </summary>
        private static string ReadeKey(int startIndex, IEnumerable<string> commands)
        {
            builder.Clear();
            var cursorPosition = startIndex;
            var builderPosition = 0;

            while (true)
            {
                var input = Console.ReadKey(intercept: true);

                if (!keys_sp.Any(ks => ks == input.Key))
                {

                    Utilities.CopyAndRemoveChars(builder, builderPosition, builder.Length - builderPosition, out char[] array);

                    builder.Append(input.KeyChar);
                    builder.Append(array);

                    cursorPosition++;
                    builderPosition++;

                    Console.Write(input.KeyChar + new string(array));
                    Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                }
                else
                {
                    try
                    {
                        switch (input.Key)
                        {

                            case ConsoleKey.LeftArrow:
                                if (builderPosition > 0)
                                {
                                    cursorPosition -= 1; builderPosition -= 1; Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (builderPosition < builder.Length)
                                {
                                    cursorPosition += 1; builderPosition += 1; ; Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                                }
                                ; break;
                            case ConsoleKey.Backspace:
                                if (builder.Length > 0)
                                {
                                    Utilities.CopyAndRemoveChars(builder, builderPosition, builder.Length - builderPosition, out char[] array);
                                    builder.Remove(builderPosition - 1, 1);
                                    builder.Append(array);
                                    cursorPosition -= 1;
                                    builderPosition -= 1;
                                    Utilities.ClearCurrentInput(startIndex);
                                    Console.Write(builder.ToString());
                                    Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                                }
                                ; break;
                            case ConsoleKey.Tab:
                                {
                                    var match = Utilities.FindMatch(builder, commands);
                                    Utilities.ClearCurrentInput(startIndex);
                                    builder.Clear();
                                    builder.Append(match);
                                    Console.Write(builder.ToString());
                                    cursorPosition = match.Length + startIndex;
                                    builderPosition = match.Length;
                                    Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                                }
                                ; break;

                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    if (input.Key == ConsoleKey.Enter)
                        break;
                }

            }
            return builder.ToString();
        }
    }
}
