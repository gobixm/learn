using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Tests
{
    internal class LinqTest
    {
        [Test]
        public void ShouldParallelForAllEnumerable()
        {
            var threadIds = new int[100];
            ParallelEnumerable.Range(0, 100)
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .ForAll(i => threadIds[i] = Thread.CurrentThread.ManagedThreadId);
            Assert.AreEqual(Environment.ProcessorCount, threadIds.Distinct().Count());
        }

        [Test]
        public void ShouldJoinAnonimous()
        {
            var texts = Enumerable.Range(0, 10)
                .Select(i => new {Id = i, Text = "text" + i}).ToList();
            var descriptions = Enumerable.Range(0, 10)
                .Select(i => new {Id = i, Description = "descriptions" + i}).ToList();
            var joined = texts.Join(descriptions, x => x.Id, y => y.Id,
                (x, y) => new {x.Id, x.Text, y.Description}).ToList();
            Assert.AreEqual(texts.Count, joined.Count);
            Assert.AreEqual(joined.Count,
                joined.Count(
                    x =>
                        x.Text == texts.First(y => y.Id == x.Id).Text &&
                        x.Description == descriptions.First(y => y.Id == x.Id).Description));
        }

        [Test]
        public void ShouldLeftJoinAnonimous()
        {
            var items = new[]
            {
                new {Id = 1, Name = "Item1"},
                new {Id = 2, Name = "Item2"},
                new {Id = 3, Name = "Item3"},
                new {Id = 4, Name = "Item4"},
                new {Id = 5, Name = "Item5"}
            };

            var groups = new[]
            {
                new {Id = 1, ItemId = 1, Name = "Group1"},
                new {Id = 2, ItemId = 1, Name = "Group2"},
                new {Id = 3, ItemId = 2, Name = "Group3"}
            };

            var itemGroups = items.GroupJoin(groups, x => x.Id, y => y.ItemId, (x, y) => new {Item = x, Groups = y})
                .SelectMany(result => result.Groups.DefaultIfEmpty(), (result, group) => new
                {
                    Item = result.Item.Name,
                    Group = (group == null) ? "<none>" : group.Name
                }).ToList();
            var item2 = itemGroups.First(x => x.Item == "Item2");
            Assert.AreEqual("Group3", item2.Group);

            var item5 = itemGroups.First(x => x.Item == "Item5");
            Assert.AreEqual("<none>", item5.Group);
        }

        [Test]
        public void ShoudSelectIn()
        {
            var list = new[] {1, 2, 3, 4, 5};
            var inClause = new[] {1, 5};
            var resultList = list.Join(inClause, x => x, y => y, (x, y) => x);
            Assert.IsTrue(inClause.SequenceEqual(resultList));
        }
    }
}