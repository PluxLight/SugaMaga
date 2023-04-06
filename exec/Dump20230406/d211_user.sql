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
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `user_idx` bigint NOT NULL AUTO_INCREMENT,
  `email` varchar(50) DEFAULT NULL,
  `nickname` varchar(16) DEFAULT NULL,
  `uid` varchar(100) NOT NULL,
  `active` int DEFAULT '1',
  PRIMARY KEY (`user_idx`),
  UNIQUE KEY `uid_UNIQUE` (`uid`),
  UNIQUE KEY `UK_ob8kqyqqgmefl0aco34akdtpe` (`email`),
  UNIQUE KEY `nickname_UNIQUE` (`nickname`)
) ENGINE=InnoDB AUTO_INCREMENT=138 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (2,'demo01@email.com','demo012','1MNlG0QAvaXAFfYgd4p9OyjpGJq2',1),(3,'jeon@email.com','eunsu','iZcds61i7NOt9wyCPS4MUkaSKsD3',1),(4,'kim@email.com','kim','y01Ptnb8sodLqZLSlRKdzRKWI713',1),(5,'park@email.com','heajun','mV3CCyfC4VQ5AzQWwgqP8VRXFs83',1),(6,'lee@email.com','seokgi','bqENGmhsmaScVJLbjj1nw6skkHm2',1),(7,'jung@email.com','jinuk','wbtDYL8ZcXXn0QrLzRjfRoANCq12',1),(75,'pluxlight@gmail.com','plux','bpNG2yAnO2YULovCpTM0Rzq2vLG2',1),(93,'demo02@email.com','demodemo0202','3mu2RGTbElPS4EcA9HDUu8s3ler1',1),(95,'test@gmail.com','testtest','E6MwnYo3ZzLjLRzBjykZ0NtRuYh2',1),(97,'suga001@email.com','suga001','VJNVe2KvBWc7YkJaDTs10YxA8ry2',1),(99,'1@email.com','01','l8aH0oFlt6VodB1PnnpIpG0nXWE3',1),(101,'suga002@email.com','suga002','ULA6hjyx42aPc5tbamhBypbqXJO2',1),(103,'suga003@email.com','suga003','xLXOscnBzlcsCdmaevU6JIuJxuN2',1),(105,'suga004@email.com','suga004','3V0H2DrkuJR1o6DwQ56dG7kzeYs1',1),(107,'suga005@email.com','suga005','Sb6ipofHIOWfQT4TxJGxuMwb3Gv2',1),(109,'suga006@email.com','suga006','HCYrP19URZONcdpAsUPzq6Nkve12',1),(111,'suga007@email.com','suga007','AyD5QSVP3mVVmBqIAi0aFhW9lai2',1),(113,'suga008@email.com','suga008','SbNx1rRLahg4cT7Gzw53R5imddq1',1),(115,'suga009@email.com','suga009','rqbmZyu2nBT22Y4CjfudnUcmYdA2',1),(117,'suga010@email.com','suga010','qAFzk8wMGRTmkc62AMmrrQ2fDv12',1),(119,'suga011@email.com','suga011','jHca0bCRHzgRGNO6yLHLFV3Gk673',1),(121,'suga012@email.com','suga012','qV5P1fTw3KQAG6uGdUV0ygkasaR2',1),(123,'suga013@email.com','suga013','ISwV87sH9ScyNOiY02XMbchMPfJ2',1),(125,'suga014@email.com','suga014','fg70PQkyaiRUFx8lxMlrJlaFdi32',1),(127,'suga015@email.com','suga015','g4sLUj8yncQHHPiRyfMJ8YmBUot2',1),(129,'suga016@email.com','suga016','H2LyPtLqYHfA9ohS7uqj0fiMVXa2',1),(131,'suga017@email.com','suga017','z9mofRmmLvRuVHMjIDCYkneFuj82',1),(133,'suga018@email.com','suga018','n1HHadCbZ8RC6kGkHyZFQ9UGXzE3',1),(135,'suga019@email.com','suga019','4gk6GZTBmvaF2rvdFmkdwkB1BB72',1),(137,'suga020@email.com','suga020','7jMRrFFAWFUlMxWE0M74q3bYONh2',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
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
