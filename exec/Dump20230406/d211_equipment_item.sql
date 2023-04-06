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
-- Table structure for table `equipment_item`
--

DROP TABLE IF EXISTS `equipment_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipment_item` (
  `equip_item_idx` int NOT NULL,
  `equip_name` varchar(50) NOT NULL,
  `equip_damage` float NOT NULL,
  `equip_speed` float NOT NULL,
  `skill_name` varchar(15) NOT NULL,
  `skill_damage` float NOT NULL,
  `skill_cooltime` float NOT NULL,
  `equip_type` int NOT NULL,
  PRIMARY KEY (`equip_item_idx`),
  UNIQUE KEY `equip_name_UNIQUE` (`equip_name`),
  UNIQUE KEY `equip_item_idx_UNIQUE` (`equip_item_idx`),
  CONSTRAINT `item_code_for_equip` FOREIGN KEY (`equip_item_idx`) REFERENCES `item` (`item_code`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `item_name_for_equip` FOREIGN KEY (`equip_name`) REFERENCES `item` (`item_name`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment_item`
--

LOCK TABLES `equipment_item` WRITE;
/*!40000 ALTER TABLE `equipment_item` DISABLE KEYS */;
INSERT INTO `equipment_item` VALUES (100,'대검',10,1,'skill1',10,3,1),(101,'태도',10,1,'skill2',10,3,0),(102,'해머',10,1,'skill3',10,3,2),(103,'피리',10,1,'skill4',10,3,3),(104,'활',10,1,'skill5',10,3,0),(105,'보우건',10,1,'skill6',10,3,0),(106,'한손검',10,1,'skill7',10,3,0),(107,'쌍검',10,1,'skill8',10,3,4),(108,'도끼',10,1,'skill9',10,3,0),(109,'창',10,1,'skill10',10,3,5);
/*!40000 ALTER TABLE `equipment_item` ENABLE KEYS */;
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
