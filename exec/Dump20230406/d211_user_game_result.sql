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
-- Table structure for table `user_game_result`
--

DROP TABLE IF EXISTS `user_game_result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_game_result` (
  `user_game_result_idx` int NOT NULL AUTO_INCREMENT,
  `game_room_idx` int NOT NULL,
  `uid` varchar(100) NOT NULL,
  `result_rank` int NOT NULL,
  `result_kill` int NOT NULL,
  `map_idx` int NOT NULL,
  PRIMARY KEY (`user_game_result_idx`),
  KEY `map_idx_for_user_game_result_idx` (`map_idx`),
  KEY `uid_for_user_game_result_idx` (`uid`),
  CONSTRAINT `map_idx_for_user_game_result` FOREIGN KEY (`map_idx`) REFERENCES `map` (`map_idx`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `uid_for_user_game_result` FOREIGN KEY (`uid`) REFERENCES `user` (`uid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_game_result`
--

LOCK TABLES `user_game_result` WRITE;
/*!40000 ALTER TABLE `user_game_result` DISABLE KEYS */;
INSERT INTO `user_game_result` VALUES (1,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',2,3,1),(2,2,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',1,49,1),(3,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(4,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(5,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(6,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(7,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(8,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(9,1,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',3,27,1),(10,4,'1MNlG0QAvaXAFfYgd4p9OyjpGJq2',2,3,1);
/*!40000 ALTER TABLE `user_game_result` ENABLE KEYS */;
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
