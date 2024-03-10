using CardSort.Models;
using System.Collections;

namespace CardSort.Tests
{
    public class MapManagerTests
    {
        [Theory]
        [ClassData(typeof(MapManagerTestData))]
        public void ClearMap(
            CardStack[,] map, 
            CardStack card, 
            Coord coord,
            CardStack[,] expectedResult)
        {
            var mapManager = new MapManager(map);
            mapManager.PlaceCard(card, coord);
            var result = mapManager.GetMap();
        }

        public class MapManagerTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                FirstMap.InitMap(),
                new CardStack() {Cards = new Card[] { new Card(Models.Enums.Color.Orange, 2, new Coord(0, 1)) } },
                new Coord(0, 1),
                FirstMap.InitMap()
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}