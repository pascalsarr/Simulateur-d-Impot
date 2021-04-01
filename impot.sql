-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  mer. 27 nov. 2019 à 02:15
-- Version du serveur :  5.7.26
-- Version de PHP :  7.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `impot`
--

-- --------------------------------------------------------

--
-- Structure de la table `employes`
--

DROP TABLE IF EXISTS `employes`;
CREATE TABLE IF NOT EXISTS `employes` (
  `Nom` varchar(30) NOT NULL,
  `Prenom` varchar(30) NOT NULL,
  `Matricule` varchar(255) NOT NULL,
  `SalaireBrut` int(30) NOT NULL,
  `NbreJours` int(30) NOT NULL,
  `Conjoint` int(30) NOT NULL,
  `NbreEnfant` int(30) NOT NULL,
  PRIMARY KEY (`Matricule`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `employes`
--

INSERT INTO `employes` (`Nom`, `Prenom`, `Matricule`, `SalaireBrut`, `NbreJours`, `Conjoint`, `NbreEnfant`) VALUES
('Fall', 'Aicha', 'aasxq1grg2', 1000000, 30, 1, 5),
('asss', 'ass', 'ajs88', 300000, 30, 1, 3),
('Ndiaye ', 'Modou', 'wessex', 295000, 25, 0, 2),
('beye', 'samba', 'zaawwww', 68000, 30, 0, 0),
('aaaa', 'zazza', 'zzaa', 99999, 20, 1, 3);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
