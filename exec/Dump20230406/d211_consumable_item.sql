-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: aeoragy.com    Database: d211
-- ------------------------------------------------------
-- Server version	8.0.32-0ubuntu0.20.04.2

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `consumable_item`
--

DROP TABLE IF EXISTS `consumable_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consumable_item` (
  `consumable_item_idx` int NOT NULL,
  `consumable_name` varchar(50) NOT NULL,
  `consumable_category` text NOT NULL,
  `consumable_value` int NOT NULL,
  PRIMARY KEY (`consumable_item_idx`),
  UNIQUE KEY `consumable_name_UNIQUE` (`consumable_name`),
  UNIQUE KEY `consumable_item_idx_UNIQUE` (`consumable_item_idx`),
  CONSTRAINT `item_code_for_consum` FOREIGN KEY (`consumable_item_idx`) REFERENCES `item` (`item_code`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `item_name_for_consum` FOREIGN KEY (`consumable_name`) REFERENCES `item` (`item_name`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consumable_item`
--

LOCK TABLES `consumable_item` WRITE;
/*!40000 ALTER TABLE `consumable_item` DISABLE KEYS */;
INSERT INTO `consumable_item` VALUES (200,'초급 회복약','회복',10),(201,'중급 회복약','회복',20),(202,'고급 회복약','회복',30),(203,'최고급 회복약','회복',50);
/*!40000 ALTER TABLE `consumable_item` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-04-06 22:19:47
