using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Task2
{
    [TestFixture]
    class LotteryTests
    {
        public static LotteryTests operator +(LotteryTests x, LotteryTests y)
        {
            return x+y;
        }

        [Test]
        public void check_amnt_of_no_to_gen()
        {
            var lot = new Lottery("Name", 50, 12, 5, 2);

            var count_to_draw = lot.main_to_draw + lot.bonus_to_draw;
            List<int> list = lot.generate_rand();

            Assert.That(list.Count, Is.Not.Null.And.
                EqualTo(count_to_draw));
        }

        [Test]
        public void list_in_asc_order_and_unique()
        {
            var lot = new Lottery("Name", 50, 12, 5, 2);
            List<int> list = lot.generate_rand();
            List<int> main_list = list.GetRange(0, lot.main_to_draw);
            List<int> bonus_list = list.GetRange(lot.main_to_draw, lot.bonus_to_draw);

            Assert.That(main_list, Is.Ordered.Ascending);
            Assert.That(main_list, Is.Unique);
            Assert.That(bonus_list, Is.Ordered.Ascending);
            Assert.That(bonus_list, Is.Unique);

        }

        [Test]
        public void all_list_items_in_range()
        {
            var lot = new Lottery("Name", 45, 0, 6, 0);
            Assert.That(lot.generate_rand(), Is.All.InRange(1, lot.main_balls));
        }

        [Test]
        public void check_constructor()
        {
            var lot = new Lottery("Name",50,12,5,2);

            Assert.That(lot.Name, Has.Length.GreaterThan(2));
            Assert.IsTrue(lot.main_balls == 50);
            Assert.IsTrue(lot.main_to_draw == 5);
            Assert.IsTrue(lot.bonus_balls == 12);
            Assert.IsTrue(lot.bonus_to_draw == 2);
        }
    }
}
