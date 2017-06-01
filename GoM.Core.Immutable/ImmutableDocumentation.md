Explication de l'organisation du modèle en général
Utilisation du modèle
Le tout en anglais...

This part contains GoM.Core.Abstractions, GoM.Core.Immutable, GoM.Core.Immutable.Tests, GoM.Core.Mutable, GoM.Core.Mutable.Tests

The files GoM.Core.X.Tests are the unit tests associated to the X part. We use XUnit with FluentAssertions.

GoM.Core.Abstractions contains every interfaces of our project. 

GoM.Core.Mutable fonctionality is obvious. It contains the basic model which can be changed. 

Conversely the GoM.Core.Immutable part contains object wich can not be changed. For example, they have not constructors. To create an immutable object, you have to use Object.Create(). It is this function whuch use a private constructor. Also, you can not change properties of an immutable object. So we implemented the Visitor Pattern to modify theses immutable objects.

GoM.Core.Abstractions
=====================

Theses are specials objects in our project. 
Some objects got Details which is a detailled implementation of themselves. 
For example. A IBasicGitBranch got a property Name and a property Details wich is a IGitBranch type. In IGitBranch object you have more *details* : the list of Projects and the Version. 