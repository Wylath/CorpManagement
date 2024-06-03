USE [DBCorpManagement]
GO

DELETE FROM [gradepoint];
INSERT INTO [gradepoint] ([id] ,[name] ,[totalpoint]) VALUES
(1,'Grade global',31600),
(2,'Grade secondaire',9760),
(3,'Autres',0);

DELETE FROM [profilelevel];
INSERT INTO [profilelevel] ([id], [name]) VALUES
(1, 'Administration'),
(2, 'All=S'),
(3, 'All=M'),
(4, 'All=A'),
(5, 'All=V'),
(6, 'Nothing'),
(7, 'Utilisateurs=V, Service=V, Vehicule=V, Pneu=V, Article=V, Facture=V, Compagnie=V, Assurance=V');

DELETE FROM [invoice_type];
INSERT INTO [invoice_type] ([id], [name]) VALUES
(1,'Assurance'),
(2,'Commande d''article'),
(3,'Entretien');

DELETE FROM [provider_type];
INSERT INTO [provider_type] ([id], [name]) VALUES
(1,'Garage'),
(2,'Assurance'),
(3,'March� principale'),
(4,'March� secondaire'),
(5,'Pompe � essence'),
(6,'Carwash'),
(7,'Contr�le technique');

DELETE FROM [status_vehicle];
INSERT INTO [status_vehicle] ([id], [name]) VALUES
(1,'Neuf'),
(2,'Normal'),
(3,'Mauvais �tat'),
(4,'D�class�'),
(5,'Vendu');

DELETE FROM [type_article];
INSERT INTO [type_article] ([id], [name]) VALUES
(1,'Habillement'),
(2,'Papeterie'),
(3,'Article pour voiture'),
(4,'Informatique'),
(5,'Radio'),
(6,'T�l�phone'),
(7,'Equipement');

DELETE FROM [type_servicing];
INSERT INTO [type_servicing] ([id], [name]) VALUES
(1,'R�paration'),
(2,'Petit entretien'),
(3,'Gros entretien'),
(4,'Contr�le technique'),
(5,'Carburant'),
(6,'Pneus : Crevaison'),
(7,'Pneus : Permutations'),
(8,'Pneus : Usur'),
(9,'Pneus : Autres'),
(10,'Nettoyage'),
(11,'Achat'),
(12,'Vente'),
(13,'Autres');

DELETE FROM [order_status_type];
INSERT INTO [order_status_type] ([id], [name]) VALUES
(1,'Command�'),
(2,'En cours'),
(3,'Re�u'),
(4,'Annul�');

DELETE FROM [fuel];
INSERT INTO [fuel] ([id], [name]) VALUES
(1,'Diesel'),
(2,'Essence'),
(3,'Electrique'),
(4,'Autre');

DELETE FROM [size_clothing];
INSERT INTO [size_clothing] ([id], [size]) VALUES
-- haut
(1,'XXS'),
(2,'XS'),
(3,'S'),
(4,'M'),
(5,'L'),
(6,'XL'),
(7,'XXL'),
(8,'XXXL'),
-- chaussure & pantalon
(9,'34'),
(10,'35'),
(11,'35.5'),
(12,'36'),
(13,'36.5'),
(14,'37'),
(15,'37.5'),
(16,'38'),
(17,'38.5'),
(18,'39'),
(19,'39.5'),
(20,'40'),
(21,'40.5'),
(22,'41'),
(23,'41.5'),
(24,'42'),
(25,'42.5'),
(26,'43'),
(27,'43.5'),
(28,'44'),
(29,'44.5'),
(30,'45'),
(31,'45.5'),
(32,'46'),
(33,'46.5'),
(34,'47'),
(35,'47.5'),
(36,'48'),
(37,'48.5'),
(38,'49'),
(39,'49.5'),
(40,'50'),
(41,'52'),
(42,'54'),
(43,'56'),
(44,'58'),
(45,'60'),
(46,'62'),
(47,'64');

DELETE FROM [state];
INSERT INTO [state] ([id], [name]) VALUES
(1,'Neuf'),
(2,'Normal'),
(3,'Us�'),
(4,'Crev�'),
(5,'Autre');

DELETE FROM [police_locality];
INSERT INTO [police_locality] ([id], [name]) VALUES
(1,'Locality 1'),
(2,'Locality 2');

DELETE FROM [state_article_att];
INSERT INTO [state_article_att] ([id], [name]) VALUES
(1,'Neuf'),
(2,'Normal'),
(3,'Us�'),
(4,'Cass�'),
(5,'En r�paration'),
(6,'Vendu'),
(7,'Perdu');