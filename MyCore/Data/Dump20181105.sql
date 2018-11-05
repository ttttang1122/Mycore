-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: mycore
-- ------------------------------------------------------
-- Server version	5.7.22-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20180521052407_IntialCreateMycore','2.0.2-rtm-10011'),('20180726133225_Goodinfos','2.0.2-rtm-10011'),('20180726140340_editgoodinfo','2.0.2-rtm-10011'),('20180820124746_SupperAdd','2.0.2-rtm-10011'),('20180820140446_StoreInfo','2.0.2-rtm-10011'),('20180822103316_orderbill','2.0.2-rtm-10011'),('20180822134508_Orderbilledit1','2.0.2-rtm-10011'),('20180822140238_editbillls','2.0.2-rtm-10011'),('20180918025740_editOrderBills1','2.0.2-rtm-10011'),('20180921062641_createInstore','2.0.2-rtm-10011'),('20180927082506_editinstores1','2.0.2-rtm-10011'),('20180927123049_editinstore22','2.0.2-rtm-10011'),('20180930063817_editbuyreturn','2.0.2-rtm-10011'),('20181011073920_createsell','2.0.2-rtm-10011'),('20181011081642_ediesells','2.0.2-rtm-10011'),('20181015014054_editgoodid','2.0.2-rtm-10011'),('20181015020952_editshname','2.0.2-rtm-10011'),('20181015083803_createmorelose','2.0.2-rtm-10011'),('20181016084737_createtakestock','2.0.2-rtm-10011'),('20181017131407_eidtfktake','2.0.2-rtm-10011'),('20181017132028_deltake','2.0.2-rtm-10011'),('20181017132219_createtake','2.0.2-rtm-10011');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goodinfo`
--

DROP TABLE IF EXISTS `goodinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `goodinfo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `ClassID` varchar(45) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `EditDate` datetime DEFAULT NULL,
  `EditName` varchar(45) DEFAULT NULL,
  `ForType` varchar(45) DEFAULT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `PYM` varchar(145) DEFAULT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `ShopPrice` decimal(18,2) NOT NULL,
  `TXM` varchar(100) DEFAULT NULL,
  `TYName` varchar(145) DEFAULT NULL,
  `XKZ` varchar(145) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goodinfo`
--

LOCK TABLES `goodinfo` WRITE;
/*!40000 ALTER TABLE `goodinfo` DISABLE KEYS */;
INSERT INTO `goodinfo` VALUES (1,'12313','234','2018-07-26 21:44:38','US-0001','234','2018-07-26 22:05:58','US-0001','234','234','0001','棉签','234','234','234',22.00,'234','234','234234',0),(2,NULL,NULL,'2018-07-26 22:06:58','US-0001','123',NULL,NULL,NULL,NULL,'12313','0002','2131223','123',NULL,2.00,NULL,'123',NULL,1),(3,'23232','23','2018-09-28 09:17:15','US-0001','包',NULL,NULL,'23','10g','0003','创口贴','10','1231','拉稀客',10.00,'12313','1231','2323',0);
/*!40000 ALTER TABLE `goodinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goodsstore`
--

DROP TABLE IF EXISTS `goodsstore`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `goodsstore` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `Good_id` int(11) NOT NULL,
  `MJPH` varchar(100) DEFAULT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `Num` decimal(18,2) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `SCPH` varchar(100) DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `Store_id` int(11) NOT NULL,
  `scDate` datetime DEFAULT NULL,
  `yxqDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goodsstore`
--

LOCK TABLES `goodsstore` WRITE;
/*!40000 ALTER TABLE `goodsstore` DISABLE KEYS */;
INSERT INTO `goodsstore` VALUES (11,NULL,'包','10g','0003','创口贴',3,'123','10',10.00,10.00,'拉稀客','123','成品仓库',1,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(12,NULL,'234','234','0001','棉签',1,'123','234',18.00,20.00,'234','123','成品仓库',1,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(13,NULL,'包','10g','0003','创口贴',3,'123','10',0.00,10.00,'拉稀客','123','的风格的',2,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(14,NULL,'234','234','0001','棉签',1,'123','234',0.00,20.00,'234','123','的风格的',2,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(15,NULL,'234','234','0001','棉签',1,'456','234',0.00,20.00,'234','456','成品仓库',1,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(16,NULL,'包','10g','0003','创口贴',3,NULL,'10',0.00,10.00,'拉稀客',NULL,'成品仓库',1,NULL,NULL),(17,NULL,'234','234','0001','棉签',1,NULL,'234',0.00,20.00,'234',NULL,'成品仓库',1,NULL,NULL),(18,NULL,'包','10g','0003','创口贴',3,'123','10',50.00,20.00,'拉稀客','123','成品仓库',1,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(19,NULL,'234','234','0001','棉签',1,'123','234',37.00,20.00,'234','123','成品仓库',1,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(20,NULL,'包','10g','0003','创口贴',3,'456','10',30.00,5.00,'拉稀客','456','成品仓库',1,'2018-02-05 00:00:00','2019-02-02 00:00:00'),(21,NULL,'234','234','0001','棉签',1,'456','234',39.00,8.00,'234','456','成品仓库',1,'2018-02-05 00:00:00','2019-02-02 00:00:00'),(22,NULL,'包','10g','0003','创口贴',3,'456','10',50.00,10.00,'拉稀客','456','成品仓库',1,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(23,NULL,'234','234','0001','棉签',1,'456','234',90.00,11.00,'234','456','成品仓库',1,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(24,NULL,'包','10g','0003','创口贴',3,NULL,'10',12.00,1.34,'拉稀客',NULL,'成品仓库',1,NULL,NULL),(25,NULL,'234','234','0001','棉签',1,NULL,'234',5.00,2.55,'234',NULL,'成品仓库',1,NULL,NULL),(26,NULL,'包','10g','0003','创口贴',3,NULL,'10',10.00,12.55,'拉稀客','123','成品仓库',1,NULL,NULL),(27,NULL,'234','234','0001','棉签',1,NULL,'234',9.00,13.53,'234','12-','成品仓库',1,NULL,NULL),(28,NULL,'包','10g','0003','创口贴',3,NULL,'10',11.00,30.36,'拉稀客',NULL,'的风格的',2,NULL,NULL),(29,NULL,'234','234','0001','棉签',1,NULL,'234',10.00,100.00,'234',NULL,'的风格的',2,NULL,NULL);
/*!40000 ALTER TABLE `goodsstore` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `instorebill`
--

DROP TABLE IF EXISTS `instorebill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `instorebill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `BillDate` datetime DEFAULT NULL,
  `BillID` varchar(45) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `OrderBillID` varchar(45) DEFAULT NULL,
  `OrderBill_id` int(11) DEFAULT NULL,
  `SHDate` datetime DEFAULT NULL,
  `SHName` varchar(45) DEFAULT NULL,
  `SHStatus` int(11) DEFAULT NULL,
  `Sum` decimal(18,2) DEFAULT NULL,
  `SupName` varchar(145) DEFAULT NULL,
  `Sup_id` int(11) NOT NULL,
  `YSName` varchar(45) DEFAULT NULL,
  `YSNameID` int(11) NOT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StroeInfo_id` int(11) NOT NULL DEFAULT '0',
  `BillType` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `instorebill`
--

