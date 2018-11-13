CREATE DATABASE  IF NOT EXISTS `rehepapp` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `rehepapp`;
-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: rehepapp
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `basicblock`
--

DROP TABLE IF EXISTS `basicblock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `basicblock` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `module_id` int(11) NOT NULL,
  `rva` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_basicblock_module` (`module_id`),
  CONSTRAINT `fk_basicblock_module` FOREIGN KEY (`module_id`) REFERENCES `module` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=64518 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `bot_collect`
--

DROP TABLE IF EXISTS `bot_collect`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bot_collect` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `project_id` int(11) DEFAULT NULL,
  `code` varchar(32) NOT NULL,
  `last_connect` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code_UNIQUE` (`code`) /*!80000 INVISIBLE */,
  KEY `code_index` (`code`),
  KEY `project_index` (`project_id`),
  CONSTRAINT `f_bot_collect_proj` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `bot_coverage`
--

DROP TABLE IF EXISTS `bot_coverage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bot_coverage` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `project_id` int(11) DEFAULT NULL,
  `code` varchar(64) NOT NULL,
  `last_connect` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code_UNIQUE` (`code`),
  KEY `code_index` (`code`),
  KEY `projectid_index` (`project_id`),
  CONSTRAINT `f_bot_cov_proj` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `coverage_calc_bb`
--

DROP TABLE IF EXISTS `coverage_calc_bb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `coverage_calc_bb` (
  `coverage_calc_file_id` int(11) NOT NULL,
  `basicblock_id` int(11) NOT NULL,
  PRIMARY KEY (`coverage_calc_file_id`,`basicblock_id`),
  KEY `idx_cc_bb_file` (`coverage_calc_file_id`),
  KEY `idx_cc_bb_bb` (`basicblock_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `coverage_calc_file`
--

DROP TABLE IF EXISTS `coverage_calc_file`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `coverage_calc_file` (
  `testcase_id` int(11) NOT NULL,
  `bb_count` int(11) NOT NULL,
  `url` varchar(256) NOT NULL,
  `selected` varchar(45) NOT NULL DEFAULT '0',
  PRIMARY KEY (`testcase_id`),
  KEY `idx_cc_file_id` (`testcase_id`),
  KEY `idx_cc_file_count` (`bb_count`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `module`
--

DROP TABLE IF EXISTS `module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `module` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `project_id` int(11) NOT NULL,
  `name` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_module_project` (`project_id`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_module_project` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `project`
--

DROP TABLE IF EXISTS `project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `project` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(64) NOT NULL,
  `code` varchar(16) NOT NULL,
  `extension` varchar(16) DEFAULT NULL,
  `magic` varchar(32) DEFAULT NULL,
  `testcase_count` int(11) NOT NULL DEFAULT '0',
  `coverage_count` int(11) NOT NULL DEFAULT '0',
  `active` int(11) DEFAULT '1',
  `isdefault` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`),
  UNIQUE KEY `code_UNIQUE` (`code`),
  KEY `code_IDX` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `search`
--

DROP TABLE IF EXISTS `search`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `search` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `project_id` int(11) NOT NULL,
  `bot_id` int(11) DEFAULT NULL,
  `search_str` varchar(8) NOT NULL,
  `started` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ended` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idx_search_uniq` (`project_id`,`search_str`),
  KEY `idx_search_proj` (`project_id`),
  KEY `idx_search_bot` (`bot_id`) /*!80000 INVISIBLE */,
  KEY `idx_search_str` (`search_str`) /*!80000 INVISIBLE */,
  KEY `idx_search_start` (`started`) /*!80000 INVISIBLE */,
  CONSTRAINT `f_search_collect` FOREIGN KEY (`bot_id`) REFERENCES `bot_collect` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `f_search_project` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `testcase`
--

DROP TABLE IF EXISTS `testcase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `testcase` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `project_id` int(11) NOT NULL,
  `url` varchar(256) NOT NULL,
  `hash` varchar(32) NOT NULL,
  `size` int(11) NOT NULL,
  `added` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `started` timestamp NULL DEFAULT NULL,
  `ended` timestamp NULL DEFAULT NULL,
  `module_count` int(11) DEFAULT NULL,
  `basicblock_count` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idx_testcase_project_id_hash` (`project_id`,`hash`),
  KEY `idx_testcase_project_id` (`project_id`),
  KEY `idx_testcase_started` (`started`),
  KEY `idx_testcase_ended` (`ended`),
  KEY `idx_testcase_basicblock_count` (`basicblock_count`),
  KEY `idx_testcase_module_count` (`module_count`),
  CONSTRAINT `fk_testcase_project` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4804 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `testcase_basicblock`
--

DROP TABLE IF EXISTS `testcase_basicblock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `testcase_basicblock` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `testcase_id` int(11) NOT NULL,
  `basicblock_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idx_testcase_basicblock_uq` (`testcase_id`,`basicblock_id`) /*!80000 INVISIBLE */,
  KEY `idx_testcase_basicblock_t` (`testcase_id`) /*!80000 INVISIBLE */,
  KEY `idx_testcase_basicblock_b` (`basicblock_id`),
  CONSTRAINT `fk_testcase_basicblock_b` FOREIGN KEY (`basicblock_id`) REFERENCES `basicblock` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_testcase_basicblock_t` FOREIGN KEY (`testcase_id`) REFERENCES `testcase` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=263211 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `testcase_module`
--

DROP TABLE IF EXISTS `testcase_module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `testcase_module` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `testcase_id` int(11) NOT NULL,
  `module_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idx_testcase_module_uq` (`testcase_id`,`module_id`) /*!80000 INVISIBLE */,
  KEY `idx_testcase_module_t` (`testcase_id`) /*!80000 INVISIBLE */,
  KEY `idx_testcase_module_m` (`module_id`),
  CONSTRAINT `fk_testcase_module_m` FOREIGN KEY (`module_id`) REFERENCES `module` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_testcase_module_t` FOREIGN KEY (`testcase_id`) REFERENCES `testcase` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3766 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-11-13 18:21:05
