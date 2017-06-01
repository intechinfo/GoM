# Communicator

Our feature was about to communicate with the git repositories and list the content of this repositories.   
We made the ICommunicator interface who describe the elements implemented in the Communicator class.    
---  
### Properties 
   
** string Source { get; } ** : 	
Return : * string * 	
Gets the link (path or url) from the repository    
** string ReposPath { get; } ** : 	
Return : * string *	
Gets the repository in which all the Git repositories are stock in Gom   
** Repository Repository { get; } ** : Repository instance;   
** Path { get; set; } ** : 	
Return : string 	
Repository’s path;  	
** Url { get; } ** : 
Return : Uri 	
Initiates and get Url grom the repository if the URL is on the net;      
	
## Methods 		 
** isRepository() ** : 		
Return : bool 	
Checks if the source is a Git repository     
** loadRepository() ** : 	
Return : * Repository *	
Gets the Git repository from the source   
**  getFiles(string searchPattern = "*") ** : 	
Parameter : * string , contains a filter on the name or extension *		
Return : * List<string> * 	
Gets and returns all the files from the repository.   
*Can also take a filter* (string searchPattern = « * ») to return only the files who are similar to the filter    
** getFolders(string searchPattern = "*") **  :		
Parameter : * string , contains a filter on the name or extension *		
Return : * List<string>	*	
Gets all the subfolders from the current folder. Uses a regex to filter 
Can also get a filter parameter to get a choosen repository		
**  getBasicGitRepository() ** : 	
Returns : * BasicGitRepository * 	
Convert the current git repository in object BasicGitRepository;   	
** getAllBranches() ** : 
Return : * List<BasicGitBranch> *
Returns the list of the branches from a repository ;	
** getCommitAncestor(Commit commit) ** : 
Return : * List<Commit> *	
Parameter : * Commit * 	
get the list of the parents from a commit;	
Parameters : * Commit * 	
Return : * List from parents * 		
** MostRecentVersion() **: 	
Return : * VersionTag * 	
Returns the last project version in the repository, there can be much projects in the repository 	
convertBranchToGitBranch(Branch branch); : convert the branches object in git branches.		
Return : * BasicGitBranch * 	
Parameters : * GoM branch * 	
Return Branch version from Git 	

## GitFileProvider
	
This API is made so the user can freely move beetwen branches and commits in a Git repository. He can also access File (readonly). 	
	
--- 
	
### Methods	
** GitFileProvider **	
Get the git repository from the given path 	
Param : * string path => * 	
	Example : "C:\Dev\GoM"
Return not found if there is no .git 	
	
** GetDirectoryContent **	
Param : * (string subpath)  *	
Return : * IDirectoryContents * 	
Exception : * NotFoundDirectoryContents.Singleton if not found *	
Open a precise path, from one of this type :	
Root (empty) (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);	
            var rootDir = git.GetDirectoryContents("");) )	

* Branches (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);		
            var rootDir = git.GetDirectoryContents(@"branches\origin/perso-KKKMPT\GoM.GitFileProvider"); )	
	
* Tags (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);	
            var rootDir = git.GetDirectoryContents(@"tags\V1.0.0\GoM.GitFileProvider"); )	
	
* Commit (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);		
            var rootDir = git.GetDirectoryContents(@"commits\1921471fd36db781bef6833b4723f34afccd8d71\GoM.GitFileProvider"); )	
	
If only the type is given, return all elements of this type. 		
	Example : (@"branches") will return every branches of the repository. 	

** GetFileInfo **  	
Param : * string subpath * 	
Return : * FileInfoFile *	
Get a specific file from a subpath. 	
	
*  Branches (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);	
            IFileInfo fileInBranchGuillaume = git.GetFileInfo (@"branches\origin/perso-KKKMPT\GoM.GitFileProvider\app.config "); )	
	
* Tags (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);	
            IFileInfo fileInBranchGuillaume = git.GetFileInfo (@"tags\V1.0.0\GoM.GitFileProvider\app.config "); )	
	
* Commit (Exemple : GitFileProvider git = new GitFileProvider(ProjectRootPath);		
            IFileInfo fileInBranchGuillaume = git.GetFileInfo (@"commits\1921471fd36db781bef6833b4723f34afccd8d71\GoM.GitFileProvider\app.config "); )	
