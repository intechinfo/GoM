# GoM Persistence

GoM's persistence system, file-based.

---

## Description
	
Part of the GoM core, permitting to keep the GoM informations related to the environment defined by the user. 


## Synopsis


The _Gom.Core.Persistence_ package works on two main parts: 
	
	
- The loading part: Will try to load and parse an existing `.xml` file from the given `.gom` folder.

- The saving part:  Will save all the informations needed to keep the created environment in the `.gom` folder. 

( For more documentations details, see the _IPersistence.cs_ file. ) 

	
	
The  `.gom` folder contains an `.xml` file in which all the datas concerning the environment are written, thanks to built-in `ToXML()` functions ( see the _Persistence.cs_ file ).



The XML format choice have been made for its clearity and its lightness, and its easyness to be used in a functional programming.
(also because the `newtonsoft.json` library is not already flexible enough for it).

	


## Tests
	
Few tests have been developped and can be found in _/Tests/GoM.Core.Persistence.Tests/_

## Dependencies and other informations 
	
 - Using __netcoreapp 1.1__
	

---

### Credits:


- ROUSSEL Kevin

- RICHARD Emeric

- ELBAZ Amram
