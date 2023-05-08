CREATE DATABASE  IF NOT EXISTS `woojin_library` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `woojin_library`;
-- MySQL dump 10.13  Distrib 8.0.33, for macos13 (arm64)
--
-- Host: localhost    Database: woojin_library
-- ------------------------------------------------------
-- Server version	8.0.32

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
-- Table structure for table `log_data`
--

DROP TABLE IF EXISTS `log_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `log_data` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `log_time` longtext NOT NULL,
  `log_contents` longtext NOT NULL,
  `log_action` longtext,
  `user_name` longtext NOT NULL,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `log_data`
--

LOCK TABLES `log_data` WRITE;
/*!40000 ALTER TABLE `log_data` DISABLE KEYS */;
INSERT INTO `log_data` VALUES (15,'5/7/2023 8:53:16 PM','로그 초기화','로그 초기화','ADMIN'),(16,'5/7/2023 8:53:19 PM','로그 파일 저장','로그 파일 저장','ADMIN'),(17,'5/7/2023 8:53:21 PM','로그 파일 저장','로그 파일 저장','ADMIN'),(18,'5/7/2023 8:53:36 PM','로그 파일 삭제','로그 파일 삭제','ADMIN'),(19,'5/7/2023 9:22:15 PM','성공','로그인','userid12'),(20,'5/7/2023 9:22:19 PM','책이 존재하지 않음','책 대출 시도','userid12'),(21,'5/7/2023 9:22:27 PM','책이 존재하지 않음','책 대출 시도','userid12'),(22,'5/7/2023 9:22:41 PM','성공','빌린 도서 조회','userid12'),(23,'5/7/2023 9:22:47 PM','성공','빌린 도서 조회','userid12'),(24,'5/7/2023 9:22:48 PM','성공','책 반납','userid12'),(25,'5/7/2023 9:22:50 PM','성공','빌린 도서 조회','userid12'),(26,'5/7/2023 9:22:51 PM','성공','빌린 도서 조회','userid12'),(27,'5/7/2023 9:22:52 PM','성공','책 반납','userid12'),(28,'5/7/2023 9:22:55 PM','성공','책 대출','userid12'),(29,'5/7/2023 9:22:58 PM','성공','빌린 도서 조회','userid12'),(30,'5/7/2023 9:26:45 PM','성공','로그인','admin123'),(31,'5/7/2023 9:27:02 PM','성공','로그인','userid12'),(32,'5/7/2023 9:27:04 PM','성공','반납 도서 조회','userid12'),(33,'5/7/2023 9:35:15 PM','아이디 틀림','로그인 시도','uerid12'),(34,'5/7/2023 9:35:18 PM','성공','로그인','userid12'),(35,'5/7/2023 9:52:28 PM','성공','로그인','userid12'),(36,'5/7/2023 9:54:01 PM','성공','로그인','userid12'),(37,'5/8/2023 3:24:27 AM','아이디 틀림','로그인 시도','asdfasdf'),(38,'5/8/2023 3:26:47 AM','성공','로그인','admin123'),(39,'5/8/2023 3:26:54 AM','로그 파일 저장','로그 파일 저장','ADMIN'),(40,'5/8/2023 3:32:17 AM','성공','로그인','userid12'),(41,'5/8/2023 3:32:59 AM','성공','로그인','userid12'),(42,'5/8/2023 3:34:40 AM','성공','로그인','admin123'),(43,'5/8/2023 3:34:48 AM','책 등록','NAVER 책 등록','ADMIN'),(44,'5/8/2023 1:18:11 PM','성공','로그인','admin123'),(45,'5/8/2023 1:18:16 PM','책 등록','NAVER 책 등록','ADMIN'),(46,'5/8/2023 1:18:18 PM','성공','빌린 책 조회','ADMIN'),(47,'5/8/2023 1:18:24 PM','책 등록','NAVER 책 등록','ADMIN'),(48,'5/8/2023 1:39:28 PM','성공','로그인','admin123'),(49,'5/8/2023 1:39:39 PM','123','로그 삭제 시도','ADMIN');
/*!40000 ALTER TABLE `log_data` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-08 14:17:01
