USE [master]
GO

CREATE DATABASE [db]
 
GO
USE [db]
GO


CREATE TABLE categories(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [nom] VARCHAR(255) NOT NULL UNIQUE,
);


CREATE TABLE marques(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [nom] VARCHAR(255) NOT NULL UNIQUE,
    [photo] VARBINARY(max) NOT NULL,
);

CREATE TABLE produits(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [nom] VARCHAR(255) NOT NULL UNIQUE,
    [prix] VARCHAR(255) NOT NULL,
    [description] TEXT NOT NULL,
    [quantity] INT NOT NULL,
    [etat] VARCHAR(255) NOT NUll,
    [statut] VARCHAR(255) NOT NUll,
    [produit_date] DATETIME NOT NULL,
    [categorie_id] INT NOT NULL,
    [marque_id] INT NOT NULL,
    FOREIGN KEY (categorie_id) REFERENCES categories(id) ON DELETE CASCADE,
    FOREIGN KEY (marque_id) REFERENCES marques(id) ON DELETE CASCADE,
    check(statut in ('en stock', 'rupture de stock') AND etat in ('neuf', 'occasion') AND quantity >= 0),
);

CREATE TABLE produit_photos(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [photo] VARBINARY(max) NOT NULL,
    [produit_id] INT NOT NULL,
    FOREIGN KEY (produit_id) references produits(id) ON DELETE CASCADE
);

CREATE TABLE reductions(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [nom] VARCHAR(225) NOT NULL UNIQUE,
    [reduction_montant] INT NOT NULL,
);

CREATE TABLE reduction_produits(

    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [produit_id] INT NOT NULL,  
    [reduction_id] INT NOT NULL,
    [date_debut] DATE NOT NULL,
    [date_fin] DATE NOT NULL,
    FOREIGN KEY (produit_id) REFERENCES produits(id) ON DELETE CASCADE,
    FOREIGN KEY (reduction_id) REFERENCES reductions(id) ON DELETE CASCADE
);


CREATE TABLE clients(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [nom] VARCHAR(255) NOT NULL,
    [prenom] VARCHAR(255) NOT NULL,
    [email] VARCHAR(255) NOT NULL UNIQUE,
    [telephone] VARCHAR(255) UNIQUE,
    [adresse] VARCHAR(255) NOT NULL,
    [mot_de_passe] VARCHAR(225) NOT NULL,
    [uuid] VARCHAR(225) NOT NULL,
);

CREATE TABLE produit_ratings(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [text] TEXT,
    [rating] INT NOT NULL,
    [date] DATETIME NOT NULL,
    [produit_id] INT NOT NULL,
    [client_id] INT NOT NULL,
    FOREIGN KEY (produit_id) REFERENCES produits(id) ON DELETE CASCADE,
    FOREIGN KEY (client_id) REFERENCES clients(id) ON DELETE CASCADE,
    CHECK (rating BETWEEN 0 AND 5)
);

CREATE TABLE feedbacks(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [full_name] VARCHAR(225) NOT NULL,
    [email] VARCHAR(225) NOT NULL,
    [subject] VARCHAR(225) NOT NULL,
    [message] VARCHAR(225) NOT NULL
);

CREATE TABLE orders(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [client_uuid] VARCHAR(225) NOT NULL,
    [order_date] DATE NOT NULL,

);

CREATE TABLE order_items(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [quantity] INT NOT NULL,
    [list_prix] FLOAT NOT NULL,
    [discount] INT NOT NULL,
    [order_id] INT NULL,
    [produit_id] INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
    FOREIGN KEY (produit_id) REFERENCES produits(id) ON DELETE CASCADE
);
