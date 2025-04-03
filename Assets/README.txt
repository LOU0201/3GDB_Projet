README:

	Aller dans Scenes->SceneDebut
	Pour commencer prenez le prefab cube dans Prefab > Tom > Level Design
Penssez bien à activer le Grid Snapping, en haut au milieu.créez
Quand vous créer de nouveaux block maiter les dans la Grille3d
Maintenant vous pouvez modifier vos niveaux facilement.
Notez bien pour creer des cubes spéciaux modifier le type.
Exception pour les cubes temporaires modifier manuellement le boolean Temporaire.
Dans le Manageur vous choisisser votre liste
pour que les block créer par le joueur soit bloquant (jaune) aller dans Grille3d et cocher BlockJaune
	Si vous avez des bugs ou des incompréentions, n'ésitez surtout pas à nous faire signe !!!
	Ou encore des idées d'améliorations !!!




Grille_3d:
	Joueur: Joueur 1
	Prefab Boite: Boite
	Non_Blockeur : True
	Des: Null
	Reset Tom: Null
	Prefab Cube Rouge: Null
	ListeTom:Manager

CAMERA:
	Copier/coller la caméra dans New Scenen 2

Manager:
	Liste : La liste que vous vouler implémenter:{rien, trou, cube}
	Current Index : 0
	G3D : grille3d
	Joueur : Joueur

Joueur:
	Liste:Manager
	Update-grille 3d:Grille3d

Upcoming Spawn Icons : à ignorer, cherche dans Lou prefab folder --> "display" coller dedans pour qu'il marche normalement 

POUR L'UI, UTILISER LE CROIX ROUGE!!!

pour le reste contentez-vous de copier coller dans la scène SceneDebut
et de vider la hiérarchie emphant de grille3d pour avoir une scène vierge

Pour créer un niveau, copier-coller la scène SceneDebut. Commencer la création d'un nouveau niveau en glissant un préfab Boite dans
l'empty Grille3d. Régler le bloc d'entrée sur Debut et de sortie sur Fin. Une fois le niveau créé, ajouter un empty, l'appeler "centre"
et le positionner au milieu des axes x, y et z du niveau. L'assigner, ensuite, comme valeur des variables Target du préfab Camera et
Player Transform du script Camera Turn du Text (TMP) de l'intro. Vous pouvez ajuster la position de "centre" si vous ne la considérez
pas comme correcte. Puis, régler le zoom de la caméra (variable Distance from Target du préfab Camera) à une valeur permettant d'avoir
une bonne vue du niveau. Enfin, assigner la Grille3d au G3D du Manager et les valeurs 3 à distance et 10 à speed de la Capsule.

Dans le ResetTom, Il faut remplir les références des Textes. Pour cela, allez dans l'objet "DisplayNext Variant", puis dans Score,
prendre les enfants Score Text, Collectible Text et Return Text et les mettres comme valeurs des 3 Score_Text du ResetTom du bloc de
Début, dans ce même ordre.

Pour les niveaux possédant un nombre minimum et maximum de sorties, entrer le nombre minimum dans Minsortie et maximum dans Maxsortie.
Pour ceux n'ayant pas de minimum de sorties, rentrer le nombre de sortie dans Maxsortie et -1 dans Minsortie.
Si le niveau possède un collectible, le référencer dans Collec.
Activer Annule si le niveau comporte le challenge de ne pas utiliser le retour-arrière.