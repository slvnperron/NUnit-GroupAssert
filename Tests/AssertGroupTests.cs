namespace Tests
{
    #region

    using NUnit.Framework;

    using Nunit_GroupAssert;

    #endregion

    [TestFixture]
    public class AssertGroupTests
    {
        [Test]
        public void Verify_GroupsExceptions()
        {
            // Verifies on disposal
            using (var group = new AssertGroup())
            {
                group.Add(() => Assert.AreEqual(10, 20));
                group.Add(() => Assert.AreEqual(1, 1));
                group.Add(() => Assert.AreEqual(3, 4));
                group.Add(() => Assert.IsTrue(1 > 3));
            }
        }

        [Test]
        public void Verify_DoesNotThrow_WhenNoException()
        {
            var group = new AssertGroup();
            group.Add(() => Assert.AreEqual(1, 1));
            group.Add(() => Assert.AreEqual(2, 2));
            group.Verify();
        }

        internal class Obj
        {
            public int Value { get; set; }

            public Obj(int Value)
            {
                this.Value = Value;
            }
        }

        [Test]
        public void Verify_ThatThow_InPlace()
        {
            Obj v = new Obj(10);

            var group = new AssertGroup();
            group.Add(() => Assert.AreEqual(v.Value, 11, "Should fail test"));

            v.Value = 10;

            group.Verify();
        }
    }
}
