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
-- Table structure for table `drop_table`
--

DROP TABLE IF EXISTS `drop_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drop_table` (
  `drop_table_idx` int NOT NULL AUTO_INCREMENT,
  `table_idx` int NOT NULL,
  `item_code` int NOT NULL,
  `item_name` varchar(50) NOT NULL,
  `percentage` int NOT NULL,
  PRIMARY KEY (`drop_table_idx`),
  KEY `item_code_for_drop_table_idx` (`item_code`,`item_name`),
  KEY `item_name_for_drop_tabl_idx` (`item_name`),
  CONSTRAINT `item_code_for_drop_table` FOREIGN KEY (`item_code`) REFERENCES `item` (`item_code`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `item_name_for_drop_tabl` FOREIGN KEY (`item_name`) REFERENCES `item` (`item_name`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drop_table`
--

LOCK TABLES `drop_table` WRITE;
/*!40000 ALTER TABLE `drop_table` DISABLE KEYS */;
INSERT INTO `drop_table` VALUES (1,1,100,'대검',10),(2,1,101,'태도',10),(3,1,102,'해머',10),(4,1,103,'피리',10),(5,1,104,'활',10),(6,1,105,'보우건',10),(7,1,106,'한손검',10),(8,1,107,'쌍검',10),(9,1,108,'도끼',10),(10,1,109,'창',10),(11,2,200,'초급 회복약',50),(12,2,201,'중급 회복약',25),(13,2,202,'고급 회복약',15),(14,2,203,'최고급 회복약',10);
/*!40000 ALTER TABLE `drop_table` ENABLE KEYS */;
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
