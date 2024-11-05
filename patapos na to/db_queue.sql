-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 05, 2024 at 05:35 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_queue`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbl_control`
--

CREATE TABLE `tbl_control` (
  `cashierOpen` tinyint(1) NOT NULL,
  `id` int(11) NOT NULL,
  `pharmacyOpen` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_control`
--

INSERT INTO `tbl_control` (`cashierOpen`, `id`, `pharmacyOpen`) VALUES
(0, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_queue`
--

CREATE TABLE `tbl_queue` (
  `id` int(11) NOT NULL,
  `serviceName` varchar(45) NOT NULL,
  `priority` varchar(45) NOT NULL,
  `qnumber` varchar(45) NOT NULL,
  `isQueuing` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_queue`
--

INSERT INTO `tbl_queue` (`id`, `serviceName`, `priority`, `qnumber`, `isQueuing`) VALUES
(245, 'Cashier', 'R', 'CS-R1000', 1),
(246, 'Cashier', 'R', 'CS-R1001', 1),
(247, 'Cashier', 'P', 'CS-P1002', 1),
(248, 'Pharmacy', 'R', 'PH-R1003', 1),
(249, 'Pharmacy', 'R', 'PH-R1004', 1),
(250, 'Pharmacy', 'P', 'PH-P1005', 1),
(251, 'Cashier', 'R', 'CS-R1006', 1),
(252, 'Cashier', 'R', 'CS-R1007', 1),
(253, 'Cashier', 'R', 'CS-R1008', 1),
(254, 'Cashier', 'P', 'CS-P1009', 1),
(255, 'Cashier', 'P', 'CS-P1010', 1),
(256, 'Pharmacy', 'R', 'PH-R1011', 1),
(257, 'Pharmacy', 'R', 'PH-R1012', 1),
(258, 'Pharmacy', 'R', 'PH-R1013', 1),
(259, 'Pharmacy', 'P', 'PH-P1014', 1),
(260, 'Pharmacy', 'P', 'PH-P1015', 1),
(261, 'Cashier', 'R', 'CS-R1016', 1),
(262, 'Cashier', 'P', 'CS-P1017', 1),
(263, 'Pharmacy', 'R', 'PH-R1018', 1),
(264, 'Cashier', 'P', 'CS-P1019', 1),
(265, 'Cashier', 'P', 'CS-P1020', 1),
(266, 'Cashier', 'P', 'CS-P1021', 1),
(267, 'Cashier', 'R', 'CS-R1022', 1),
(268, 'Cashier', 'R', 'CS-R1023', 1),
(269, 'Cashier', 'R', 'CS-R1024', 1),
(270, 'Cashier', 'P', 'CS-P1025', 1),
(271, 'Cashier', 'R', 'CS-R1026', 1),
(272, 'Pharmacy', 'P', 'PH-P1027', 1),
(273, 'Pharmacy', 'P', 'PH-P1028', 1),
(274, 'Cashier', 'R', 'CS-R1029', 1),
(275, 'Pharmacy', 'R', 'PH-R1030', 1),
(276, 'Pharmacy', 'P', 'PH-P1031', 1),
(277, 'Cashier', 'P', 'CS-P1032', 1),
(278, 'Cashier', 'R', 'CS-R1033', 1),
(279, 'Cashier', 'P', 'CS-P1034', 1),
(280, 'Cashier', 'R', 'CS-R1035', 1),
(281, 'Cashier', 'P', 'CS-P1036', 1),
(282, 'Pharmacy', 'R', 'PH-R1037', 1),
(283, 'Pharmacy', 'P', 'PH-P1038', 1),
(284, 'Cashier', 'R', 'CS-R1039', 1),
(285, 'Cashier', 'R', 'CS-R1040', 1),
(286, 'Cashier', 'R', 'CS-R1041', 1),
(287, 'Cashier', 'R', 'CS-R1042', 1),
(288, 'Cashier', 'R', 'CS-R1043', 1),
(289, 'Cashier', 'R', 'CS-R1044', 1),
(290, 'Pharmacy', 'P', 'PH-P1045', 1),
(291, 'Cashier', 'R', 'CS-R1046', 1),
(292, 'Cashier', 'R', 'CS-R1047', 1),
(293, 'Cashier', 'R', 'CS-R1048', 1),
(294, 'Pharmacy', 'R', 'PH-R1049', 1),
(295, 'Pharmacy', 'R', 'PH-R1050', 1),
(296, 'Cashier', 'R', 'CS-R1051', 1),
(297, 'Cashier', 'R', 'CS-R1052', 1),
(298, 'Cashier', 'R', 'CS-R1053', 1),
(299, 'Cashier', 'R', 'CS-R1054', 1),
(300, 'Cashier', 'R', 'CS-R1055', 1),
(301, 'Cashier', 'R', 'CS-R1056', 1),
(302, 'Cashier', 'R', 'CS-R1057', 1),
(303, 'Cashier', 'R', 'CS-R1058', 1),
(304, 'Cashier', 'R', 'CS-R1059', 1),
(305, 'Window 1', 'R', 'W1-R1060', 1),
(306, 'Window 2', 'R', 'W2-R1061', 1),
(307, 'Window 1', 'R', 'W1-R1062', 1),
(308, 'Window 2', 'R', 'W2-R1063', 1),
(309, 'Window 2', 'P', 'W2-P1064', 1),
(310, 'Window 1', 'R', 'W1-R1065', 1),
(311, 'Window 2', 'R', 'W2-R1066', 1),
(312, 'Window 1', 'R', 'W1-R1067', 1),
(313, 'Window 2', 'R', 'W2-R1068', 1),
(314, 'Window 1', 'R', 'W1-R1069', 1),
(315, 'Window 2', 'R', 'W2-R1070', 1),
(316, 'Window 2', 'P', 'W2-P1071', 1),
(317, 'Window 1', 'P', 'W1-P1072', 1),
(318, 'Window 1', 'P', 'W1-P1073', 1),
(319, 'Window 1', 'P', 'W1-P1074', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_control`
--
ALTER TABLE `tbl_control`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tbl_queue`
--
ALTER TABLE `tbl_queue`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbl_control`
--
ALTER TABLE `tbl_control`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tbl_queue`
--
ALTER TABLE `tbl_queue`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=320;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
