using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2
{
    [TestFixture]
    class Tests
    {

        public static Tests operator +(Tests x, Tests y)
        {
            return x+y;
        }

        [Test]
        public void sum_no_more_than_three_times()
        {
            var sc = new Scratch_card("Name");
            List<int> list=sc.generate_rand();

            var gbi = list.GroupBy(i => i);
            foreach (var grp in gbi)
            {
                Assert.That(grp.Count(), Is.LessThanOrEqualTo(3));
            }
        }

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

        [Test]
        public void no_lottery_without_main_to_draw()
        {
            Assert.Catch(()=>
            {
                var lot = new Lottery("Name", 0, 12, 0, 2);
            }
        );
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

        [Test]
        public void check_amnt_of_no_to_gen()
        {
            var lot = new Lottery("Name", 50, 12, 5, 2);

            var count_to_draw = lot.main_to_draw + lot.bonus_to_draw;
            List<int> list = lot.generate_rand();

            Assert.That(list.Count, Is.Not.Null.And.
                EqualTo(count_to_draw));
        }
        
       
    }
}
