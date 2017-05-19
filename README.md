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

