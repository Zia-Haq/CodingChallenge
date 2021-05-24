using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {

        [Test]
        public void SearchForNonExistentShirtTest()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                  new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.White},
                Sizes = new List<Size> { Size.Large }

            };

            var results = searchEngine.Search(searchOptions);

            Assert.IsTrue(results.Shirts.Count == 0); //Shirts count should be zero as no 'white - large' in the store

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }


        [Test]
        public void SearchWithNoOptionFilterTest()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                  new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                
            };

            var results = searchEngine.Search(searchOptions);

            Assert.IsTrue(results.Shirts.Count == 5); //Specifying no filter should return all the available shirts

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }


        [Test]
        public void SearchAgainstSizeOnlyOptionTest()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                  new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.IsTrue(results.Shirts.Count == 2); //should return 2 in total as only two large shirts available

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchAgainstColorOnlyOptionTest()
        {
            var shirts = new List<Shirt>
            {
                 new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                  new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.IsTrue(results.Shirts.Count == 3); //should return 3 items in total as only three red shirts available

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
      
        [Test]
        public void SearchAgainstBothColorAndSizeOptionTest()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                  new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red, Color.Blue},
                Sizes = new List<Size> {Size.Large}
            };

            var results = searchEngine.Search(searchOptions);

            //should return 2 items in total as only two red shirts available in Red and Blue color with Large size
            
            Assert.IsTrue(results.Shirts.Count == 2);
                       

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
    }
}
