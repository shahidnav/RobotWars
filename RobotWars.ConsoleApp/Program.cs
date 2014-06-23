using System;
using System.Collections.Generic;
using System.Text;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Command.Parser;
using RobotWars.Report;
using RobotWars.Robot;

namespace RobotWars.ConsoleApp
{
    internal class Program
    {
        private static IControlSystem ConstructControlSystem()
        {
            var battleArena = new BattleArena();
            var commandMatcher = new CommandMatcher();
            Func<Dimension, ISetBattleArenaDimensionsCommand> setBattleArenaCommandFactory =
                dimension => new SetBattleArenaDimensionsCommand(dimension);
            Func<Point, Heading, IPlaceRobotCommand> placeRobotCommandFactory =
                (point, heading) => new PlaceRobotCommand(point, heading);
            Func<List<Movement>, IMoveRobotCommand> moveRobotCommandFactory = list => new MoveRobotCommand(list);
            var commandParser = new CommandParser(commandMatcher, setBattleArenaCommandFactory, placeRobotCommandFactory,
                                                  moveRobotCommandFactory);
            Func<IRobot> robotFactory = () => new BattleRobot();
            var commandInvoker = new CommandInvoker(robotFactory);
            var reporter = new Reporter();
            return new ControlSystem(battleArena, commandParser, commandInvoker, reporter);
        }

        private static string GetAutomatedInstructions()
        {
            var commandStringBuilder = new StringBuilder();
            Console.WriteLine("Automated Instructions:");
            commandStringBuilder.AppendLine("5 5");
            commandStringBuilder.AppendLine("1 2 N");
            commandStringBuilder.AppendLine("LMLMLMLMM");
            commandStringBuilder.AppendLine("3 3 E");
            commandStringBuilder.Append("MMRMMRMRRM");
            return commandStringBuilder.ToString();
        }

        private static string GetInstructionsFromConsole()
        {
            var input = new StringBuilder();
            Console.WriteLine("Please enter your instructions ending with a blank line:");
            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                input.AppendLine(line);
                line = Console.ReadLine();
            }

            return input.ToString().TrimEnd('\n', '\r');
        }

        private static void Main(string[] args)
        {
            string instructions = string.Empty;
            var choice = DisplayMenu();
            switch (choice)
            {
                case "1":
                    instructions = GetAutomatedInstructions();
                    break;
                case "2":
                    instructions = GetInstructionsFromConsole();
                    break;
            }
            var controlSystem = ConstructControlSystem();
            controlSystem.Execute(instructions);
            var report = controlSystem.ProduceRobotsReport();
            Display(instructions, report);
        }

        private static string DisplayMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("----------------------");
            Console.WriteLine("1 : Automated Console");
            Console.WriteLine("2 : Manual Console");
            Console.WriteLine("----------------------");
            Console.Write("Please enter a number from above and press the <enter> key: ");

            string choice = Console.ReadLine();
            while (choice != "1" && choice != "2")
            {
                Console.WriteLine();
                Console.Write("Invalid menu choice, please try again: ");
                choice = Console.ReadLine();
            }

            return choice;
        }

        private static void Display(string instructions, string report)
        {
            Console.WriteLine("Input:");
            Console.WriteLine(instructions);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Output:");
            Console.WriteLine(report);
            Console.Write(Environment.NewLine);
            Console.Write("Press any key to exit...");
            Console.ReadLine();
        }
    }
}