LOCK TABLES `instorebill` WRITE;
/*!40000 ALTER TABLE `instorebill` DISABLE KEYS */;
INSERT INTO `instorebill` VALUES (8,NULL,'2018-09-28 00:00:00','IS.20180928154237','2018-09-28 15:42:37','US-0001','OR.20180928154145',12,'2018-09-28 15:42:37','US-0001',1,500.00,'温州大商人',1,'战三',2,'成品仓库',1,'IS'),(12,NULL,'2018-09-28 00:00:00','IS.20180928160006','2018-09-28 16:00:07',NULL,'OR.20180928154145',12,'2018-09-28 16:00:07',NULL,1,600.00,'温州大商人',1,'战三',2,'成品仓库',1,'IS'),(13,NULL,'2018-09-30 00:00:00','BR.20180930165815','2018-09-30 16:58:15','US-0001',NULL,NULL,'2018-10-08 10:32:18','US-0001',1,400.00,'浙江小商人',2,'战三',2,'成品仓库',1,'BR'),(14,NULL,'2018-10-08 00:00:00','BR.20181008103320','2018-10-08 10:33:20','US-0001',NULL,NULL,'2018-10-08 11:23:58','US-0001',1,150.00,'浙江小商人',2,'系统管理员',1,'成品仓库',1,'BR'),(15,NULL,'2018-10-15 00:00:00','IS.20181015111203','2018-10-15 11:12:04','US-0001','OR.20180928160732',13,'2018-10-15 11:12:56','US-0001',1,3000.00,'温州大商人',1,'战三',2,'成品仓库',1,'IS'),(16,NULL,'2018-10-18 00:00:00','IS.20181018104235','2018-10-18 10:42:36','US-0001','OR.20181018104152',14,'2018-10-18 10:43:27','US-0001',1,470.00,'大老板',4,'战三',2,'成品仓库',1,'IS'),(17,NULL,'2018-10-19 00:00:00','IS.20181019083548','2018-10-19 08:35:48','US-0001','OR.20181019083457',15,'2018-10-19 08:35:48','US-0001',1,320.00,'瑞安小企业',3,'战三',2,'成品仓库',1,'IS'),(18,NULL,'2018-10-19 00:00:00','IS.20181019083653','2018-10-19 08:36:54','US-0001','OR.20181019083457',15,'2018-10-19 08:36:54','US-0001',1,420.00,'瑞安小企业',3,'战三',2,'成品仓库',1,'IS'),(19,NULL,'2018-10-19 00:00:00','BR.20181019083749','2018-10-19 08:37:50','US-0001',NULL,NULL,'2018-10-19 08:37:50','US-0001',1,210.00,'大老板',4,'战三',2,'成品仓库',1,'BR'),(20,NULL,'2018-10-19 00:00:00','IS.20181019083842','2018-10-19 08:38:42','US-0001','OR.20181019083457',15,'2018-10-19 08:38:42','US-0001',1,750.00,'瑞安小企业',3,'战三',2,'成品仓库',1,'IS'),(21,NULL,'2018-10-19 00:00:00','IS.20181019083930','2018-10-19 08:39:31','US-0001','OR.20181019083457',15,'2018-10-19 08:39:31','US-0001',1,110.00,'瑞安小企业',3,'战三',2,'成品仓库',1,'IS'),(22,NULL,'2018-11-04 00:00:00','IS.20181104172418','2018-11-04 17:24:19','US-0001','OR.20181104170230',17,'2018-11-04 17:24:19','US-0001',1,54.33,'大老板',4,'战三',2,'成品仓库',1,'IS'),(23,NULL,'2018-11-05 00:00:00','IS.20181105092523','2018-11-05 09:25:24','US-0001','OR.20181105084943',18,'2018-11-05 09:25:24','US-0001',1,328.45,'大老板',4,'战三',2,'成品仓库',1,'IS'),(24,NULL,'2018-11-05 00:00:00','BR.20181105092910','2018-11-05 09:29:10','US-0001',NULL,NULL,'2018-11-05 09:29:10','US-0001',1,101.58,'瑞安小企业',3,'系统管理员',1,'成品仓库',1,'BR'),(25,NULL,'2018-11-05 00:00:00','IS.20181105101247','2018-11-05 10:12:48','US-0001','OR.20181021100546',16,'2018-11-05 10:12:48','US-0001',1,1333.96,'大老板',4,'战三',2,'的风格的',2,'IS');
/*!40000 ALTER TABLE `instorebill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `instorebill_mx`
--

DROP TABLE IF EXISTS `instorebill_mx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `instorebill_mx` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `Bill_id` int(11) NOT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `Good_id` int(11) NOT NULL,
  `MJPH` varchar(100) DEFAULT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `Num` decimal(18,2) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `SCPH` varchar(100) DEFAULT NULL,
  `Sum` decimal(18,2) NOT NULL,
  `scDate` datetime DEFAULT NULL,
  `yxqDate` datetime DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StroeInfo_id` int(11) DEFAULT NULL,
  `OrderRow` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_InStoreBill_MX_Bill_id` (`Bill_id`),
  CONSTRAINT `FK_InStoreBill_MX_InStoreBill_Bill_id` FOREIGN KEY (`Bill_id`) REFERENCES `instorebill` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `instorebill_mx`
--

LOCK TABLES `instorebill_mx` WRITE;
/*!40000 ALTER TABLE `instorebill_mx` DISABLE KEYS */;
INSERT INTO `instorebill_mx` VALUES (16,NULL,8,'包','10g','0003','创口贴',3,'123','10',10.00,10.00,'拉稀客','123',100.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,18),(17,NULL,8,'234','234','0001','棉签',1,'123','234',20.00,20.00,'234','123',400.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,19),(24,NULL,12,'包','10g','0003','创口贴',3,'123','10',20.00,10.00,'拉稀客','123',200.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,18),(25,NULL,12,'234','234','0001','棉签',1,'123','234',20.00,20.00,'234','123',400.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,19),(26,NULL,13,'234','234','0001','棉签',1,'123','234',20.00,20.00,'234','123',400.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,12),(30,NULL,14,'234','234','0001','棉签',1,'123','234',5.00,20.00,'234','123',100.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,12),(31,NULL,14,'包','10g','0003','创口贴',3,'123','10',5.00,10.00,'拉稀客','123',50.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,11),(32,NULL,15,'包','10g','0003','创口贴',3,'123','10',50.00,20.00,'拉稀客','123',1000.00,'2018-05-02 00:00:00','2020-05-02 00:00:00','成品仓库',1,20),(33,NULL,15,'234','234','0001','棉签',1,'123','234',100.00,20.00,'234','123',2000.00,'2018-05-02 00:00:00','2020-05-02 00:00:00','成品仓库',1,21),(34,NULL,16,'包','10g','0003','创口贴',3,'456','10',30.00,5.00,'拉稀客','456',150.00,'2018-02-05 00:00:00','2019-02-02 00:00:00','成品仓库',1,22),(35,NULL,16,'234','234','0001','棉签',1,'456','234',40.00,8.00,'234','456',320.00,'2018-02-05 00:00:00','2019-02-02 00:00:00','成品仓库',1,23),(36,NULL,17,'包','10g','0003','创口贴',3,'456','10',10.00,10.00,'拉稀客','456',100.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,24),(37,NULL,17,'234','234','0001','棉签',1,'456','234',20.00,11.00,'234','456',220.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,25),(38,NULL,18,'包','10g','0003','创口贴',3,'456','10',20.00,10.00,'拉稀客','456',200.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,24),(39,NULL,18,'234','234','0001','棉签',1,'456','234',20.00,11.00,'234','456',220.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,25),(40,NULL,19,'包','10g','0003','创口贴',3,'123','10',10.00,10.00,'拉稀客','123',100.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,11),(41,NULL,19,'234','234','0001','棉签',1,'456','234',10.00,11.00,'234','456',110.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,23),(42,NULL,20,'包','10g','0003','创口贴',3,'456','10',20.00,10.00,'拉稀客','456',200.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,24),(43,NULL,20,'234','234','0001','棉签',1,'456','234',50.00,11.00,'234','456',550.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,25),(44,NULL,21,'234','234','0001','棉签',1,'456','234',10.00,11.00,'234','456',110.00,'2018-02-02 00:00:00','2020-02-02 00:00:00','成品仓库',1,25),(45,NULL,22,'包','10g','0003','创口贴',3,NULL,'10',12.00,1.34,'拉稀客',NULL,16.08,NULL,NULL,'成品仓库',1,28),(46,NULL,22,'234','234','0001','棉签',1,NULL,'234',15.00,2.55,'234',NULL,38.25,NULL,NULL,'成品仓库',1,29),(47,NULL,23,'包','10g','0003','创口贴',3,NULL,'10',10.00,12.55,'拉稀客','123',125.50,NULL,NULL,'成品仓库',1,30),(48,NULL,23,'234','234','0001','棉签',1,NULL,'234',15.00,13.53,'234','12-',202.95,NULL,NULL,'成品仓库',1,31),(49,NULL,24,'234','234','0001','棉签',1,NULL,'234',8.00,2.55,'234',NULL,20.40,NULL,NULL,'成品仓库',1,25),(50,NULL,24,'234','234','0001','棉签',1,NULL,'234',6.00,13.53,'234','12-',81.18,NULL,NULL,'成品仓库',1,27),(51,NULL,25,'包','10g','0003','创口贴',3,NULL,'10',11.00,30.36,'拉稀客',NULL,333.96,NULL,NULL,'的风格的',2,26),(52,NULL,25,'234','234','0001','棉签',1,NULL,'234',10.00,100.00,'234',NULL,1000.00,NULL,NULL,'的风格的',2,27);
/*!40000 ALTER TABLE `instorebill_mx` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `menu`
--

DROP TABLE IF EXISTS `menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(45) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `MenuID` varchar(45) DEFAULT NULL,
  `MenuIcon` varchar(100) DEFAULT NULL,
  `MenuName` varchar(45) DEFAULT NULL,
  `MenuNameCN` varchar(45) DEFAULT NULL,
  `MenuParentID` varchar(45) DEFAULT NULL,
  `MenuSort` int(11) DEFAULT NULL,
  `MenuType` int(11) DEFAULT NULL,
  `MenuUrl` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=152 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu`
--

LOCK TABLES `menu` WRITE;
/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
INSERT INTO `menu` VALUES (1,NULL,NULL,NULL,'ME-0001','layui-icon layui-icon-set','Home','系统设置','0',4,1,NULL),(2,NULL,NULL,NULL,'ME-0028',NULL,'Del','删除','ME-0007',3,2,NULL),(3,NULL,NULL,NULL,'ME-0027',NULL,'Edit','修改','ME-0007',2,2,NULL),(4,NULL,NULL,NULL,'ME-0026',NULL,'Add','新增','ME-0007',1,2,NULL),(5,NULL,NULL,NULL,'ME-0025',NULL,'Del','删除','ME-0006',3,2,NULL),(6,NULL,NULL,NULL,'ME-0024',NULL,'Edit','修改','ME-0006',2,2,NULL),(7,NULL,NULL,NULL,'ME-0023',NULL,'Add','新增','ME-0006',1,2,NULL),(8,NULL,NULL,NULL,'ME-0022',NULL,'Authorize','授权','ME-0005',4,2,NULL),(9,NULL,NULL,NULL,'ME-0021',NULL,'Search','查询','ME-0005',4,2,NULL),(10,NULL,NULL,NULL,'ME-0020',NULL,'Del','删除','ME-0005',3,2,NULL),(11,NULL,NULL,NULL,'ME-0019',NULL,'Edit','修改','ME-0005',2,2,NULL),(12,NULL,NULL,NULL,'ME-0018',NULL,'Add','新增','ME-0005',1,2,NULL),(13,NULL,NULL,NULL,'ME-0017',NULL,'Search','查询','ME-0004',4,2,NULL),(14,NULL,NULL,NULL,'ME-0016',NULL,'Del','删除','ME-0004',3,2,NULL),(15,NULL,NULL,NULL,'ME-0015',NULL,'Edit','修改','ME-0004',2,2,NULL),(16,NULL,NULL,NULL,'ME-0014',NULL,'Add','新增','ME-0004',1,2,NULL),(17,NULL,NULL,NULL,'ME-0013',NULL,'ShopReport','销售统计报表','ME-0011',3,1,'/SellTJReport/SellTJReportIndex'),(18,NULL,NULL,NULL,'ME-0012',NULL,'ShopBill','销售开单','ME-0011',1,1,'/Sell/SellIndex'),(19,NULL,NULL,NULL,'ME-0011','layui-icon layui-icon-set-fill','Shopping','销售管理','ME-0002',2,1,NULL),(20,NULL,NULL,NULL,'ME-0010',NULL,'PutSto','采购入库','ME-0008',2,1,'/InStore/InStoreIndex'),(21,NULL,NULL,NULL,'ME-0009',NULL,'Order','采购订单','ME-0008',1,1,'/OrderBill/OrderBillIndex'),(22,NULL,NULL,NULL,'ME-0008','layui-icon layui-icon-cart','Buy','采购管理','ME-0002',1,1,NULL),(23,NULL,NULL,NULL,'ME-0007',NULL,'Menu','菜单管理','ME-0001',4,1,'/Menu/MenuIndex'),(24,NULL,NULL,NULL,'ME-0006',NULL,'Office','部门管理','ME-0001',3,1,'/Office/OfficeIndex'),(25,NULL,NULL,NULL,'ME-0005',NULL,'Role','角色管理','ME-0001',2,1,'/Role/RoleIndex'),(26,NULL,NULL,NULL,'ME-0004',NULL,'Home','用户管理','ME-0001',1,1,'/User/UserIndex'),(27,NULL,NULL,NULL,'ME-0003','layui-icon layui-icon-app','App','财务报表','0',3,1,NULL),(28,NULL,NULL,NULL,'ME-0002','layui-icon layui-icon-component','component','综合业务','0',2,1,NULL),(29,NULL,'2018-05-21 13:56:52','US-0001','ME-0029',NULL,'BuyReport','采购统计报表','ME-0008',4,1,'/CGTJReport/CGTJReportIndex'),(30,NULL,'2018-06-05 15:05:31','US-0001','ME-0030',NULL,'Excel','导出','ME-0004',5,2,NULL),(31,NULL,'2018-06-15 10:13:47','US-0001','ME-0031','layui-icon layui-icon-form','Base','基础档案','0',1,1,NULL),(32,NULL,'2018-06-15 10:16:34','US-0001','ME-0032',NULL,'Goods','商品信息','ME-0031',1,1,'/GoodsInfo/GoodIndex'),(33,NULL,'2018-06-15 10:18:03','US-0001','ME-0033',NULL,'Supply','客商信息','ME-0031',2,1,'/SuperInfo/SupIndex'),(38,NULL,'2018-06-15 10:48:19','US-0001','ME-0037','layui-icon layui-icon-table','ShopReport','采购统计报表','ME-0003',1,1,'/CGTJReport/CGTJReportIndex'),(39,NULL,'2018-06-15 10:49:52','US-0001','ME-0038','layui-icon layui-icon-table','ShopAnalyse','采购分析报表','ME-0003',4,1,'/CGFXReport/CGFXReportIndex/'),(40,NULL,'2018-06-15 10:52:26','US-0001','ME-0039','layui-icon layui-icon-table','ShopReport','销售统计报表','ME-0003',5,1,'/SellTJReport/SellTJReportIndex/'),(42,NULL,'2018-06-15 10:58:26','US-0001','ME-0041','layui-icon layui-icon-table','MonthShopReport','销售明细报表','ME-0003',6,1,'/SellMXReport/SellMXReportIndex/'),(43,NULL,'2018-06-15 10:59:28','US-0001','ME-0042','layui-icon layui-icon-table','ShopAnalyse','销售分析报表','ME-0003',7,1,'/SellFXReport/SellFXReportIndex/'),(46,NULL,'2018-06-15 11:03:13','US-0001','ME-0045','layui-icon layui-icon-table','MonthPrefitRport','毛利月报表','ME-0003',10,1,'/MakeMoneyReport/MakeMoneyReportIndex/'),(47,NULL,'2018-06-15 11:05:08','US-0001','ME-0046','layui-icon layui-icon-table','PrefitAnasly','毛利分析报表','ME-0003',11,1,'/MakeMoneyTJ/MakeMoneyTJIndex'),(48,NULL,'2018-06-15 11:07:20','US-0001','ME-0047','layui-icon layui-icon-home','StockManage','库存管理','ME-0002',3,1,NULL),(49,NULL,'2018-06-15 11:09:30','US-0001','ME-0048',NULL,'BadManage','报损单','ME-0047',1,1,'/GoodsLose/GoodsLoseIndex'),(50,NULL,'2018-06-15 11:10:51','US-0001','ME-0049',NULL,'MoreBill','报溢单','ME-0047',2,1,'/GoodsMore/GoodsMoreIndex'),(51,NULL,'2018-06-15 11:13:32','US-0001','ME-0050',NULL,'StockTest','库存盘点','ME-0047',4,1,'/TakeStock/TakeStockIndex'),(52,NULL,'2018-06-15 11:15:16','US-0001','ME-0051',NULL,'Stock','库存信息','ME-0047',3,1,'/GoodsStore/GoodsStoreIndex'),(53,NULL,'2018-06-15 11:17:07','US-0001','ME-0052',NULL,'BadReport','报损报溢报表','ME-0047',5,1,'/MoreLoseReport/MoreLoseReportIndex'),(56,NULL,'2018-06-15 11:19:53','US-0001','ME-0055',NULL,'StockManage','仓库管理','ME-0031',5,1,'/StoreInfo/StoreIndex'),(58,NULL,'2018-07-20 15:34:22','US-0001','ME-0057',NULL,'Add','新增','ME-0032',1,2,NULL),(63,NULL,'2018-07-20 15:37:06','US-0001','ME-0058',NULL,'Edit','修改','ME-0032',2,2,NULL),(65,NULL,'2018-07-20 15:37:54','US-0001','ME-0059',NULL,'Del','删除','ME-0032',3,2,NULL),(66,NULL,'2018-07-20 15:38:12','US-0001','ME-0060',NULL,'Search','查询','ME-0032',4,2,NULL),(67,NULL,'2018-07-20 15:38:30','US-0001','ME-0061',NULL,'Excel','导出','ME-0032',5,2,NULL),(68,NULL,'2018-08-20 19:42:36','US-0001','ME-0062',NULL,'Add','新增','ME-0033',1,2,NULL),(69,NULL,'2018-08-20 19:42:49','US-0001','ME-0063',NULL,'Edit','修改','ME-0033',2,2,NULL),(70,NULL,'2018-08-20 19:43:18','US-0001','ME-0064',NULL,'Del','删除','ME-0033',3,2,NULL),(71,NULL,'2018-08-20 19:43:43','US-0001','ME-0065',NULL,'Search','查询','ME-0033',4,2,NULL),(72,NULL,'2018-08-20 19:44:01','US-0001','ME-0066',NULL,'Excel','导出','ME-0033',5,2,NULL),(73,NULL,'2018-08-20 22:01:10','US-0001','ME-0067',NULL,'Add','新增','ME-0055',1,2,NULL),(74,NULL,'2018-08-20 22:01:21','US-0001','ME-0068',NULL,'Edit','修改','ME-0055',2,2,NULL),(75,NULL,'2018-08-20 22:01:33','US-0001','ME-0069',NULL,'Del','删除','ME-0055',3,2,NULL),(76,NULL,'2018-08-20 22:01:50','US-0001','ME-0070',NULL,'Search','查询','ME-0055',4,2,NULL),(77,NULL,'2018-08-20 22:02:06','US-0001','ME-0071',NULL,'Excel','导出','ME-0055',5,2,NULL),(78,NULL,'2018-08-20 22:18:13','US-0001','ME-0072',NULL,'Add','新增','ME-0009',1,2,NULL),(79,NULL,'2018-08-20 22:18:25','US-0001','ME-0073',NULL,'Edit','修改','ME-0009',2,2,NULL),(80,NULL,'2018-08-20 22:18:43','US-0001','ME-0074',NULL,'Del','删除','ME-0009',3,2,NULL),(82,NULL,'2018-08-20 22:19:27','US-0001','ME-0076',NULL,'SH','审核确认','ME-0009',5,2,NULL),(83,NULL,'2018-08-20 22:20:02','US-0001','ME-0077',NULL,'NOSH','撤销审核','ME-0009',6,2,NULL),(84,NULL,'2018-08-20 22:20:18','US-0001','ME-0078',NULL,'Excel','导出','ME-0009',7,2,NULL),(85,NULL,'2018-08-20 22:20:34','US-0001','ME-0079',NULL,'Search','查询','ME-0009',8,2,NULL),(86,NULL,'2018-09-26 10:27:00','US-0001','ME-0080',NULL,'Add','新增','ME-0010',1,2,NULL),(87,NULL,'2018-09-26 10:27:14','US-0001','ME-0081',NULL,'Edit','修改','ME-0010',2,2,NULL),(88,NULL,'2018-09-26 10:27:29','US-0001','ME-0082',NULL,'Del','删除','ME-0010',3,2,NULL),(89,NULL,'2018-09-26 10:28:11','US-0001','ME-0083',NULL,'SH','审核确认','ME-0010',4,2,NULL),(90,NULL,'2018-09-26 10:28:34','US-0001','ME-0084',NULL,'NOSH','撤销审核','ME-0010',5,2,NULL),(91,NULL,'2018-09-26 10:29:00','US-0001','ME-0085',NULL,'Excel','导出','ME-0010',6,2,NULL),(92,NULL,'2018-09-26 10:29:16','US-0001','ME-0086',NULL,'Search','查询','ME-0010',7,2,NULL),(93,NULL,'2018-09-27 09:23:02','US-0001','ME-0087',NULL,'BuyReturn','采购退货','ME-0008',3,1,'/BuyReturn/BuyReturnIndex'),(94,NULL,'2018-09-27 09:23:52','US-0001','ME-0088',NULL,'ShopReturn','销售退回','ME-0011',2,1,'/SellReturn/SellReturnIndex'),(95,NULL,'2018-09-29 15:34:16','US-0001','ME-0089',NULL,'Excel','导出','ME-0051',1,2,NULL),(96,NULL,'2018-09-29 15:34:27','US-0001','ME-0090',NULL,'Search','查询','ME-0051',2,2,NULL),(97,NULL,'2018-09-30 15:05:31','US-0001','ME-0091',NULL,'Add','新增','ME-0087',1,2,NULL),(98,NULL,'2018-09-30 15:05:47','US-0001','ME-0092',NULL,'Edit','修改','ME-0087',2,2,NULL),(99,NULL,'2018-09-30 15:06:04','US-0001','ME-0093',NULL,'Del','删除','ME-0087',3,2,NULL),(100,NULL,'2018-09-30 15:06:29','US-0001','ME-0094',NULL,'SH','审核确认','ME-0087',4,2,NULL),(101,NULL,'2018-09-30 15:06:47','US-0001','ME-0095',NULL,'NOSH','撤销审核','ME-0087',5,2,NULL),(102,NULL,'2018-09-30 15:07:04','US-0001','ME-0096',NULL,'Excel','导出','ME-0087',6,2,NULL),(103,NULL,'2018-09-30 15:07:18','US-0001','ME-0097',NULL,'Search','查询','ME-0087',7,2,NULL),(104,NULL,'2018-10-08 15:38:31','US-0001','ME-0098',NULL,'Search','查询','ME-0029',1,2,NULL),(105,NULL,'2018-10-08 15:38:51','US-0001','ME-0099',NULL,'Excel','导出','ME-0029',2,2,NULL),(106,NULL,'2018-10-11 16:00:14','US-0001','ME-0100',NULL,'Add','新增','ME-0012',1,2,NULL),(107,NULL,'2018-10-11 16:00:35','US-0001','ME-0101',NULL,'Edit','修改','ME-0012',2,2,NULL),(108,NULL,'2018-10-11 16:01:04','US-0001','ME-0102',NULL,'Del','删除','ME-0012',3,2,NULL),(109,NULL,'2018-10-11 16:01:46','US-0001','ME-0103',NULL,'SH','单据过账','ME-0012',4,2,NULL),(110,NULL,'2018-10-11 16:02:53','US-0001','ME-0104',NULL,'Excel','导出','ME-0012',5,2,NULL),(111,NULL,'2018-10-11 16:04:47','US-0001','ME-0105',NULL,'Search','查询','ME-0012',6,2,NULL),(112,NULL,'2018-10-15 13:51:42','US-0001','ME-0106',NULL,'Add','新增','ME-0088',1,2,NULL),(113,NULL,'2018-10-15 13:51:52','US-0001','ME-0107',NULL,'Edit','修改','ME-0088',2,2,NULL),(114,NULL,'2018-10-15 13:52:01','US-0001','ME-0108',NULL,'Del','删除','ME-0088',3,2,NULL),(115,NULL,'2018-10-15 13:52:25','US-0001','ME-0109',NULL,'SH','过账确认','ME-0088',4,2,NULL),(116,NULL,'2018-10-15 13:52:46','US-0001','ME-0110',NULL,'Excel','导出','ME-0088',5,2,NULL),(117,NULL,'2018-10-15 13:53:00','US-0001','ME-0111',NULL,'Search','查询','ME-0088',6,2,NULL),(118,NULL,'2018-10-15 15:46:35','US-0001','ME-0112',NULL,'Excel','导出','ME-0013',1,2,NULL),(119,NULL,'2018-10-15 15:46:48','US-0001','ME-0113',NULL,'Search','查询','ME-0013',2,2,NULL),(120,NULL,'2018-10-15 16:41:44','US-0001','ME-0114',NULL,'Add','新增','ME-0048',1,2,NULL),(121,NULL,'2018-10-15 16:41:57','US-0001','ME-0115',NULL,'Edit','修改','ME-0048',2,2,NULL),(122,NULL,'2018-10-15 16:42:09','US-0001','ME-0116',NULL,'Del','删除','ME-0048',3,2,NULL),(123,NULL,'2018-10-15 16:42:36','US-0001','ME-0117',NULL,'SH','过账确认','ME-0048',4,2,NULL),(124,NULL,'2018-10-15 16:42:59','US-0001','ME-0118',NULL,'Excel','导出','ME-0048',5,2,NULL),(125,NULL,'2018-10-15 16:43:11','US-0001','ME-0119',NULL,'Search','查询','ME-0048',6,2,NULL),(126,NULL,'2018-10-16 13:59:40','US-0001','ME-0120',NULL,'Add','新增','ME-0049',1,2,NULL),(127,NULL,'2018-10-16 13:59:56','US-0001','ME-0121',NULL,'Edit','修改','ME-0049',2,2,NULL),(128,NULL,'2018-10-16 14:00:11','US-0001','ME-0122',NULL,'Del','删除','ME-0049',3,2,NULL),(129,NULL,'2018-10-16 14:00:39','US-0001','ME-0123',NULL,'SH','过账确认','ME-0049',4,2,NULL),(130,NULL,'2018-10-16 14:00:54','US-0001','ME-0124',NULL,'Excel','导出','ME-0049',5,2,NULL),(131,NULL,'2018-10-16 14:01:05','US-0001','ME-0125',NULL,'Search','查询','ME-0049',6,2,NULL),(132,NULL,'2018-10-16 14:15:08','US-0001','ME-0126',NULL,'Excel','导出','ME-0052',1,2,NULL),(133,NULL,'2018-10-16 14:15:23','US-0001','ME-0127',NULL,'Search','查询','ME-0052',2,2,NULL),(134,NULL,'2018-10-16 15:32:41','US-0001','ME-0128',NULL,'Add','盘盈盘亏','ME-0050',1,2,NULL),(135,NULL,'2018-10-16 15:32:52','US-0001','ME-0129',NULL,'Excel','导出','ME-0050',2,2,NULL),(136,NULL,'2018-10-16 15:33:05','US-0001','ME-0130',NULL,'Search','查询','ME-0050',3,2,NULL),(138,NULL,'2018-10-18 09:23:32','US-0001','ME-0132',NULL,'Excel','导出','ME-0037',2,2,NULL),(140,NULL,'2018-10-22 08:41:36','US-0001','ME-0134','layui-icon layui-icon-table','CGTJMX','采购明细统计报表','ME-0003',2,1,'/CGMXReport/CGMXReportIndex'),(141,NULL,'2018-10-22 13:36:20','US-0001','ME-0135',NULL,'Excel','导出','ME-0134',1,2,NULL),(142,NULL,'2018-10-23 13:27:03','US-0001','ME-0136',NULL,'Search','查询','ME-0038',1,2,NULL),(143,NULL,'2018-10-23 13:27:19','US-0001','ME-0137',NULL,'Search','查询','ME-0037',2,2,NULL),(144,NULL,'2018-10-23 13:27:34','US-0001','ME-0138',NULL,'Search','查询','ME-0134',2,2,NULL),(145,NULL,'2018-10-23 13:44:22','US-0001','ME-0139',NULL,'Search','查询','ME-0042',1,2,NULL),(146,NULL,'2018-10-23 14:26:18','US-0001','ME-0140',NULL,'Excel','导出','ME-0041',1,2,NULL),(147,NULL,'2018-10-23 14:26:27','US-0001','ME-0141',NULL,'Search','查询','ME-0041',2,2,NULL),(148,NULL,'2018-10-23 14:49:37','US-0001','ME-0142',NULL,'Excel','导出','ME-0039',1,2,NULL),(149,NULL,'2018-10-23 14:49:51','US-0001','ME-0143',NULL,'Search','查询','ME-0039',2,2,NULL),(150,NULL,'2018-10-23 15:20:19','US-0001','ME-0144',NULL,'Search','查询','ME-0046',1,2,NULL),(151,NULL,'2018-10-23 16:21:34','US-0001','ME-0145',NULL,'Search','查询','ME-0045',1,2,NULL);
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `morelosebill`
--

DROP TABLE IF EXISTS `morelosebill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `morelosebill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `BillDate` datetime DEFAULT NULL,
  `BillID` varchar(45) DEFAULT NULL,
  `BillType` varchar(10) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `SHDate` datetime DEFAULT NULL,
  `SHName` varchar(45) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StroeInfo_id` int(11) NOT NULL,
  `Sum` decimal(18,2) DEFAULT NULL,
  `YSName` varchar(45) DEFAULT NULL,
  `YSNameID` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `morelosebill`
--

LOCK TABLES `morelosebill` WRITE;
/*!40000 ALTER TABLE `morelosebill` DISABLE KEYS */;
INSERT INTO `morelosebill` VALUES (2,NULL,'2018-10-16 00:00:00','LS.20181016133601','LS','2018-10-16 13:36:02','US-0001','2018-10-16 13:36:02','US-0001',1,'成品仓库',1,220.00,'战三',2),(3,NULL,'2018-10-16 00:00:00','LS.20181016133635','LS','2018-10-16 13:36:35','US-0001','2018-10-16 13:38:11','US-0001',1,'成品仓库',1,220.00,'战三',2),(4,NULL,'2018-10-16 00:00:00','LS.20181016133835','LS','2018-10-16 13:38:36','US-0001','2018-10-16 13:39:03','US-0001',1,'成品仓库',1,60.00,'战三',2),(5,NULL,'2018-10-16 00:00:00','MR.20181016140145','MR','2018-10-16 14:01:45','US-0001','2018-10-16 14:01:45','US-0001',1,'成品仓库',1,260.00,'战三',2),(6,NULL,'2018-10-16 00:00:00','MR.20181016140418','MR','2018-10-16 14:04:18','US-0001','2018-10-16 14:04:45','US-0001',1,'成品仓库',1,310.00,'战三',2),(7,NULL,'2018-10-16 00:00:00','MR.20181016141312','MR','2018-10-16 14:13:13','US-0001','2018-10-16 14:13:25','US-0001',1,'成品仓库',1,80.00,'战三',2),(26,NULL,'2018-10-17 21:46:44','LS.20181017214644','LS','2018-10-17 21:46:44','US-0001','2018-10-17 21:46:44','US-0001',1,'成品仓库',1,250.00,'战三',2),(27,NULL,'2018-10-17 21:46:44','MR.20181017214644','MR','2018-10-17 21:46:44','US-0001','2018-10-17 21:46:44','US-0001',1,'成品仓库',1,100.00,'战三',2);
/*!40000 ALTER TABLE `morelosebill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `morelosebill_mx`
--

DROP TABLE IF EXISTS `morelosebill_mx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `morelosebill_mx` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `Bill_id` int(11) NOT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `Good_id` int(11) NOT NULL,
  `MJPH` varchar(100) DEFAULT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `Num` decimal(18,2) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `SCPH` varchar(100) DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StoreRow` int(11) DEFAULT NULL,
  `StroeInfo_id` int(11) DEFAULT NULL,
  `Sum` decimal(18,2) NOT NULL,
  `scDate` datetime DEFAULT NULL,
  `yxqDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_MoreLoseBill_MX_Bill_id` (`Bill_id`),
  CONSTRAINT `FK_MoreLoseBill_MX_MoreLoseBill_Bill_id` FOREIGN KEY (`Bill_id`) REFERENCES `morelosebill` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `morelosebill_mx`
--

LOCK TABLES `morelosebill_mx` WRITE;
/*!40000 ALTER TABLE `morelosebill_mx` DISABLE KEYS */;
INSERT INTO `morelosebill_mx` VALUES (1,NULL,2,'234','234','0001','棉签',1,'123','234',1.00,20.00,'234','123','成品仓库',12,1,20.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(2,NULL,2,'234','234','0001','棉签',1,'123','234',10.00,20.00,'234','123','成品仓库',19,1,200.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(5,NULL,3,'234','234','0001','棉签',1,'123','234',1.00,20.00,'234','123','成品仓库',12,1,20.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(6,NULL,3,'234','234','0001','棉签',1,'123','234',10.00,20.00,'234','123','成品仓库',19,1,200.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(11,NULL,4,'234','234','0001','棉签',1,'123','234',2.00,20.00,'234','123','成品仓库',12,1,40.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(12,NULL,4,'包','10g','0003','创口贴',3,'123','10',2.00,10.00,'拉稀客','123','成品仓库',11,1,20.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(13,NULL,5,'234','234','0001','棉签',1,'123','234',3.00,20.00,'234','123','成品仓库',12,1,60.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(14,NULL,5,'234','234','0001','棉签',1,'123','234',10.00,20.00,'234','123','成品仓库',19,1,200.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(17,NULL,6,'包','10g','0003','创口贴',3,'123','10',11.00,10.00,'拉稀客','123','成品仓库',11,1,110.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(18,NULL,6,'包','10g','0003','创口贴',3,'123','10',10.00,20.00,'拉稀客','123','成品仓库',18,1,200.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(21,NULL,7,'234','234','0001','棉签',1,'123','234',2.00,20.00,'234','123','成品仓库',12,1,40.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(22,NULL,7,'234','234','0001','棉签',1,'123','234',2.00,20.00,'234','123','成品仓库',19,1,40.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(59,NULL,26,'包','10g','0003','创口贴',3,'123','10',5.00,10.00,'拉稀客','123','成品仓库',11,1,50.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(60,NULL,26,'包','10g','0003','创口贴',3,'123','10',10.00,20.00,'拉稀客','123','成品仓库',18,1,200.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(61,NULL,27,'234','234','0001','棉签',1,'123','234',3.00,20.00,'234','123','成品仓库',12,1,60.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(62,NULL,27,'234','234','0001','棉签',1,'123','234',2.00,20.00,'234','123','成品仓库',19,1,40.00,'2018-05-02 00:00:00','2020-05-02 00:00:00');
/*!40000 ALTER TABLE `morelosebill_mx` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `office`
--

DROP TABLE IF EXISTS `office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `office` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `EditName` varchar(45) DEFAULT NULL,
  `EditeDate` datetime DEFAULT NULL,
  `FZName` varchar(45) DEFAULT NULL,
  `OfficeID` varchar(45) DEFAULT NULL,
  `OfficeName` varchar(45) DEFAULT NULL,
  `ParentID` varchar(45) DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `office`
--

LOCK TABLES `office` WRITE;
/*!40000 ALTER TABLE `office` DISABLE KEYS */;
INSERT INTO `office` VALUES (1,NULL,'2018-05-21 13:37:20','US-0001',NULL,NULL,'大师','OF-0001','全球总公司','0','正常'),(2,NULL,'2018-05-21 13:40:48','US-0001',NULL,NULL,'大师弟','OF-0002','中国总公司','OF-0001','正常');
/*!40000 ALTER TABLE `office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderbill`
--

DROP TABLE IF EXISTS `orderbill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orderbill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `BillDate` datetime DEFAULT NULL,
  `BillID` varchar(45) DEFAULT NULL,
  `CGNameID` int(11) NOT NULL,
  `SHDate` datetime DEFAULT NULL,
  `SHName` varchar(45) DEFAULT NULL,
  `SHStatus` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Sup_id` int(11) NOT NULL,
  `CGName` varchar(45) DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `SupName` varchar(145) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderbill`
--

LOCK TABLES `orderbill` WRITE;
/*!40000 ALTER TABLE `orderbill` DISABLE KEYS */;
INSERT INTO `orderbill` VALUES (12,NULL,'2018-09-28 00:00:00','OR.20180928154145',1,'2018-09-28 15:41:46','US-0001',1,1,1,'系统管理员','US-0001','温州大商人','2018-09-28 15:41:46'),(13,NULL,'2018-09-28 00:00:00','OR.20180928160732',2,'2018-09-28 16:07:33','US-0001',1,1,1,'战三','US-0001','温州大商人','2018-09-28 16:07:33'),(14,NULL,'2018-10-18 00:00:00','OR.20181018104152',2,'2018-10-18 10:41:53','US-0001',1,1,4,'战三','US-0001','大老板','2018-10-18 10:41:53'),(15,NULL,'2018-10-19 00:00:00','OR.20181019083457',2,'2018-10-19 08:34:57','US-0001',1,1,3,'战三','US-0001','瑞安小企业','2018-10-19 08:34:57'),(16,NULL,'2018-10-21 00:00:00','OR.20181021100546',2,'2018-10-21 10:05:46','US-0001',1,1,4,'战三','US-0001','大老板','2018-10-21 10:05:46'),(17,NULL,'2018-11-04 00:00:00','OR.20181104170230',1,'2018-11-04 17:02:31','US-0001',1,1,4,'系统管理员','US-0001','大老板','2018-11-04 17:02:31'),(18,NULL,'2018-11-05 00:00:00','OR.20181105084943',2,'2018-11-05 08:49:44','US-0001',1,1,4,'战三','US-0001','大老板','2018-11-05 08:49:44');
/*!40000 ALTER TABLE `orderbill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderbill_mx`
--

DROP TABLE IF EXISTS `orderbill_mx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orderbill_mx` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `Bill_id` int(11) NOT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `EndNum` decimal(18,2) NOT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `Good_id` int(11) NOT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `Num` decimal(18,2) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Sum` decimal(18,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_OrderBill_MX_Bill_id` (`Bill_id`),
  CONSTRAINT `FK_OrderBill_MX_OrderBill_Bill_id` FOREIGN KEY (`Bill_id`) REFERENCES `orderbill` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderbill_mx`
--

LOCK TABLES `orderbill_mx` WRITE;
/*!40000 ALTER TABLE `orderbill_mx` DISABLE KEYS */;
INSERT INTO `orderbill_mx` VALUES (18,NULL,12,'包',30.00,'10g','0003','创口贴',3,'10',30.00,10.00,'拉稀客',1,300.00),(19,NULL,12,'234',40.00,'234','0001','棉签',1,'234',40.00,20.00,'234',1,800.00),(20,NULL,13,'包',50.00,'10g','0003','创口贴',3,'10',50.00,20.00,'拉稀客',1,1000.00),(21,NULL,13,'234',100.00,'234','0001','棉签',1,'234',100.00,20.00,'234',1,2000.00),(22,NULL,14,'包',30.00,'10g','0003','创口贴',3,'10',30.00,5.00,'拉稀客',1,150.00),(23,NULL,14,'234',40.00,'234','0001','棉签',1,'234',40.00,8.00,'234',1,320.00),(24,NULL,15,'包',50.00,'10g','0003','创口贴',3,'10',50.00,10.00,'拉稀客',1,500.00),(25,NULL,15,'234',100.00,'234','0001','棉签',1,'234',100.00,11.00,'234',1,1100.00),(26,NULL,16,'包',11.00,'10g','0003','创口贴',3,'10',11.00,30.00,'拉稀客',1,330.00),(27,NULL,16,'234',10.00,'234','0001','棉签',1,'234',10.00,100.00,'234',1,1000.00),(28,NULL,17,'包',12.00,'10g','0003','创口贴',3,'10',12.00,1.34,'拉稀客',1,16.08),(29,NULL,17,'234',15.00,'234','0001','棉签',1,'234',15.00,2.55,'234',1,38.25),(30,NULL,18,'包',10.00,'10g','0003','创口贴',3,'10',10.00,12.55,'拉稀客',1,125.50),(31,NULL,18,'234',15.00,'234','0001','棉签',1,'234',15.00,13.53,'234',1,202.95);
/*!40000 ALTER TABLE `orderbill_mx` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `EditDate` datetime DEFAULT NULL,
  `EditName` varchar(45) DEFAULT NULL,
  `RoleID` varchar(45) DEFAULT NULL,
  `RoleName` varchar(45) DEFAULT NULL,
  `RoleType` varchar(45) DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,NULL,'2018-05-21 13:29:49','US-0001',NULL,NULL,'RL-0001','管理员','系统管理','正常'),(2,NULL,'2018-05-21 13:33:55','US-0001',NULL,NULL,'RL-0002','员工','业务管理','正常');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roleauthorize`
--

DROP TABLE IF EXISTS `roleauthorize`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `roleauthorize` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `MenuID` varchar(45) DEFAULT NULL,
  `RoleID` int(11) NOT NULL,
  `Sort` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=192 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roleauthorize`
--

LOCK TABLES `roleauthorize` WRITE;
/*!40000 ALTER TABLE `roleauthorize` DISABLE KEYS */;
INSERT INTO `roleauthorize` VALUES (51,NULL,NULL,'ME-0008',2,NULL),(52,NULL,NULL,'ME-0012',2,NULL),(53,NULL,NULL,'ME-0013',2,NULL),(54,NULL,NULL,'ME-0011',2,NULL),(55,NULL,NULL,'ME-0002',2,NULL),(56,NULL,NULL,'ME-0003',2,NULL),(57,NULL,NULL,'ME-0014',2,NULL),(58,NULL,NULL,'ME-0015',2,NULL),(59,NULL,NULL,'ME-0017',2,NULL),(60,NULL,NULL,'ME-0004',2,NULL),(61,NULL,NULL,'ME-0021',2,NULL),(62,NULL,NULL,'ME-0005',2,NULL),(63,NULL,NULL,'ME-0023',2,NULL),(64,NULL,NULL,'ME-0024',2,NULL),(65,NULL,NULL,'ME-0025',2,NULL),(66,NULL,NULL,'ME-0006',2,NULL),(67,NULL,NULL,'ME-0001',2,NULL),(68,NULL,NULL,'ME-0010',2,NULL),(69,NULL,NULL,'ME-0009',2,NULL),(70,NULL,NULL,'ME-0047',1,NULL),(71,NULL,NULL,'ME-0048',1,NULL),(72,NULL,NULL,'ME-0114',1,NULL),(73,NULL,NULL,'ME-0115',1,NULL),(74,NULL,NULL,'ME-0116',1,NULL),(75,NULL,NULL,'ME-0119',1,NULL),(76,NULL,NULL,'ME-0118',1,NULL),(77,NULL,NULL,'ME-0097',1,NULL),(78,NULL,NULL,'ME-0049',1,NULL),(79,NULL,NULL,'ME-0120',1,NULL),(80,NULL,NULL,'ME-0121',1,NULL),(81,NULL,NULL,'ME-0117',1,NULL),(82,NULL,NULL,'ME-0096',1,NULL),(83,NULL,NULL,'ME-0076',1,NULL),(84,NULL,NULL,'ME-0094',1,NULL),(85,NULL,NULL,'ME-0093',1,NULL),(86,NULL,NULL,'ME-0092',1,NULL),(87,NULL,NULL,'ME-0091',1,NULL),(88,NULL,NULL,'ME-0087',1,NULL),(89,NULL,NULL,'ME-0099',1,NULL),(90,NULL,NULL,'ME-0098',1,NULL),(91,NULL,NULL,'ME-0029',1,NULL),(92,NULL,NULL,'ME-0079',1,NULL),(93,NULL,NULL,'ME-0078',1,NULL),(94,NULL,NULL,'ME-0077',1,NULL),(95,NULL,NULL,'ME-0122',1,NULL),(96,NULL,NULL,'ME-0074',1,NULL),(97,NULL,NULL,'ME-0095',1,NULL),(98,NULL,NULL,'ME-0123',1,NULL),(99,NULL,NULL,'ME-0031',1,NULL),(100,NULL,NULL,'ME-0125',1,NULL),(101,NULL,NULL,'ME-0069',1,NULL),(102,NULL,NULL,'ME-0068',1,NULL),(103,NULL,NULL,'ME-0067',1,NULL),(104,NULL,NULL,'ME-0055',1,NULL),(105,NULL,NULL,'ME-0066',1,NULL),(106,NULL,NULL,'ME-0065',1,NULL),(107,NULL,NULL,'ME-0064',1,NULL),(108,NULL,NULL,'ME-0063',1,NULL),(109,NULL,NULL,'ME-0062',1,NULL),(110,NULL,NULL,'ME-0033',1,NULL),(111,NULL,NULL,'ME-0061',1,NULL),(112,NULL,NULL,'ME-0060',1,NULL),(113,NULL,NULL,'ME-0059',1,NULL),(114,NULL,NULL,'ME-0058',1,NULL),(115,NULL,NULL,'ME-0057',1,NULL),(116,NULL,NULL,'ME-0032',1,NULL),(117,NULL,NULL,'ME-0073',1,NULL),(118,NULL,NULL,'ME-0127',1,NULL),(119,NULL,NULL,'ME-0126',1,NULL),(120,NULL,NULL,'ME-0052',1,NULL),(121,NULL,NULL,'ME-0090',1,NULL),(122,NULL,NULL,'ME-0089',1,NULL),(123,NULL,NULL,'ME-0051',1,NULL),(124,NULL,NULL,'ME-0130',1,NULL),(125,NULL,NULL,'ME-0129',1,NULL),(126,NULL,NULL,'ME-0128',1,NULL),(127,NULL,NULL,'ME-0050',1,NULL),(128,NULL,NULL,'ME-0124',1,NULL),(129,NULL,NULL,'ME-0072',1,NULL),(130,NULL,NULL,'ME-0109',1,NULL),(131,NULL,NULL,'ME-0086',1,NULL),(132,NULL,NULL,'ME-0041',1,NULL),(133,NULL,NULL,'ME-0039',1,NULL),(134,NULL,NULL,'ME-0038',1,NULL),(135,NULL,NULL,'ME-0037',1,NULL),(136,NULL,NULL,'ME-0036',1,NULL),(137,NULL,NULL,'ME-0003',1,NULL),(138,NULL,NULL,'ME-0030',1,NULL),(139,NULL,NULL,'ME-0014',1,NULL),(140,NULL,NULL,'ME-0015',1,NULL),(141,NULL,NULL,'ME-0016',1,NULL),(142,NULL,NULL,'ME-0017',1,NULL),(143,NULL,NULL,'ME-0004',1,NULL),(144,NULL,NULL,'ME-0018',1,NULL),(145,NULL,NULL,'ME-0019',1,NULL),(146,NULL,NULL,'ME-0020',1,NULL),(147,NULL,NULL,'ME-0021',1,NULL),(148,NULL,NULL,'ME-0022',1,NULL),(149,NULL,NULL,'ME-0005',1,NULL),(150,NULL,NULL,'ME-0023',1,NULL),(151,NULL,NULL,'ME-0024',1,NULL),(152,NULL,NULL,'ME-0025',1,NULL),(153,NULL,NULL,'ME-0006',1,NULL),(154,NULL,NULL,'ME-0026',1,NULL),(155,NULL,NULL,'ME-0027',1,NULL),(156,NULL,NULL,'ME-0028',1,NULL),(157,NULL,NULL,'ME-0007',1,NULL),(158,NULL,NULL,'ME-0001',1,NULL),(159,NULL,NULL,'ME-0042',1,NULL),(160,NULL,NULL,'ME-0009',1,NULL),(161,NULL,NULL,'ME-0044',1,NULL),(162,NULL,NULL,'ME-0046',1,NULL),(163,NULL,NULL,'ME-0085',1,NULL),(164,NULL,NULL,'ME-0084',1,NULL),(165,NULL,NULL,'ME-0083',1,NULL),(166,NULL,NULL,'ME-0082',1,NULL),(167,NULL,NULL,'ME-0081',1,NULL),(168,NULL,NULL,'ME-0080',1,NULL),(169,NULL,NULL,'ME-0010',1,NULL),(170,NULL,NULL,'ME-0008',1,NULL),(171,NULL,NULL,'ME-0111',1,NULL),(172,NULL,NULL,'ME-0110',1,NULL),(173,NULL,NULL,'ME-0070',1,NULL),(174,NULL,NULL,'ME-0108',1,NULL),(175,NULL,NULL,'ME-0107',1,NULL),(176,NULL,NULL,'ME-0106',1,NULL),(177,NULL,NULL,'ME-0088',1,NULL),(178,NULL,NULL,'ME-0105',1,NULL),(179,NULL,NULL,'ME-0104',1,NULL),(180,NULL,NULL,'ME-0103',1,NULL),(181,NULL,NULL,'ME-0102',1,NULL),(182,NULL,NULL,'ME-0101',1,NULL),(183,NULL,NULL,'ME-0100',1,NULL),(184,NULL,NULL,'ME-0012',1,NULL),(185,NULL,NULL,'ME-0113',1,NULL),(186,NULL,NULL,'ME-0112',1,NULL),(187,NULL,NULL,'ME-0013',1,NULL),(188,NULL,NULL,'ME-0011',1,NULL),(189,NULL,NULL,'ME-0002',1,NULL),(190,NULL,NULL,'ME-0045',1,NULL),(191,NULL,NULL,'ME-0071',1,NULL);
/*!40000 ALTER TABLE `roleauthorize` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sellbill`
--

DROP TABLE IF EXISTS `sellbill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sellbill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `BillDate` datetime DEFAULT NULL,
  `BillID` varchar(45) DEFAULT NULL,
  `BillType` varchar(10) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `SellName` varchar(45) DEFAULT NULL,
  `SellNameID` int(11) NOT NULL,
  `Status` int(11) DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StroeInfo_id` int(11) NOT NULL,
  `Sum` decimal(18,2) DEFAULT NULL,
  `SupName` varchar(145) DEFAULT NULL,
  `Sup_id` int(11) NOT NULL,
  `GiveSum` decimal(18,2) DEFAULT NULL,
  `SHDate` datetime DEFAULT NULL,
  `SHName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sellbill`
--

LOCK TABLES `sellbill` WRITE;
/*!40000 ALTER TABLE `sellbill` DISABLE KEYS */;
INSERT INTO `sellbill` VALUES (1,NULL,'2018-10-15 00:00:00','SE.20181015102244','SE','2018-10-15 10:22:45','US-0001','战三',2,1,'成品仓库',1,34.00,'温州大商人',1,5.00,'2018-10-15 10:22:45','US-0001'),(2,NULL,'2018-10-15 00:00:00','SE.20181015103500','SE','2018-10-15 10:35:01','US-0001','战三',2,1,'成品仓库',1,32.00,'瑞安小企业',3,NULL,'2018-10-15 10:47:23','US-0001'),(3,NULL,'2018-10-15 00:00:00','SE.20181015104748','SE','2018-10-15 10:47:49','US-0001','系统管理员',1,1,'成品仓库',1,32.00,'瑞安小企业',3,NULL,'2018-10-15 10:47:49','US-0001'),(5,NULL,'2018-10-15 00:00:00','SE.20181015112210','SE','2018-10-15 11:22:10','US-0001','战三',2,1,'成品仓库',1,44.00,'瑞安小企业',3,5.00,'2018-10-15 11:25:28','US-0001'),(6,NULL,'2018-10-15 00:00:00','SE.20181015112615','SE','2018-10-15 11:26:15','US-0001','战三',2,1,'成品仓库',1,22.00,'瑞安小企业',3,2.00,'2018-10-15 11:26:15','US-0001'),(7,NULL,'2018-10-15 00:00:00','SE.20181015112728','SE','2018-10-15 11:27:29','US-0001','战三',2,1,'成品仓库',1,32.00,'瑞安小企业',3,NULL,'2018-10-15 11:27:38','US-0001'),(8,NULL,'2018-10-15 00:00:00','SE.20181015112813','SE','2018-10-15 11:28:13','US-0001','战三',2,1,'成品仓库',1,32.00,'温州大商人',1,NULL,'2018-10-15 11:28:29','US-0001'),(9,NULL,'2018-10-15 00:00:00','SR.20181015141404','SR','2018-10-15 14:14:05','US-0001','战三',2,1,'成品仓库',1,54.00,'瑞安小企业',3,6.00,'2018-10-15 14:14:05','US-0001'),(10,NULL,'2018-10-15 00:00:00','SR.20181015141457','SR','2018-10-15 14:14:58','US-0001','战三',2,1,'成品仓库',1,172.00,'瑞安小企业',3,NULL,'2018-10-15 14:15:16','US-0001'),(11,NULL,'2018-10-15 00:00:00','SR.20181015152654','SR','2018-10-15 15:26:54','US-0001','战三',2,1,'成品仓库',1,32.00,'温州大商人',1,NULL,'2018-10-15 15:26:54','US-0001'),(12,NULL,'2018-10-18 00:00:00','SE.20181018090857','SE','2018-10-18 09:08:57','US-0001','战三',2,1,'成品仓库',1,44.00,'瑞安小企业',3,NULL,'2018-10-18 09:08:57','US-0001'),(13,NULL,'2018-10-18 00:00:00','SR.20181018090945','SR','2018-10-18 09:09:46','US-0001','战三',2,1,'成品仓库',1,44.00,'瑞安小企业',3,NULL,'2018-10-18 09:09:46','US-0001'),(14,NULL,'2018-10-23 00:00:00','SE.20181023134740','SE','2018-10-23 13:47:41','US-0001','战三',2,1,'成品仓库',1,1100.00,'瑞安小企业',3,10.00,'2018-10-23 13:47:41','US-0001'),(15,NULL,'2018-11-05 00:00:00','SE.20181105093007','SE','2018-11-05 09:30:08','US-0001','战三',2,1,'成品仓库',1,66.87,'温州大商人',1,NULL,'2018-11-05 09:30:08','US-0001'),(16,NULL,'2018-11-05 00:00:00','SE.20181105093427','SE','2018-11-05 09:34:47','US-0001','战三',2,1,'成品仓库',1,112.06,'瑞安小企业',3,NULL,'2018-11-05 09:34:28','US-0001');
/*!40000 ALTER TABLE `sellbill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sellbill_mx`
--

DROP TABLE IF EXISTS `sellbill_mx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sellbill_mx` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `Bill_id` int(11) NOT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `Good_id` int(11) NOT NULL,
  `InPrice` decimal(18,2) NOT NULL,
  `MJPH` varchar(100) DEFAULT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `Num` decimal(18,2) NOT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `SCPH` varchar(100) DEFAULT NULL,
  `SellPrice` decimal(18,2) NOT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StoreRow` int(11) DEFAULT NULL,
  `StroeInfo_id` int(11) DEFAULT NULL,
  `Sum` decimal(18,2) NOT NULL,
  `scDate` datetime DEFAULT NULL,
  `yxqDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_SellBill_MX_Bill_id` (`Bill_id`),
  CONSTRAINT `FK_SellBill_MX_SellBill_Bill_id` FOREIGN KEY (`Bill_id`) REFERENCES `sellbill` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sellbill_mx`
--

LOCK TABLES `sellbill_mx` WRITE;
/*!40000 ALTER TABLE `sellbill_mx` DISABLE KEYS */;
INSERT INTO `sellbill_mx` VALUES (1,NULL,1,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(2,NULL,1,'包','10g','0003','创口贴',3,10.00,'123','10',1.00,'拉稀客','123',12.00,'成品仓库',11,1,12.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(3,NULL,2,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(4,NULL,2,'包','10g','0003','创口贴',3,10.00,'123','10',1.00,'拉稀客','123',10.00,'成品仓库',11,1,10.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(5,NULL,3,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(6,NULL,3,'包','10g','0003','创口贴',3,10.00,'123','10',1.00,'拉稀客','123',10.00,'成品仓库',11,1,10.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(11,NULL,5,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(12,NULL,5,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(13,NULL,6,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',19,1,22.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(16,NULL,7,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',19,1,22.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(17,NULL,7,'包','10g','0003','创口贴',3,20.00,'123','10',1.00,'拉稀客','123',10.00,'成品仓库',18,1,10.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(20,NULL,8,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(21,NULL,8,'包','10g','0003','创口贴',3,10.00,'123','10',1.00,'拉稀客','123',10.00,'成品仓库',11,1,10.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(22,NULL,9,'234','234','0001','棉签',1,20.00,'123','234',2.00,'234','123',22.00,'成品仓库',19,1,44.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(23,NULL,9,'包','10g','0003','创口贴',3,20.00,'123','10',1.00,'拉稀客','123',10.00,'成品仓库',18,1,10.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(26,NULL,10,'234','234','0001','棉签',1,20.00,'123','234',6.00,'234','123',22.00,'成品仓库',12,1,132.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(27,NULL,10,'包','10g','0003','创口贴',3,10.00,'123','10',4.00,'拉稀客','123',10.00,'成品仓库',11,1,40.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(28,NULL,11,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(29,NULL,11,'包','10g','0003','创口贴',3,10.00,'123','10',1.00,'拉稀客','123',10.00,'成品仓库',11,1,10.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(30,NULL,12,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(31,NULL,12,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(32,NULL,13,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(33,NULL,13,'234','234','0001','棉签',1,20.00,'123','234',1.00,'234','123',22.00,'成品仓库',12,1,22.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(34,NULL,14,'234','234','0001','棉签',1,20.00,'123','234',50.00,'234','123',22.00,'成品仓库',19,1,1100.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(35,NULL,15,'234','234','0001','棉签',1,2.55,'123','234',2.00,'234','123',22.26,'成品仓库',25,1,44.51,NULL,NULL),(36,NULL,15,'234','234','0001','棉签',1,8.00,'456','234',1.00,'234','456',22.36,'成品仓库',21,1,22.36,'2018-02-05 00:00:00','2019-02-02 00:00:00'),(37,NULL,16,'234','234','0001','棉签',1,20.00,'123','234',2.00,'234','123',22.35,'成品仓库',12,1,44.69,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(38,NULL,16,'234','234','0001','棉签',1,20.00,'123','234',3.00,'234','123',22.46,'成品仓库',19,1,67.37,'2018-05-02 00:00:00','2020-05-02 00:00:00');
/*!40000 ALTER TABLE `sellbill_mx` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storeinfo`
--

DROP TABLE IF EXISTS `storeinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `storeinfo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Address` varchar(145) DEFAULT NULL,
  `BZ` varchar(200) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `EditDate` datetime DEFAULT NULL,
  `EditName` varchar(45) DEFAULT NULL,
  `Sizes` varchar(145) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `UseSay` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storeinfo`
--

LOCK TABLES `storeinfo` WRITE;
/*!40000 ALTER TABLE `storeinfo` DISABLE KEYS */;
INSERT INTO `storeinfo` VALUES (1,'21313','123132','2018-08-20 22:05:54','US-0001','2018-08-20 22:06:01','US-0001','132213',1,'成品仓库','123圣大非省'),(2,'的风格',NULL,'2018-08-20 22:06:23','US-0001','2018-08-20 22:06:31','US-0001','的风格',2,'的风格的',NULL);
/*!40000 ALTER TABLE `storeinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supperinfo`
--

DROP TABLE IF EXISTS `supperinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `supperinfo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Address` varchar(200) DEFAULT NULL,
  `BZ` varchar(200) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `EditDate` datetime DEFAULT NULL,
  `EditName` varchar(45) DEFAULT NULL,
  `FZName` varchar(45) DEFAULT NULL,
  `PYM` varchar(145) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `SupID` varchar(45) DEFAULT NULL,
  `SupName` varchar(145) DEFAULT NULL,
  `SupType` int(11) DEFAULT NULL,
  `WeiXin` varchar(45) DEFAULT NULL,
  `dq` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supperinfo`
--

LOCK TABLES `supperinfo` WRITE;
/*!40000 ALTER TABLE `supperinfo` DISABLE KEYS */;
INSERT INTO `supperinfo` VALUES (1,'温州瑞安梅山村','22','2018-08-20 20:48:28','US-0001','2018-08-20 20:48:59','US-0001','大侠','1333','13888888',1,'0001','温州大商人',0,'11','浙江温州'),(2,NULL,NULL,'2018-08-20 20:50:13','US-0001','2018-08-20 20:50:22','US-0001',NULL,NULL,NULL,2,'223232','浙江小商人',2,NULL,NULL),(3,NULL,NULL,'2018-10-11 13:55:11','US-0001','2018-10-15 08:24:46','US-0001',NULL,NULL,NULL,1,'0002','瑞安小企业',2,NULL,NULL),(4,NULL,NULL,'2018-10-15 08:24:37','US-0001',NULL,NULL,NULL,NULL,NULL,1,'0003','大老板',1,NULL,NULL);
/*!40000 ALTER TABLE `supperinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `takestockbill`
--

DROP TABLE IF EXISTS `takestockbill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `takestockbill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `BillDate` datetime DEFAULT NULL,
  `BillID` varchar(45) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StroeInfo_id` int(11) NOT NULL,
  `YSName` varchar(45) DEFAULT NULL,
  `YSNameID` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `takestockbill`
--

LOCK TABLES `takestockbill` WRITE;
/*!40000 ALTER TABLE `takestockbill` DISABLE KEYS */;
INSERT INTO `takestockbill` VALUES (2,NULL,'2018-10-17 00:00:00','PD.20181017214644','2018-10-17 21:46:44','US-0001','成品仓库',1,'战三',2);
/*!40000 ALTER TABLE `takestockbill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `takestockbill_mx`
--

DROP TABLE IF EXISTS `takestockbill_mx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `takestockbill_mx` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(200) DEFAULT NULL,
  `Bill_id` int(11) NOT NULL,
  `DW` varchar(45) DEFAULT NULL,
  `GGType` varchar(45) DEFAULT NULL,
  `GoodID` varchar(45) DEFAULT NULL,
  `GoodName` varchar(145) DEFAULT NULL,
  `Good_id` int(11) NOT NULL,
  `HowNum` decimal(18,2) NOT NULL,
  `MJPH` varchar(100) DEFAULT NULL,
  `ModelType` varchar(45) DEFAULT NULL,
  `Price` decimal(18,2) NOT NULL,
  `SCCJ` varchar(145) DEFAULT NULL,
  `SCPH` varchar(100) DEFAULT NULL,
  `StockNum` decimal(18,2) NOT NULL,
  `StoreName` varchar(45) DEFAULT NULL,
  `StoreRow` int(11) DEFAULT NULL,
  `StroeInfo_id` int(11) DEFAULT NULL,
  `TakeNum` decimal(18,2) NOT NULL,
  `scDate` datetime DEFAULT NULL,
  `yxqDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_TakeStockBill_MX_Bill_id` (`Bill_id`),
  CONSTRAINT `FK_TakeStockBill_MX_TakeStockBill_Bill_id` FOREIGN KEY (`Bill_id`) REFERENCES `takestockbill` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `takestockbill_mx`
--

LOCK TABLES `takestockbill_mx` WRITE;
/*!40000 ALTER TABLE `takestockbill_mx` DISABLE KEYS */;
INSERT INTO `takestockbill_mx` VALUES (5,NULL,2,'234','234','0001','棉签',1,3.00,'123','234',20.00,'234','123',17.00,'成品仓库',12,1,20.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(6,NULL,2,'234','234','0001','棉签',1,2.00,'123','234',20.00,'234','123',88.00,'成品仓库',19,1,90.00,'2018-05-02 00:00:00','2020-05-02 00:00:00'),(7,NULL,2,'包','10g','0003','创口贴',3,-5.00,'123','10',10.00,'拉稀客','123',25.00,'成品仓库',11,1,20.00,'2018-02-02 00:00:00','2020-02-02 00:00:00'),(8,NULL,2,'包','10g','0003','创口贴',3,-10.00,'123','10',20.00,'拉稀客','123',60.00,'成品仓库',18,1,50.00,'2018-05-02 00:00:00','2020-05-02 00:00:00');
/*!40000 ALTER TABLE `takestockbill_mx` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `BZ` varchar(145) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateName` varchar(45) DEFAULT NULL,
  `EditDate` datetime DEFAULT NULL,
  `EditName` varchar(45) DEFAULT NULL,
  `LoginName` varchar(45) DEFAULT NULL,
  `LoginPWD` varchar(45) DEFAULT NULL,
  `OID` int(11) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `RID` int(11) DEFAULT NULL,
  `Sex` varchar(45) DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  `UserID` varchar(45) DEFAULT NULL,
  `UserName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_User_OID` (`OID`),
  KEY `IX_User_RID` (`RID`),
  CONSTRAINT `FK_User_Office_OID` FOREIGN KEY (`OID`) REFERENCES `office` (`id`),
  CONSTRAINT `FK_User_Role_RID` FOREIGN KEY (`RID`) REFERENCES `role` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'one3243',NULL,NULL,'2018-08-06 20:05:26','US-0001','Admin','123',NULL,NULL,NULL,NULL,NULL,'US-0001','系统管理员'),(2,'234324','2018-08-06 20:04:11','US-0001','2018-08-06 20:05:30','US-0001','zs','123',1,'213123',1,'男','正常','US-0002','战三');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'mycore'
--

--
-- Dumping routines for database 'mycore'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-11-05 14:02:58
