using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RobotWars.Command.Parser
{
    public class CommandMatcher : ICommandMatcher
    {
        private readonly Dictionary<string, CommandType> _patternCommandMatchDictionary;

        public CommandMatcher()
        {
            _patternCommandMatchDictionary = new Dictionary<string, CommandType>
                {
                    //Matches: Integer number any number of times, white space, integer number any number of times.
                    {@"^\d+\s\d+$", CommandType.SetBattleArenaDimensions},
                    //Matches: Integer number any number of times, white space, integer number any number of times, white space, single char in (N,E,S,W).
                    {@"^\d+\s\d+\s[NESW]$", CommandType.PlaceRobot},
                    //Matches L,R,M any number of times
                    {@"^[LRM]+$", CommandType.MoveRobot}
                };
        }

        public CommandType Match(string inputLineToMatch)
        {
            try
            {
                KeyValuePair<string, CommandType> commandTypeMatch =
                    _patternCommandMatchDictionary.First(pair => new Regex(pair.Key).IsMatch(inputLineToMatch));
                return commandTypeMatch.Value;
            }
            catch (Exception ex)
            {
                string message = string.Format("Unable to match a command to the following input: '{0}'",
                                               inputLineToMatch);
                throw new MatchNotFoundException(message, ex);
            }
        }
    }
}