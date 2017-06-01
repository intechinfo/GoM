This part contains GoM.Core.Immutable

GoM.Core.Immutable
==================

GoM.Core.Immutable part contains object wich can not be changed. They haven't constructors and every properties are Readonly. To create an immutable object, you have to use Object.Create(). It is this function whuch use a private constructor. Also, you can not change properties of an immutable object. So we implemented the Visitor Pattern to modify theses immutable objects.

How to use ?
==========

How to create an object ?
----------

To instantiate an object you must use Create function. 
For example :
```
Immutable.GitBranch gitBranch = Immutable.GitBranch.Create("My git branch", newBranchVersionInfo, projects);
```

The Create function will use a private constructor to instantiate a new GitBranch immutable.

How to modify an object ?
----------
To modify the branch name for example, you can not do ``` gitBranch.Name = "MyNewName" ``` because every properties are readonly.

You need to use 
```
GoMcontext.UpdateBranchName(GoMcontext.Repositories[0].Details, "My git branch", "MyNewName");
```

In fact, the API will instantiate a new object with the new information. If the object contains a lot of under objects we use the Visitor Pattern.

Visitor Pattern
--------

TODO : Explain how it works.


How to swith from mutable to immutable implementation ?
---------------

You got an extension on the context. So you can use : 
```ImmutableGoMContext immutableContext = MutableGoMContext.ToImmutable();```
And of course : 
```MutableGoMContext mutableContext = ImmutableGoMContext.ToMutable();```






