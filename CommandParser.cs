using System;
using System.Collections.Generic;

namespace CommandParser_Alpha
{
    public class CommandParser
    {
        private readonly Dictionary<string, Action> commands;

        public CommandParser(Dictionary<string, Action> _commands)
        {
            commands = _commands;
        }

        public void Prompt(string promptMessage = "", string exitCommand = "")
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(promptMessage))
                    Console.Write(promptMessage);

                string command = Console.ReadLine().ToLowerInvariant();

                if (!string.IsNullOrEmpty(exitCommand))
                    if (command == exitCommand)
                        break;

                if (commands.ContainsKey(command))
                {
                    commands[command].Invoke();
                }
                else
                {
                    Console.WriteLine($"\"{command}\" is not a valid command.");
                }
            }
        }
    }
}
