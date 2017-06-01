This part contains GoM.Core.Immutable

GoM.Core.Immutable
==================

GoM.Core.Immutable part contains object wich can not be changed. For example, they have not constructors. To create an immutable object, you have to use Object.Create(). It is this function whuch use a private constructor. Also, you can not change properties of an immutable object. So we implemented the Visitor Pattern to modify theses immutable objects.
