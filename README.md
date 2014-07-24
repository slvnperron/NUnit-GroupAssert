NUnit-GroupAssert
=================

Adds group assertions to NUnit (similar to AssertJ's Soft Assertions)

Example
-------

```csharp
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

// OR

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
```

Will output:

```
Test failed because one or more assertions failed: 
1)	Expected: 10
	  But was:  20
	From Verify_GroupsExceptions at line 18

2)	Expected: 3
	  But was:  4
	From Verify_GroupsExceptions at line 20

3)	Expected: True
	  But was:  False
	From Verify_GroupsExceptions at line 21
```
