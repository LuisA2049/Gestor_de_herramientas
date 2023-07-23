-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: bd_prestamos_itspp
-- ------------------------------------------------------
-- Server version	8.0.31

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
-- Table structure for table `tabla_bitacora`
--

DROP TABLE IF EXISTS `tabla_bitacora`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_bitacora` (
  `bitacora_id` int NOT NULL AUTO_INCREMENT,
  `bitacora_fecha` varchar(100) NOT NULL,
  `bitacora_operacion` varchar(9999) NOT NULL,
  PRIMARY KEY (`bitacora_id`)
) ENGINE=InnoDB AUTO_INCREMENT=88 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_bitacora`
--

LOCK TABLES `tabla_bitacora` WRITE;
/*!40000 ALTER TABLE `tabla_bitacora` DISABLE KEYS */;
INSERT INTO `tabla_bitacora` VALUES (79,'Fecha: 7/22/2023 4:33:26 PM','Cerro sesión el usuario: '),(80,'Fecha: 7/22/2023 4:37:01 PM','Inicio sesión el usuario: luis'),(81,'Fecha: 7/22/2023 4:38:22 PM','Cerro sesión el usuario: luis'),(82,'Fecha: 7/22/2023 4:39:56 PM','Inicio sesión el usuario: luis'),(83,'Fecha: 7/22/2023 4:41:04 PM','Cerro sesión el usuario: luis'),(84,'Fecha: 7/22/2023 4:41:06 PM','Cerro sesión el usuario: luis'),(85,'Fecha: 7/22/2023 4:41:23 PM','Inicio sesión el usuario: luis'),(86,'Fecha: 7/22/2023 4:45:12 PM','Cerro sesión el usuario: luis'),(87,'Fecha: 7/22/2023 4:45:13 PM','Cerro sesión el usuario: luis');
/*!40000 ALTER TABLE `tabla_bitacora` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabla_inventario`
--

DROP TABLE IF EXISTS `tabla_inventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_inventario` (
  `objeto_id` int NOT NULL AUTO_INCREMENT,
  `objeto_nombre` varchar(45) NOT NULL,
  `objeto_descripcion` varchar(500) NOT NULL,
  `objeto_cantidad` int NOT NULL,
  `objeto_disponible` varchar(13) NOT NULL,
  `objeto_estado` varchar(20) NOT NULL,
  `lab_id` int NOT NULL,
  PRIMARY KEY (`objeto_id`),
  KEY `fk_tabla_inventario_tabla_labs1_idx` (`lab_id`),
  CONSTRAINT `fk_tabla_inventario_tabla_labs1` FOREIGN KEY (`lab_id`) REFERENCES `tabla_labs` (`lab_id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_inventario`
--

LOCK TABLES `tabla_inventario` WRITE;
/*!40000 ALTER TABLE `tabla_inventario` DISABLE KEYS */;
/*!40000 ALTER TABLE `tabla_inventario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabla_labs`
--

DROP TABLE IF EXISTS `tabla_labs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_labs` (
  `lab_id` int NOT NULL,
  `lab_nombre` varchar(45) NOT NULL,
  `lab_locacion` varchar(1) NOT NULL,
  PRIMARY KEY (`lab_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_labs`
--

LOCK TABLES `tabla_labs` WRITE;
/*!40000 ALTER TABLE `tabla_labs` DISABLE KEYS */;
INSERT INTO `tabla_labs` VALUES (1,'redes','a'),(2,'centro de computo - b','b'),(3,'centro de computo - c','c'),(4,'turismo','c'),(5,'laboratorio de ing. civil','c'),(9,'todos','©');
/*!40000 ALTER TABLE `tabla_labs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabla_prestamos`
--

DROP TABLE IF EXISTS `tabla_prestamos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_prestamos` (
  `prestamos_id` int NOT NULL AUTO_INCREMENT,
  `prestamos_fecha_prestamo` date DEFAULT NULL,
  `prestamos_fecha_retorno` date DEFAULT NULL,
  `prestamos_comentarios` varchar(45) DEFAULT NULL,
  `usuarios_id` int DEFAULT NULL,
  `prestatario_numControl` int DEFAULT NULL,
  PRIMARY KEY (`prestamos_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_prestamos`
--

LOCK TABLES `tabla_prestamos` WRITE;
/*!40000 ALTER TABLE `tabla_prestamos` DISABLE KEYS */;
/*!40000 ALTER TABLE `tabla_prestamos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabla_prestatario`
--

DROP TABLE IF EXISTS `tabla_prestatario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_prestatario` (
  `prestatario_numControl` int NOT NULL,
  `prestatario_nombre` varchar(45) NOT NULL,
  `prestatario_apellido` varchar(45) NOT NULL,
  `prestatario_carrera` varchar(45) NOT NULL,
  `prestatario_semestre` varchar(45) NOT NULL,
  `prestatario_correo` varchar(60) NOT NULL,
  `prestatario_numero` double NOT NULL,
  PRIMARY KEY (`prestatario_numControl`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_prestatario`
--

LOCK TABLES `tabla_prestatario` WRITE;
/*!40000 ALTER TABLE `tabla_prestatario` DISABLE KEYS */;
/*!40000 ALTER TABLE `tabla_prestatario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabla_prestatario_herramientas`
--

DROP TABLE IF EXISTS `tabla_prestatario_herramientas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_prestatario_herramientas` (
  `prestamos_id` int DEFAULT NULL,
  `objeto_id` int DEFAULT NULL,
  `objeto_cantidad` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_prestatario_herramientas`
--

LOCK TABLES `tabla_prestatario_herramientas` WRITE;
/*!40000 ALTER TABLE `tabla_prestatario_herramientas` DISABLE KEYS */;
/*!40000 ALTER TABLE `tabla_prestatario_herramientas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tabla_usuarios`
--

DROP TABLE IF EXISTS `tabla_usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tabla_usuarios` (
  `usuarios_id` int NOT NULL AUTO_INCREMENT,
  `usuarios_nombre` varchar(45) DEFAULT NULL,
  `usuarios_contraseña` varchar(45) DEFAULT NULL,
  `usuarios_tipo` varchar(45) DEFAULT NULL,
  `lab_id` int DEFAULT NULL,
  PRIMARY KEY (`usuarios_id`),
  UNIQUE KEY `usuarios_id_UNIQUE` (`usuarios_id`),
  KEY `fk_tabla_usuarios_tabla_labs_idx` (`lab_id`),
  CONSTRAINT `fk_tabla_usuarios_tabla_labs` FOREIGN KEY (`lab_id`) REFERENCES `tabla_labs` (`lab_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tabla_usuarios`
--

LOCK TABLES `tabla_usuarios` WRITE;
/*!40000 ALTER TABLE `tabla_usuarios` DISABLE KEYS */;
INSERT INTO `tabla_usuarios` VALUES (1,'luis','admin','Admin',9),(2,'admin','admin','Admin',9),(3,'ortiz','ortiz','Laboratorista',1),(5,'pedro','pedro','Laboratorista',2);
/*!40000 ALTER TABLE `tabla_usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-22 16:56:40
