using System;
using System.Collections.Generic;

namespace CommandParser_Alpha
{
    public class CommandParser
    {
        private readonly Dictionary<string, Func<Queue<string>?, object?>> commands;

        public CommandParser(Dictionary<string, Func<Queue<string>?, object?>> _commands)
        {
            commands = _commands;
        }

        public void Prompt(string promptMessage = "", string exitCommand = "", Queue<string>? autoExecQueue = null)
        {
            // Loop until the exit command is given
            while (true)
            {
                if (!string.IsNullOrEmpty(promptMessage))
                    Console.Write(promptMessage);

                // This should help prevent the prompt firing multiple times
                // if the user presses Enter during the invoked function.
                string command = "";
                while (string.IsNullOrWhiteSpace(command))
                {
                    // If there is a command on the auto-exec queue, dequeue it and run that command.
                    // Otherwise, wait for the user's input.
                    if (autoExecQueue?.Count > 0)
                    {
                        command += autoExecQueue.Dequeue();
                    }
                    else
                    {
                        command += Console.ReadLine()?.ToLowerInvariant();
                    }
                }

                if (!string.IsNullOrEmpty(exitCommand))
                    if (command == exitCommand)
                        break;

                if (commands.ContainsKey(command))
                {
                    // Pass the auto-exec queue into the invoked function
                    commands[command].Invoke(autoExecQueue);
                }
                else
                {
                    Console.WriteLine($"\"{command}\" is not a valid command.");
                }
            }
        }
    }
}
