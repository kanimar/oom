using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Task2
{
    [TestFixture]
    class ScratchCardTests
    {
        [Test]
        public void is_different_balance_after_game()
        {
            var sc = new Scratch_card("Name");
            decimal init_balance = sc.balance;
            sc.play();
            decimal new_balance = sc.balance;

            Assert.AreNotEqual(init_balance, new_balance);
        }

        [Test]
        public void sum_no_more_than_three_times()
        {
            var sc = new Scratch_card("Name");
            List<int> list = sc.generate_rand();

            var gbi = list.GroupBy(i => i);
            foreach (var grp in gbi)
            {
                Assert.That(grp.Count(), Is.LessThanOrEqualTo(3));
            }
        }

        [Test]
        public void all_list_items_not_null()
        {
            var sc = new Scratch_card("Name");
            List<int> list = sc.generate_rand();
            Assert.That(list, Has.All.GreaterThan(0));
        }

        [Test]
        public void is_the_same_type()
        {
            IGambling sc = new Scratch_card("Name");
            Assert.IsTrue(sc.GetType().Equals(typeof(Scratch_card)));
        }
    }
}
