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