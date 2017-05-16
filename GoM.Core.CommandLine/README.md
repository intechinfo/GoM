# GoM command-line documentation

## gom Command

Syntax :        gom [option]
Help option :   -h
Description :   Affiche les différentes commandes disponibles.

## init Command

Syntax :        gom init [option]
Help option :   -h
Description :   Cette commande initialise un nouveau repository GoM dans le répertoire courant.
Exceptions :    Si le répertoire courant ou l'un de ses fils est déjà un repository, l'initialisation échoue.

## files Command

Syntax :        gom files [option] [path]
Help option :   -h
Description :   Liste les dossiers et fichiers contenus dans le répertoire correspondant au [path] spécifié.
Exceptions :    * Si aucun [path] n'est spécifié, le contenu du répertoire courant est listé.
                * Si le [path] spécifié n'existe pas, un message d'erreur est affiché.

## add Command

Syntax :        gom add [option] [path]
Help option :   -h
Options :       * [-r|--repository]
                * [-p|--project]
                * [-p -all|--project -all]
                * [-b|--branch]
Description :   
Exceptions :    Si aucun [path] n'est spécifié, une erreur est affiché ainsi que l'aide sur la commande.

## fetch Command

Syntax :        gom fetch [option] [path]
Help option :   -h
Options :       * [-r|--repository]
                * [-p|--project]
                * [-p -all|--project -all]
                * [-b|--branch]
Description :   Vérifie sur la branche distante spécifié dans le [path] si des modifications ont été apportée et les télécharge.
Exceptions :    Si aucun [path] n'est spécifié, une erreur est affiché ainsi que l'aide sur la commande.
