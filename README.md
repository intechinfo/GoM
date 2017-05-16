Class ReadRepository

Constructeur : 

public ReadRepository(String uri)
Le constructeur prend en paramètre le chemin du repository

-----------------------------------------------------------

Méthodes :

public bool gitRepositoryExist()
Cette méthode permet de vérifier l'existence d'un repository git 
Valeurs de retour : true -> si le repository git existe
	            false -> si le repository git n'existe pas


public Branch getBranch(String name)
Cette méthode permet de récupérer une branche à partir de son name (param d'entrée)
Valeur de retour : null -> si aucune branche n'est trouvée
                   ou la branche trouvée


private List<Branch> getAllBranch()
Elle permet de récupérer toutes les branches du repository courant
Valeur de retour : null -> si le repository ne comprend aucune branche
                   ou List composée de branches


public BranchVersionInfo getBranchVersionInfo(String branchName)
Elle permet d'obtenir la version d'une branche à partir de son name (param d'entrée)
Valeur de retour : null -> si la branche n'a pas été trouvé
                   ou BranchVersionInfo de la branche trouvée


public List<Commit> getCommitAncestor(Commit commit)
Méthode permettant de récupérer tous les parents d'un commit
Valeur de retour : null ->si aucun ancestre n'a été trouvé
                   ou List composée de commit


public List<GitBranch> getListGitBranch()
Méthode permettant de récupérer toutes les branches git du repository
Valeur de retour : null -> le repository ne possède aucune branche
                   ou List de GitBranch trouvée



----------------------------------------------------------------------

Package : 
  libgit2sharp

Project : 
  GoM.Core.Extract

Test : 
  GoM.Core.Extract.Tests




