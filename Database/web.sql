/*
 Navicat MySQL Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 50710
 Source Host           : localhost:3306
 Source Schema         : web

 Target Server Type    : MySQL
 Target Server Version : 50710
 File Encoding         : 65001

 Date: 26/06/2018 20:57:54
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for 工厂表
-- ----------------------------
DROP TABLE IF EXISTS `工厂表`;
CREATE TABLE `工厂表`  (
  `工厂名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `工厂负责人` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂地址` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂邮编` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂介绍` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂电话` varchar(12) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂电子邮箱` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂经纬度` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`工厂名称`) USING BTREE,
  INDEX `集团分工厂`(`集团名称`) USING BTREE,
  INDEX `工厂名称`(`工厂名称`, `集团名称`) USING BTREE,
  CONSTRAINT `集团分工厂` FOREIGN KEY (`集团名称`) REFERENCES `集团表` (`集团名称`) ON DELETE NO ACTION ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 工厂表
-- ----------------------------
INSERT INTO `工厂表` VALUES ('工厂A1', '', '荆州南环路', NULL, '', NULL, NULL, '集团A', NULL);
INSERT INTO `工厂表` VALUES ('工厂A2', '', '荆州北京路', NULL, '', NULL, NULL, '集团A', NULL);
INSERT INTO `工厂表` VALUES ('工厂B1', NULL, '宜昌三峡', NULL, NULL, NULL, NULL, '集团B', NULL);
INSERT INTO `工厂表` VALUES ('工厂B2', NULL, '宜昌清江', NULL, NULL, NULL, NULL, '集团B', NULL);
INSERT INTO `工厂表` VALUES ('工厂B3', NULL, '宜昌某处', NULL, NULL, NULL, NULL, '集团B', NULL);

-- ----------------------------
-- Table structure for 文档库
-- ----------------------------
DROP TABLE IF EXISTS `文档库`;
CREATE TABLE `文档库`  (
  `行号` int(11) NOT NULL,
  `井号` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `文档名` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `上传时间` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `备注` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `类型` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`行号`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 文档库
-- ----------------------------
INSERT INTO `文档库` VALUES (10, '15', '韵腹相同韵母不同声母相同.xls', '1205171621', '', '技术文档');
INSERT INTO `文档库` VALUES (12, '11', '田 完美形声字 声同韵不同.xls', '1205171640', '', '技术文档');
INSERT INTO `文档库` VALUES (13, '12', '2b9024df.jpg', '1205241026', '', '技术文档');
INSERT INTO `文档库` VALUES (18, 'ABCE', '中文摘要.doc', '1206121524', 'aaa', '技术文档');
INSERT INTO `文档库` VALUES (19, '32', '新建 Microsoft Word 文档.doc', '1206291238', 'guzhang', '故障信息');
INSERT INTO `文档库` VALUES (21, '222', '定向井计算实用表.xls', '2015-6-19 9:26:58', '33333', NULL);

-- ----------------------------
-- Table structure for 机组表
-- ----------------------------
DROP TABLE IF EXISTS `机组表`;
CREATE TABLE `机组表`  (
  `机组编号` int(10) NOT NULL,
  `机组名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `机组描述` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `负责人` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `机组经纬度` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`机组编号`) USING BTREE,
  INDEX `机组所属`(`负责人`) USING BTREE,
  INDEX `a`(`工厂名称`) USING BTREE,
  INDEX `集团名称`(`集团名称`, `工厂名称`, `负责人`) USING BTREE,
  CONSTRAINT `机组表_ibfk_1` FOREIGN KEY (`集团名称`, `工厂名称`, `负责人`) REFERENCES `用户表` (`集团名称`, `工厂名称`, `用户名`) ON DELETE NO ACTION ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 机组表
-- ----------------------------
INSERT INTO `机组表` VALUES (10081, '机组A1001', NULL, '集团A', '工厂A1', 'jgx', NULL);
INSERT INTO `机组表` VALUES (10082, '机组A1002', NULL, '集团A', '工厂A1', 'bk001', NULL);
INSERT INTO `机组表` VALUES (10083, '机组A1003', NULL, '集团A', '工厂A1', 'jgx', NULL);
INSERT INTO `机组表` VALUES (10084, '机组A1003', NULL, '集团B', '工厂B2', 'bk002', NULL);

-- ----------------------------
-- Table structure for 用户表
-- ----------------------------
DROP TABLE IF EXISTS `用户表`;
CREATE TABLE `用户表`  (
  `用户编号` int(11) NOT NULL AUTO_INCREMENT,
  `用户名` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `密码` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `电子邮箱` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `电话号码` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `工厂名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `角色名称` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `备注` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`用户编号`) USING BTREE,
  INDEX `用户单位`(`工厂名称`, `集团名称`) USING BTREE,
  INDEX `用户角色`(`角色名称`) USING BTREE,
  INDEX `用户名`(`用户名`) USING BTREE,
  INDEX `用户名_2`(`用户名`, `工厂名称`, `集团名称`) USING BTREE,
  INDEX `用户名_3`(`用户名`, `工厂名称`) USING BTREE,
  INDEX `工厂名称`(`工厂名称`, `用户名`, `集团名称`) USING BTREE,
  INDEX `工厂名称_2`(`工厂名称`, `用户名`) USING BTREE,
  INDEX `工厂名称_3`(`工厂名称`) USING BTREE,
  INDEX `集团名称`(`集团名称`) USING BTREE,
  INDEX `集团名称_2`(`集团名称`, `工厂名称`, `用户名`) USING BTREE,
  CONSTRAINT `用户单位` FOREIGN KEY (`工厂名称`, `集团名称`) REFERENCES `工厂表` (`工厂名称`, `集团名称`) ON DELETE NO ACTION ON UPDATE RESTRICT,
  CONSTRAINT `用户角色` FOREIGN KEY (`角色名称`) REFERENCES `角色表` (`角色名称`) ON DELETE NO ACTION ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12559 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 用户表
-- ----------------------------
INSERT INTO `用户表` VALUES (10001, 'jgx', '123', '11111@163.com', '15134654841', '工厂A1', '集团A', '集团负责人', '');
INSERT INTO `用户表` VALUES (10002, 'admin', '123', '181313@163.com', '135351', '工厂A1', '集团A', '管理员', '');
INSERT INTO `用户表` VALUES (10005, 'bk001', '123', '123123@163.com', '13146431464', '工厂A1', '集团A', '机组人员', '');
INSERT INTO `用户表` VALUES (10007, 'bk002', '123', '1222@163.com', '1111', '工厂B2', '集团B', '机组人员', '');
INSERT INTO `用户表` VALUES (10015, 'jack', '123', '18163132129@163.com', '18131313155', '工厂A1', '集团A', '管理员', '');
INSERT INTO `用户表` VALUES (10068, 'zl', '123', '18216@63.com', '15454555222', '工厂A1', '集团A', '专家', '');
INSERT INTO `用户表` VALUES (12258, 'ls', '123', '1852@163.com', '1155445545', '工厂A1', '集团A', '专家', '');

-- ----------------------------
-- Table structure for 菜单表
-- ----------------------------
DROP TABLE IF EXISTS `菜单表`;
CREATE TABLE `菜单表`  (
  `菜单号` int(10) NOT NULL,
  `菜单名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `父菜单号` int(11) DEFAULT NULL,
  `菜单序号` int(11) DEFAULT NULL,
  `菜单描述` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `URL` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `菜单图标` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `菜单等级` int(11) DEFAULT NULL,
  PRIMARY KEY (`菜单名称`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 菜单表
-- ----------------------------
INSERT INTO `菜单表` VALUES (13, '用户管理', 1, 4, 'a', '/User/Show.aspx', 'a', 2);
INSERT INTO `菜单表` VALUES (21, '菜单管理', 2, 1, 'a', '/Menu/Show.aspx', 'a', 2);

-- ----------------------------
-- Table structure for 角色表
-- ----------------------------
DROP TABLE IF EXISTS `角色表`;
CREATE TABLE `角色表`  (
  `角色名称` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `角色描述` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`角色名称`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 角色表
-- ----------------------------
INSERT INTO `角色表` VALUES ('专家', NULL);
INSERT INTO `角色表` VALUES ('工厂负责人', NULL);
INSERT INTO `角色表` VALUES ('机组人员', NULL);
INSERT INTO `角色表` VALUES ('管理员', NULL);
INSERT INTO `角色表` VALUES ('集团负责人', NULL);

-- ----------------------------
-- Table structure for 集团表
-- ----------------------------
DROP TABLE IF EXISTS `集团表`;
CREATE TABLE `集团表`  (
  `集团名称` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `集团负责人` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团地址` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团邮编` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团介绍` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团电话` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团电子邮箱` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `集团经纬度` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`集团名称`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of 集团表
-- ----------------------------
INSERT INTO `集团表` VALUES ('集团A', '老总A', '湖北荆州', NULL, '', NULL, NULL, NULL);
INSERT INTO `集团表` VALUES ('集团B', '老总B', '湖北宜昌', NULL, '', NULL, NULL, NULL);

-- ----------------------------
-- View structure for menue
-- ----------------------------
DROP VIEW IF EXISTS `menue`;
CREATE ALGORITHM = UNDEFINED DEFINER = `root`@`localhost` SQL SECURITY DEFINER VIEW `menue` AS select `集团表`.`集团名称` AS `集团名称`,`工厂表`.`工厂名称` AS `工厂名称`,`用户表`.`用户名` AS `用户名`,`机组表`.`机组名称` AS `机组名称` from (((`集团表` left join `工厂表` on((`工厂表`.`集团名称` = `集团表`.`集团名称`))) left join `用户表` on(((`用户表`.`工厂名称` = `工厂表`.`工厂名称`) and (`用户表`.`集团名称` = `工厂表`.`集团名称`)))) left join `机组表` on(((`机组表`.`集团名称` = `用户表`.`集团名称`) and (`机组表`.`工厂名称` = `用户表`.`工厂名称`) and (`机组表`.`负责人` = `用户表`.`用户名`)))) order by `集团表`.`集团名称`,`工厂表`.`工厂名称`,`用户表`.`用户名`,`机组表`.`机组名称`;

SET FOREIGN_KEY_CHECKS = 1;
