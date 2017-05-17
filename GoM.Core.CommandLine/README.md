# GoM command-line documentation

## gom Command

Syntax :        gom [option]<br/>
Help option :   -h<br/>
Description :   Affiche les différentes commandes disponibles.<br/>

## init Command

Syntax :        gom init [option]<br/>
Help option :   -h<br/>
Description :   Cette commande initialise un nouveau repository GoM dans le répertoire courant.<br/>
Exceptions :    Si le répertoire courant ou l'un de ses fils est déjà un repository, l'initialisation échoue.

## files Command

Syntax :        gom files [option] [path]<br/>
Help option :   -h<br/>
Description :   Liste les dossiers et fichiers contenus dans le répertoire correspondant au [path] spécifié.<br/>
Exceptions :    
* Si aucun [path] n'est spécifié, le contenu du répertoire courant est listé.
* Si le [path] spécifié n'existe pas, un message d'erreur est affiché.

## add Command

Syntax :        gom add [option] [path]<br/>
Help option :   -h<br/>
Options :       
* [-r|--repository]
* [-p|--project]
* [-p -all|--project -all]
* [-b|--branch]<br/>
Description :   <br/>
Exceptions :    Si aucun [path] n'est spécifié, une erreur est affiché ainsi que l'aide sur la commande.

## fetch Command

Syntax :        gom fetch [option] [path]<br/>
Help option :   -h<br/>
Options :       
* [-r|--repository]
* [-p|--project]
* [-p -all|--project -all]
* [-b|--branch]<br/>
Description :   Vérifie sur la branche distante spécifié dans le [path] si des modifications ont été apportée et les télécharge.<br/>
Exceptions :    Si aucun [path] n'est spécifié, une erreur est affiché ainsi que l'aide sur la commande.
