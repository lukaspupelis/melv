-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 17, 2020 at 05:06 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.2.28

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
-- Table structure for table `administrators`
--

CREATE TABLE `administrators` (
  `fk_user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `administrators`
--

INSERT INTO `administrators` (`fk_user`) VALUES
(2);

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `fk_user` int(11) NOT NULL,
  `birthdate` date NOT NULL,
  `personal_no` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `entertainment_plans`
--

CREATE TABLE `entertainment_plans` (
  `id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `text` text NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `removed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `flights`
--

CREATE TABLE `flights` (
  `id` int(11) NOT NULL,
  `direction` tinyint(1) NOT NULL,
  `departure_date` date NOT NULL,
  `arrival_date` date NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `confirmed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `flight_durations`
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
-- Table structure for table `flight_requests`
--

CREATE TABLE `flight_requests` (
  `id` int(11) NOT NULL,
  `direction` tinyint(1) NOT NULL,
  `departure_date` date NOT NULL,
  `arrival_date` date NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `fk_flight` int(11) NOT NULL,
  `fk_entertainment_plan` int(11) NOT NULL,
  `fk_food_plan` int(11) NOT NULL,
  `fk_sport_plan` int(11) NOT NULL,
  `fk_client` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `food_plans`
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
-- Dumping data for table `food_plans`
--

INSERT INTO `food_plans` (`id`, `title`, `text`, `price`, `removed`, `fk_administrator`) VALUES
(19, 'test', 'test', '1100.00', 1, 2),
(20, 'tasdrfsa', 'sadfgasdf', '100101.00', 0, 2);

-- --------------------------------------------------------

--
-- Table structure for table `payments`
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
-- Table structure for table `sport_plans`
--

CREATE TABLE `sport_plans` (
  `id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `text` text NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `removed` tinyint(1) NOT NULL,
  `fk_administrator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `users`
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
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `last_name`, `email`, `password`, `phone`, `removed`) VALUES
(1, 'Lukas', 'Pupelis', 'lukas.pupelis@ktu.edu', '*D54C8CF5290EDFF3AE9923A0C1F5EA80097221B3', '867586765', 0),
(2, 'test', 'Administratorius', 'admin@ktu.lt', '*DEE59C300700AF9B586F9F2A702231C0AC373A13', '867655564', 0);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `flights`
--
ALTER TABLE `flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `flight_durations`
--
ALTER TABLE `flight_durations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `flight_requests`
--
ALTER TABLE `flight_requests`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `food_plans`
--
ALTER TABLE `food_plans`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `payments`
--
ALTER TABLE `payments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sport_plans`
--
ALTER TABLE `sport_plans`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `administrators`
--
ALTER TABLE `administrators`
  ADD CONSTRAINT `fk_user` FOREIGN KEY (`fk_user`) REFERENCES `users` (`id`);

--
-- Constraints for table `clients`
--
ALTER TABLE `clients`
  ADD CONSTRAINT `fk_user1` FOREIGN KEY (`fk_user`) REFERENCES `users` (`id`);

--
-- Constraints for table `entertainment_plans`
--
ALTER TABLE `entertainment_plans`
  ADD CONSTRAINT `fk_eplancreator` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);

--
-- Constraints for table `flights`
--
ALTER TABLE `flights`
  ADD CONSTRAINT `fk_registered` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);

--
-- Constraints for table `flight_requests`
--
ALTER TABLE `flight_requests`
  ADD CONSTRAINT `fk_client` FOREIGN KEY (`fk_client`) REFERENCES `clients` (`fk_user`),
  ADD CONSTRAINT `fk_eplan` FOREIGN KEY (`fk_entertainment_plan`) REFERENCES `entertainment_plans` (`id`),
  ADD CONSTRAINT `fk_flight` FOREIGN KEY (`fk_flight`) REFERENCES `flights` (`id`),
  ADD CONSTRAINT `fk_fplan` FOREIGN KEY (`fk_food_plan`) REFERENCES `food_plans` (`id`),
  ADD CONSTRAINT `fk_splan` FOREIGN KEY (`fk_sport_plan`) REFERENCES `sport_plans` (`id`);

--
-- Constraints for table `food_plans`
--
ALTER TABLE `food_plans`
  ADD CONSTRAINT `fk_fplancreator` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);

--
-- Constraints for table `payments`
--
ALTER TABLE `payments`
  ADD CONSTRAINT `fk_payer` FOREIGN KEY (`fk_client`) REFERENCES `clients` (`fk_user`),
  ADD CONSTRAINT `fk_pflight` FOREIGN KEY (`fk_flight`) REFERENCES `flights` (`id`);

--
-- Constraints for table `sport_plans`
--
ALTER TABLE `sport_plans`
  ADD CONSTRAINT `fk_splancreator` FOREIGN KEY (`fk_administrator`) REFERENCES `administrators` (`fk_user`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
