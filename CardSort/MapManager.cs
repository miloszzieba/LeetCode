using CardSort.Models;
using CardSort.Models.Moves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CardSort
{
    public class MapManager
    {
        private CardStack[,] _map { get; set; }
        private int _maxX { get; set; }
        private int _maxY { get; set; }

        public MapManager(CardStack[,] map)
        {
            this._map = map;
            this._maxX = _map.GetLength(0) - 1;
            this._maxY = _map.GetLength(1) - 1;
        }

        public CardStack[,] GetMap() => _map;

        public bool PlaceCard(CardStack stack, Coord target)
        {
            if (target.X < 0 || target.X < 0) return false;
            if (target.Y > _maxX || target.Y > _maxY) return false;
            if (_map[target.X, target.Y].Blocked) return false;
            if (_map[target.X, target.Y].Cards.Any()) return false;

            _map[target.X, target.Y] = stack;

            var moves = CalculateBestMoves(target);
            if (moves == null) return true;

            // ActOnMoves

            return true;
        }

        private List<Move>? CalculateBestMoves(Coord target)
        {
            var analyses = CalculatePotentialMoves(target);
            if (analyses == null) return null;

            var bestCourse = analyses.MaxBy(x => x.ReleasedFields);
            return bestCourse!.Moves;
        }

        private List<MapAnalysis>? CalculatePotentialMoves(Coord target)
        {
            var current = new MapAnalysis(_maxX, _maxY);
            var snake = FindSnake(ref current, target);
            if (snake == null || snake.Cards.Count() == 1) return null;
            return ClearSnake(ref current, snake);
        }

        private Snake? FindSnake(ref MapAnalysis current, Coord target)
        {
            var topCard = GetTopCard(ref current, target);
            if (topCard == null) return null;
            var snake = new Snake(topCard.Value);
            BuildSnake(ref snake, ref current, target.ToLeft());
            BuildSnake(ref snake, ref current, target.ToUp());
            BuildSnake(ref snake, ref current, target.ToRight());
            BuildSnake(ref snake, ref current, target.ToDown());

            return snake;
        }

        private void BuildSnake(
            ref Snake snake,
            ref MapAnalysis current,
            Coord target)
        {
            if (snake.Coords.Contains(target)) return;
            var topCard = GetTopCard(ref current, target);
            var match = topCard != null && topCard.Value.Color == snake.Color;
            if (!match) return;

            snake.AddCard(topCard!.Value);

            BuildSnake(ref snake, ref current, target.ToLeft());
            BuildSnake(ref snake, ref current, target.ToUp());
            BuildSnake(ref snake, ref current, target.ToRight());
            BuildSnake(ref snake, ref current, target.ToDown());
        }

        private List<MapAnalysis> ClearSnake(ref MapAnalysis current, Snake snake)
        {
            var coordinatesToCheck = new List<Coord>();
            var number = snake.Cards.Sum(x => x.Number);
            if (number >= 6)
            {
                current.Moves.Add(new BingoMove(snake.Coords));
                current.Bingos++;
                foreach (var coord in snake.Coords)
                {
                    current.FieldStates[coord.X, coord.Y].offset++;
                    current.FieldStates[coord.X, coord.Y].tempNumber = null;
                    var releasedField = _map[coord.X, coord.Y].Cards.Length == current.FieldStates[coord.X, coord.Y].offset;
                    current.ReleasedFields += releasedField.ToInt();
                }
                current.FieldsToCheck.AddUnique(snake.Coords);
                return CheckRemainingFields(ref current);
            }

            var analyses = new List<MapAnalysis>();
            // OPTIMIZE: Don't GetCopy of current for first element
            foreach (var card in snake.Cards)
            {
                var newAnalysis = current.GetCopy();
                newAnalysis.Moves.Add(new SquashMove(snake.Coords, card.Coord));
                newAnalysis.FieldStates[card.Coord.X, card.Coord.Y].tempNumber = number;
                var otherFields = snake.Coords.Where(x => x != card.Coord).ToList();
                foreach (var coord in otherFields)
                {
                    newAnalysis.FieldStates[coord.X, coord.Y].offset++;
                    newAnalysis.FieldStates[coord.X, coord.Y].tempNumber = null;
                    var releasedField = _map[coord.X, coord.Y].Cards.Length == newAnalysis.FieldStates[coord.X, coord.Y].offset;
                    newAnalysis.ReleasedFields += releasedField.ToInt();
                }
                newAnalysis.FieldsToCheck.AddUnique(otherFields);
                analyses.AddRange(CheckRemainingFields(ref newAnalysis));
            }
            return analyses;
        }

        private List<MapAnalysis> CheckRemainingFields(ref MapAnalysis current)
        {
            var snakes = new List<Snake>();
            while (current.FieldsToCheck.Any())
            {
                var coord = current.FieldsToCheck.First();
                var snake = FindSnake(ref current, coord);
                if (snake == null || snake.Cards.Count() == 1)
                {
                    current.FieldsToCheck.Remove(coord);
                }
                else
                {
                    current.FieldsToCheck = current.FieldsToCheck.Except(snake.Coords).ToList();
                    snakes.Add(snake);
                }
            }
            if (!snakes.Any()) return new List<MapAnalysis>() { current };
            if (snakes.Count == 1) return ClearSnake(ref current, snakes.First());

            var analyses = new List<MapAnalysis>();
            // OPTIMIZE: Don't GetCopy of current for first element
            foreach (var snake in snakes)
            {
                var newAnalysis = current.GetCopy();
                newAnalysis.FieldsToCheck = snakes.Where(x => x != snake).SelectMany(x => x.Coords).ToList();
                analyses.AddRange(ClearSnake(ref current, snake));
            }
            return analyses;
        }

        private Card? GetTopCard(
            ref readonly MapAnalysis current,
            Coord target)
        {
            if (target.X < 0 || target.Y < 0 || target.X > _maxX || target.Y > _maxY)
                return null;

            var stack = _map[target.X, target.Y];
            if (stack.Blocked) return null;
            if (!stack.Cards.Any()) return null;

            var fieldState = current.FieldStates[target.X, target.Y];
            if (stack.Cards.Length == fieldState.offset) return null;

            var topCard = stack.Cards[fieldState.offset];
            return fieldState.tempNumber == null
                ? topCard
                : topCard.WithNumber(fieldState.tempNumber!.Value);
        }
    }
}
