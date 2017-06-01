# GoM command-line documentation

## gom Command

Syntax :        gom [option]<br/>
Help option :   -h<br/>
Description :   Displays available commands.<br/>

## init Command

Syntax :        gom init [option]<br/>
Help option :   -h<br/>
Description :   Initialize a new GoM repository in the current directory.<br/>
Exceptions :    If the current directory or one of his child is already a repository, the initialisation fail.

## files Command

Syntax :        gom files [option] [path]<br/>
Help option :   -h<br/>
Description :   List directories and files contained in the directory specified in [path].<br/>
Exceptions :    
* If no [path] is specified, current directory content is listed.
* If [path] doesn't exist, an error message is displayed.

## add Command

Syntax :        gom add [option] [path]<br/>
Help option :   -h<br/>
Options :       
* [-r|--repository]
* [-p|--project]
* [-p -all|--project -all]
* [-b|--branch]<br/>
Description :   Add repository, branch and/or project in GoM context.<br/>
Exceptions :    If no [path] is specified, an error is displayed with the command help.

## remove Command

Syntax :        gom remove [option] [path]<br/>
Help option :   -h<br/>
Options :       
* [-r|--repository]
* [-p|--project]
* [-p -all|--project -all]
* [-b|--branch]<br/>
Description :   Remove repository, branch and/or project from GoM context.<br/>
Exceptions :    If no [path] is specified, an error is displayed with the command help.

## fetch Command

Syntax :        gom fetch [option] [path]<br/>
Help option :   -h<br/>
Options :       
* [-r|--repository]
* [-p|--project]
* [-p -all|--project -all]
* [-b|--branch]<br/>
Description :   Check on the specified remote branch in [path] if there are modifications and download them.<br/>
Exceptions :    If no [path] is specified, an error is displayed with the command help.
