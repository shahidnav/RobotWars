using System;
using System.Collections.Generic;
using RobotWars.Arena;

namespace RobotWars.Robot
{
    public class BattleRobot : IRobot
    {
        public Point Position { get; set; }
        public Heading Heading { get; set; }
        private bool _isPlaced;
        private readonly IDictionary<Movement, Action> _movementMethodDictionary;
        private readonly IDictionary<Heading, Action> _leftMoveDictionary;
        private readonly IDictionary<Heading, Action> _rightMoveDictionary;
        private readonly IDictionary<Heading, Action> _forwardMoveDictionary;

        public BattleRobot()
        {
            _movementMethodDictionary = new Dictionary<Movement, Action>
            {
                {Movement.Left, () => _leftMoveDictionary[Heading].Invoke()},
                {Movement.Right, () => _rightMoveDictionary[Heading].Invoke()},
                {Movement.Forward, () => _forwardMoveDictionary[Heading].Invoke()}
            };

            _leftMoveDictionary = new Dictionary<Heading, Action>
            {
                {Heading.North, () => Heading = Heading.West},
                {Heading.East, () => Heading = Heading.North},
                {Heading.South, () => Heading = Heading.East},
                {Heading.West, () => Heading = Heading.South}
            };

            _rightMoveDictionary = new Dictionary<Heading, Action>
            {
                {Heading.North, () => Heading = Heading.East},
                {Heading.East, () => Heading = Heading.South},
                {Heading.South, () => Heading = Heading.West},
                {Heading.West, () => Heading = Heading.North}
            };
            
            _forwardMoveDictionary = new Dictionary<Heading, Action>
            {
                {Heading.North, () => {Position = new Point(Position.X, Position.Y + 1);}},
                {Heading.East, () => {Position = new Point(Position.X + 1, Position.Y);}},
                {Heading.South, () => {Position = new Point(Position.X, Position.Y - 1);}},
                {Heading.West, () => {Position = new Point(Position.X - 1, Position.Y);}}
            };
        }

        public void Place(IBattleArena battleArena, Point point, Heading heading)
        {
            if (battleArena.IsValid(point))
            {
                Position = point;
                Heading = heading;
                _isPlaced = true;
                return;
            }

            ThrowPlaceException(battleArena, point);
        }

        public void Move(IEnumerable<Movement> movements)
        {
            foreach (var movement in movements)
            {
                _movementMethodDictionary[movement].Invoke();
            }
        }

        public bool IsPlaced()
        {
            return _isPlaced;
        }

        private static void ThrowPlaceException(IBattleArena battleArena, Point aPoint)
        {
            var dimension = battleArena.GetDimension();
            var exceptionMessage = String.Format("Place failed for point ({0},{1}). Battle arena dimension is {2} x {3}.",
                aPoint.X, aPoint.Y, dimension.Width, dimension.Length);
            throw new RobotPlaceException(exceptionMessage);
        }
    }
}
