-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: 2020 m. Geg 23 d. 21:47
-- Server version: 10.4.8-MariaDB
-- PHP Version: 7.3.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `melv`
--

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `administrators`
--

CREATE TABLE `administrators` (
  `fk_user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `administrators`
--

INSERT INTO `administrators` (`fk_user`) VALUES
(2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `clients`
--

CREATE TABLE `clients` (
  `fk_user` int(11) NOT NULL,
  `birthdate` date NOT NULL,
  `personal_no` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `clients`
--

INSERT INTO `clients` (`fk_user`, `birthdate`, `personal_no`) VALUES
(1, '1998-11-14', '37000000000'),
(3, '2020-05-04', '37000000001'),
(4, '2020-05-13', '37000000002'),
(5, '2020-05-22', '37000000003'),
(6, '2020-05-12', '37000000004'),
(7, '2020-05-18', '37000000005'),
(8, '2020-05-06', '37000000006'),
(9, '2020-05-12', '37000000007'),
(10, '2020-05-15', '37000000008'),
(11, '2020-05-20', '37000000009'),
(12, '2020-05-13', '37000000010');

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `entertainment_plans`
--

CREATE TABLE `entertainment_plans` (
  `id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `text` text NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `removed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `entertainment_plans`
--

INSERT INTO `entertainment_plans` (`id`, `title`, `text`, `price`, `removed`, `fk_administrator`) VALUES
(1, 'test', 'test', '1000.00', 0, 2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `flights`
--

CREATE TABLE `flights` (
  `id` int(11) NOT NULL,
  `direction` tinyint(1) NOT NULL,
  `departure_date` date NOT NULL,
  `arrival_date` date NOT NULL,
  `confirmed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `flights`
--

INSERT INTO `flights` (`id`, `direction`, `departure_date`, `arrival_date`, `confirmed`, `fk_administrator`) VALUES
(6, 0, '2020-05-13', '2020-06-12', 0, 2),
(7, 0, '2020-05-01', '2020-05-31', 0, 2),
(8, 0, '2020-05-30', '2020-06-29', 0, 2),
(9, 0, '2020-05-25', '2020-06-24', 0, 2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `flight_durations`
--

CREATE TABLE `flight_durations` (
  `id` int(11) NOT NULL,
  `date_mars` date NOT NULL,
  `date_earth` date NOT NULL,
  `direction` tinyint(1) NOT NULL,
  `price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `flight_requests`
--

CREATE TABLE `flight_requests` (
  `id` int(11) NOT NULL,
  `direction` tinyint(1) NOT NULL,
  `departure_date` date NOT NULL,
  `arrival_date` date NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `fk_flight` int(11) DEFAULT NULL,
  `fk_entertainment_plan` int(11) NOT NULL,
  `fk_food_plan` int(11) NOT NULL,
  `fk_sport_plan` int(11) NOT NULL,
  `fk_client` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `flight_requests`
--

INSERT INTO `flight_requests` (`id`, `direction`, `departure_date`, `arrival_date`, `price`, `fk_flight`, `fk_entertainment_plan`, `fk_food_plan`, `fk_sport_plan`, `fk_client`) VALUES
(2, 0, '2020-05-13', '2020-05-22', '100000.00', NULL, 1, 22, 1, 1),
(3, 0, '2020-05-10', '2020-05-22', '1000000.00', NULL, 1, 27, 1, 3),
(4, 0, '2020-05-08', '2020-05-18', '10000000.00', NULL, 1, 24, 1, 4),
(5, 0, '2020-05-19', '2020-05-30', '1000000.00', NULL, 1, 21, 1, 5),
(6, 0, '2020-05-14', '2020-05-25', '10000000.00', NULL, 1, 25, 1, 6),
(7, 0, '2020-05-15', '2020-05-25', '10000000.00', NULL, 1, 22, 1, 7),
(8, 0, '2020-05-13', '2020-05-25', '10000000.00', NULL, 1, 24, 1, 8),
(9, 1, '2020-05-30', '2020-06-10', '1000000.00', NULL, 1, 24, 1, 9),
(10, 0, '2020-05-02', '2020-05-19', '10000.00', NULL, 1, 25, 1, 10),
(11, 1, '2020-05-20', '2020-05-29', '100000.00', NULL, 1, 25, 1, 11),
(12, 1, '2020-05-11', '2020-05-20', '1000000.00', NULL, 1, 24, 1, 12),
(13, 0, '2020-05-13', '2020-05-20', '100000.00', NULL, 1, 28, 1, 3),
(14, 1, '2020-05-20', '2020-05-27', '10000.00', NULL, 1, 26, 1, 4),
(15, 0, '2020-06-18', '2020-05-29', '1000000.00', NULL, 1, 22, 1, 6),
(16, 0, '2020-05-10', '2020-05-20', '1000000.00', NULL, 1, 28, 1, 3),
(17, 0, '2020-05-30', '2020-06-18', '100000.00', NULL, 1, 21, 1, 12),
(18, 0, '2020-05-30', '2020-06-18', '100000.00', NULL, 1, 24, 1, 11),
(19, 0, '2020-05-30', '2020-06-18', '100000.00', NULL, 1, 28, 1, 10),
(20, 0, '2020-05-30', '2020-06-18', '1000000.00', NULL, 1, 21, 1, 8),
(21, 0, '2020-05-30', '2020-06-18', '1000000.00', NULL, 1, 23, 1, 7),
(22, 0, '2020-05-30', '2020-06-18', '100000.00', NULL, 1, 21, 1, 6),
(23, 0, '2020-05-30', '2020-06-18', '1000000.00', NULL, 1, 24, 1, 5),
(24, 0, '2020-05-30', '2020-06-18', '100000.00', NULL, 1, 21, 1, 4),
(25, 0, '2020-05-30', '2020-05-28', '100000.00', NULL, 1, 28, 1, 1),
(26, 0, '2020-05-01', '2020-05-14', '1000000.00', NULL, 1, 21, 1, 3),
(27, 0, '2020-05-01', '2020-05-14', '100000.00', NULL, 1, 24, 1, 4),
(28, 0, '2020-05-01', '2020-05-13', '100000.00', NULL, 1, 26, 1, 5),
(29, 0, '2020-05-01', '2020-05-15', '10000.00', NULL, 1, 26, 1, 6),
(30, 0, '2020-05-01', '2020-05-08', '100000.00', NULL, 1, 21, 1, 7),
(31, 0, '2020-05-01', '2020-05-13', '100000.00', NULL, 1, 25, 1, 8),
(32, 0, '2020-05-01', '2020-05-19', '100000.00', NULL, 1, 25, 1, 9),
(33, 0, '2020-05-01', '2020-05-15', '1000000.00', NULL, 1, 24, 1, 10),
(34, 0, '2020-05-01', '2020-05-21', '100000.00', NULL, 1, 28, 1, 11),
(35, 0, '2020-05-01', '2020-05-20', '100000.00', NULL, 1, 25, 1, 12);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `food_plans`
--

CREATE TABLE `food_plans` (
  `id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `text` text NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `removed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `food_plans`
--

INSERT INTO `food_plans` (`id`, `title`, `text`, `price`, `removed`, `fk_administrator`) VALUES
(19, 'test', 'test', '1100.00', 1, 2),
(20, 'tasdrfsa', 'sadfgasdf', '100101.00', 1, 2),
(21, 'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA', 'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA', '10.00', 1, 2),
(22, 'AAa', 'aetasrfesad', '1050.00', 1, 2),
(23, 'test', 'test', '10.00', 1, 2),
(24, 'test', 'test1', '10.00', 1, 2),
(25, 'test', 'test1', '10.00', 1, 2),
(26, 'test', 'teee11', '11.00', 0, 2),
(27, 'xc', 'zx', '1050.00', 1, 2),
(28, 'ccc', 'cfbvn', '5.14', 0, 2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `payments`
--

CREATE TABLE `payments` (
  `id` int(11) NOT NULL,
  `date` date NOT NULL,
  `card_number` varchar(100) NOT NULL,
  `sum` decimal(10,2) NOT NULL,
  `fk_flight` int(11) NOT NULL,
  `fk_client` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `sport_plans`
--

CREATE TABLE `sport_plans` (
  `id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `text` text NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `removed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `sport_plans`
--

INSERT INTO `sport_plans` (`id`, `title`, `text`, `price`, `removed`, `fk_administrator`) VALUES
(1, 'test', 'test', '1000.00', 0, 2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `phone` varchar(12) NOT NULL,
  `removed` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sukurta duomenų kopija lentelei `users`
--

INSERT INTO `users` (`id`, `name`, `last_name`, `email`, `password`, `phone`, `removed`) VALUES
(1, 'Lukas', 'Pupelis', 'lukas.pupelis@ktu.edu', '*D54C8CF5290EDFF3AE9923A0C1F5EA80097221B3', '867586765', 0),
(2, 'test', 'Administratorius', 'admin@ktu.lt', '*DEE59C300700AF9B586F9F2A702231C0AC373A13', '867655564', 0),
(3, 'Paulius', 'Paulauskas', 'paulius.paulauskas@gmail.com', '*A02AA727CF2E8C5E6F07A382910C4028D65A053A', '867586756', 0),
(4, 'Tomas', 'Tomauskas', 'tomas.tomauskas@gmail.com', '*BEF13E9308823E34BCEFACC627EE708D6CAEE9A9', '867666423', 0),
(5, 'Jonas', 'Jonaitis', 'jonas.jonaitis@gmail.com', '*5B9956176408EDCAC92456D111CEB0706C5BC9F3', '867623123', 0),
(6, 'Petras', 'Petraitis', 'petras@gmail.com', '*7F0C90A004C46C64A0EB9DDDCE5DE0DC437A635C', '867644531', 0),
(7, 'Vytautas', 'Vytautaitis', 'vytautas@gmail.com', '*C456C4ED017C0A70C5F25F9F14962AAAF2A3CF52', '864522132', 0),
(8, 'Kęstas', 'Kęstaitis', 'kestas@gmail.com', '*F6DD0C0AC75395CB5BFC12C46B8880CD156B4799', '867655312', 0),
(9, 'Kazys', 'Binkis', 'kazys@gmail.com', '*C456C4ED017C0A70C5F25F9F14962AAAF2A3CF52', '867666541', 0),
(10, 'Sigitas', 'Geda', 'sigitas@gmail.com', '*7F0C90A004C46C64A0EB9DDDCE5DE0DC437A635C', '867655123', 0),
(11, 'Jonas', 'Biliūnas', 'biliunas@gmail.com', '*033325B717C9347C9FD49BA1F76B0D3D6253ACD9', '867655421', 0),
(12, 'Salomėja', 'Nėris', 'salomeja@gmail.com', '*7F0C90A004C46C64A0EB9DDDCE5DE0DC437A635C', '867655421', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `administrators`
--
ALTER TABLE `administrators`
  ADD PRIMARY KEY (`fk_user`);

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`fk_user`);

--
-- Indexes for table `entertainment_plans`
--
ALTER TABLE `entertainment_plans`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_eplancreator` (`fk_administrator`);

--
-- Indexes for table `flights`
--
ALTER TABLE `flights`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_registered` (`fk_administrator`);

--
-- Indexes for table `flight_durations`
--
ALTER TABLE `flight_durations`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `flight_requests`
--
ALTER TABLE `flight_requests`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_client` (`fk_client`),
  ADD KEY `fk_eplan` (`fk_entertainment_plan`),
  ADD KEY `fk_fplan` (`fk_food_plan`),
  ADD KEY `fk_splan` (`fk_sport_plan`),
  ADD KEY `fk_flight` (`fk_flight`);

--
-- Indexes for table `food_plans`
--
ALTER TABLE `food_plans`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_fplancreator` (`fk_administrator`);

--
-- Indexes for table `payments`
--
ALTER TABLE `payments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_payer` (`fk_client`),
  ADD KEY `fk_pflight` (`fk_flight`);

--
-- Indexes for table `sport_plans`
--
ALTER TABLE `sport_plans`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_splancreator` (`fk_administrator`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `entertainment_plans`
--
ALTER TABLE `entertainment_plans`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `flights`
--
ALTER TABLE `flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `flight_durations`
--
ALTER TABLE `flight_durations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `flight_requests`
--
ALTER TABLE `flight_requests`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `food_plans`
--
ALTER TABLE `food_plans`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `payments`
--
ALTER TABLE `payments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sport_plans`
--
ALTER TABLE `sport_plans`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Apribojimai eksportuotom lentelėm
--

--
-- Apribojimai lentelei `administrators`
--
ALTER TABLE `administrators`
  ADD CONSTRAINT `fk_user` FOREIGN KEY (`fk_user`) REFERENCES `users` (`id`);

--
-- Apribojimai lentelei `clients`
--
ALTER TABLE `clients`
  ADD CONSTRAINT `fk_user1` FOREIGN KEY (`fk_user`) REFERENCES `users` (`id`);

--
-- Apribojimai lentelei `entertainment_plans`
--
ALTER TABLE `entertainment_plans`
  ADD CONSTRAINT `fk_eplancreator` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);

--
-- Apribojimai lentelei `flights`
--
ALTER TABLE `flights`
  ADD CONSTRAINT `fk_registered` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);

--
-- Apribojimai lentelei `flight_requests`
--
ALTER TABLE `flight_requests`
  ADD CONSTRAINT `fk_client` FOREIGN KEY (`fk_client`) REFERENCES `clients` (`fk_user`),
  ADD CONSTRAINT `fk_eplan` FOREIGN KEY (`fk_entertainment_plan`) REFERENCES `entertainment_plans` (`id`),
  ADD CONSTRAINT `fk_flight` FOREIGN KEY (`fk_flight`) REFERENCES `flights` (`id`),
  ADD CONSTRAINT `fk_fplan` FOREIGN KEY (`fk_food_plan`) REFERENCES `food_plans` (`id`),
  ADD CONSTRAINT `fk_splan` FOREIGN KEY (`fk_sport_plan`) REFERENCES `sport_plans` (`id`);

--
-- Apribojimai lentelei `food_plans`
--
ALTER TABLE `food_plans`
  ADD CONSTRAINT `fk_fplancreator` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);

--
-- Apribojimai lentelei `payments`
--
ALTER TABLE `payments`
  ADD CONSTRAINT `fk_payer` FOREIGN KEY (`fk_client`) REFERENCES `clients` (`fk_user`),
  ADD CONSTRAINT `fk_pflight` FOREIGN KEY (`fk_flight`) REFERENCES `flights` (`id`);

--
-- Apribojimai lentelei `sport_plans`
--
ALTER TABLE `sport_plans`
  ADD CONSTRAINT `fk_splancreator` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
