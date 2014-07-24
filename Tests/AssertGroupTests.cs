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
            var group = new AssertGroup();
            group.Add(() => Assert.AreEqual(10, 20));
            group.Add(() => Assert.AreEqual(1, 1));
            group.Add(() => Assert.AreEqual(3, 4));
            group.Add(() => Assert.IsTrue(1 > 3));
            group.Verify();
        }

        [Test]
        public void Vertify_DoesNotThrow_WhenNoException()
        {
            var group = new AssertGroup();
            group.Add(() => Assert.AreEqual(1, 1));
            group.Add(() => Assert.AreEqual(2, 2));
            group.Verify();
        }
    }
}
