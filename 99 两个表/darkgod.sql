/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : darkgod

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2022-05-04 18:22:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `acct` varchar(255) NOT NULL,
  `pass` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `lv` int(11) NOT NULL,
  `exp` int(11) NOT NULL,
  `power` int(11) NOT NULL,
  `coin` int(11) NOT NULL,
  `diamond` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of account
-- ----------------------------
INSERT INTO `account` VALUES ('3', '12', '12132', '华纱', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('4', '11', '12132', '闻人媗', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('5', '1234', '12132', '', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('6', '12341', '12132', '', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('7', '167', '12132', '', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('8', '121323124', '12132', '', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('9', '123', '12123', '游珍', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('10', '456', '12123', '白冰', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('11', '4566', '12123', '', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('12', '478', '12123', '', '1', '0', '150', '5000', '500');
INSERT INTO `account` VALUES ('13', '908', '12123', '许盈', '1', '0', '150', '5000', '500');
