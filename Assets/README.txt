README:


	Pour commencer prenez le prefab cube dans Prefab > Tom > Level Design
Penssez bien à activer le Grid Snapping, en haut au milieu.créez
Quand vous créer de nouveaux block maiter les dans la Grille3d
Maintenant vous pouvez modifier vos niveaux facilement.
Notez bien pour creer des cubes spéciaux modifier le type.
Exception pour les cubes temporaires modifier manuellement le boolean Temporaire.
Dans le Manageur vous choisisser votre liste
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

==>pour le reste contentez-vous de copier coller dans la scène SceneDebut<==
et de vider la hiérarchie emphant de grille3d pour avoir une scène vierge

