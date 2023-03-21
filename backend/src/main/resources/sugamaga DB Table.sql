-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
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
  `consumable_item_idx` int NOT NULL AUTO_INCREMENT,
  `consumable_name` text NOT NULL,
  `consumable_category` text NOT NULL,
  `consumable_value` int NOT NULL,
  PRIMARY KEY (`consumable_item_idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `drop_table`
--

DROP TABLE IF EXISTS `drop_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drop_table` (
  `drop_table_idx` int NOT NULL AUTO_INCREMENT,
  `table_idx` int NOT NULL,
  `consumable_idx` int DEFAULT NULL,
  `equip_idx` int DEFAULT NULL,
  `percentage` int NOT NULL,
  PRIMARY KEY (`drop_table_idx`),
  KEY `consum_idx_for_drop_table_idx` (`consumable_idx`),
  KEY `equip_idx_for_drop_table_idx` (`equip_idx`),
  CONSTRAINT `consum_idx_for_drop_table` FOREIGN KEY (`consumable_idx`) REFERENCES `consumable_item` (`consumable_item_idx`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `equip_idx_for_drop_table` FOREIGN KEY (`equip_idx`) REFERENCES `equipment_item` (`equip_item_idx`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `equipment_item`
--

DROP TABLE IF EXISTS `equipment_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipment_item` (
  `equip_item_idx` int NOT NULL AUTO_INCREMENT,
  `equip_name` text NOT NULL,
  `equip_value` int NOT NULL,
  PRIMARY KEY (`equip_item_idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `game_room`
--

DROP TABLE IF EXISTS `game_room`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `game_room` (
  `game_room_idx` int NOT NULL AUTO_INCREMENT,
  `user_idx` bigint NOT NULL,
  `game_room_title` text NOT NULL,
  `room_state` int DEFAULT NULL,
  PRIMARY KEY (`game_room_idx`),
  KEY `user_idx_for_game_room_idx` (`user_idx`),
  CONSTRAINT `user_idx_for_game_room` FOREIGN KEY (`user_idx`) REFERENCES `user` (`user_idx`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `hibernate_sequence`
--

DROP TABLE IF EXISTS `hibernate_sequence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hibernate_sequence` (
  `next_val` bigint DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `map`
--

DROP TABLE IF EXISTS `map`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `map` (
  `map_idx` int NOT NULL AUTO_INCREMENT,
  `map_title` text NOT NULL,
  PRIMARY KEY (`map_idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `monster`
--

DROP TABLE IF EXISTS `monster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `monster` (
  `monster_idx` int NOT NULL AUTO_INCREMENT,
  `drop_table` int NOT NULL,
  `monster_name` text NOT NULL,
  PRIMARY KEY (`monster_idx`),
  KEY `drop_table_idx_for_monster_idx` (`drop_table`),
  CONSTRAINT `drop_table_idx_for_monster` FOREIGN KEY (`drop_table`) REFERENCES `drop_table` (`drop_table_idx`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sign`
--

DROP TABLE IF EXISTS `sign`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sign` (
  `user_idx` bigint NOT NULL,
  `active` int NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `uid` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`user_idx`),
  UNIQUE KEY `UK_o8da8a8rfl0x8p9kmtfkxgakg` (`email`),
  UNIQUE KEY `UK_8gvlyv89awiwcjmdrilkeffru` (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `test_table`
--

DROP TABLE IF EXISTS `test_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `test_table` (
  `pk` int NOT NULL AUTO_INCREMENT,
  `int_col` int DEFAULT NULL,
  `str_col` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`pk`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `user_idx` bigint NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `nickname` varchar(16) DEFAULT NULL,
  `uid` varchar(45) DEFAULT NULL,
  `active` int DEFAULT '1',
  PRIMARY KEY (`user_idx`),
  UNIQUE KEY `UK_ob8kqyqqgmefl0aco34akdtpe` (`email`),
  UNIQUE KEY `nickname_UNIQUE` (`nickname`),
  UNIQUE KEY `uid_UNIQUE` (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_custom`
--

DROP TABLE IF EXISTS `user_custom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_custom` (
  `user_custom_idx` int NOT NULL AUTO_INCREMENT,
  `user_idx` bigint NOT NULL,
  `cap` int NOT NULL,
  `hair` int NOT NULL,
  `face` int NOT NULL,
  `eyes` int NOT NULL,
  `mouse` int NOT NULL,
  `body` int NOT NULL,
  PRIMARY KEY (`user_custom_idx`),
  UNIQUE KEY `user_idx_UNIQUE` (`user_idx`),
  CONSTRAINT `user_idx_for_user_custom` FOREIGN KEY (`user_idx`) REFERENCES `user` (`user_idx`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_game_result`
--

DROP TABLE IF EXISTS `user_game_result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_game_result` (
  `user_game_result_idx` int NOT NULL AUTO_INCREMENT,
  `game_room_idx` int NOT NULL,
  `user_idx` bigint NOT NULL,
  `result_rank` int NOT NULL,
  `result_kill` int NOT NULL,
  `map_idx` int NOT NULL,
  PRIMARY KEY (`user_game_result_idx`),
  KEY `user_idx_fk_idx` (`user_idx`),
  KEY `map_idx_for_user_game_result_idx` (`map_idx`),
  CONSTRAINT `map_idx_for_user_game_result` FOREIGN KEY (`map_idx`) REFERENCES `map` (`map_idx`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `user_idx_for_user_game_result` FOREIGN KEY (`user_idx`) REFERENCES `user` (`user_idx`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-21 14:32:49
