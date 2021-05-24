using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly Dictionary<(Guid color, Guid size), List<Shirt>> _colorSizeList;
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
            //Our search is based on color and sizes, we can build a dictionary on these two options for quick retrieval of results
            _colorSizeList = shirts?.GroupBy(s => (s.Color.Id, s.Size.Id)).ToDictionary(grp => grp.Key, grp => grp.ToList());
        }

        /// <summary>
        /// Builds all possible keys/combinations for passed in search options, it will create keys based on color and size filter
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private IEnumerable<(Guid color, Guid size)> GetSearchKeys(SearchOptions options)
        {
            var colors = options?.Colors?.Count > 0 ? options.Colors : Color.All;
            var sizes = options?.Sizes?.Count > 0 ? options.Sizes : Size.All;

            return from c in colors
                   from s in sizes
                   select (c.Id, s.Id);


        }

        public SearchResults Search(SearchOptions options)
        {
            var lstShirts = new List<Shirt>();
            var colorCounts = Color.All.Select(c => new ColorCount() { Color = c, Count = 0 }).ToList();
            var sizeCounts = Size.All.Select(s => new SizeCount() { Size = s, Count = 0 }).ToList();


            //against the search color and/or size options get the matching shirts and update the color and size counters
            foreach (var key in GetSearchKeys(options))
            {
                if (_colorSizeList.TryGetValue(key, out List<Shirt> shirts))
                {
                    lstShirts.AddRange(shirts);
                    var colorCount = colorCounts.First(i => i.Color.Id == key.color);
                    colorCount.Count += shirts.Count;
                    var sizeCount = sizeCounts.First(i => i.Size.Id == key.size);
                    sizeCount.Count += shirts.Count;
                }
            }

            return new SearchResults
            {
                ColorCounts = colorCounts,
                Shirts = lstShirts,
                SizeCounts = sizeCounts
            };
        }
    }
}